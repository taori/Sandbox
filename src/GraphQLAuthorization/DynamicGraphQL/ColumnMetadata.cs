using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using GraphQLAuthorization.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLAuthorization.DynamicGraphQL
{
	[DebuggerDisplay("ColumnMetadata {ColumnName}")]
	public class ColumnMetadata
	{
		public string ColumnName { get; set; }
		public string DataType { get; set; }
	}

	[DebuggerDisplay("TableMetadata {TableName}")]
	public class TableMetadata
	{
		public string TableName { get; set; }
		public string AssemblyFullName { get; set; }
		public IEnumerable<ColumnMetadata> Columns { get; set; }
	}

	public interface ITableNameLookup
	{
		bool InsertKeyName(string friendlyName);
		string GetFriendlyName(string correctName);
	}

	public class TableNameLookup : ITableNameLookup
	{
		private IDictionary<string, string> _lookupTable = new Dictionary<string, string>();

		public bool InsertKeyName(string correctName)
		{
			if (!_lookupTable.ContainsKey(correctName))
			{
				var friendlyName = CanonicalName(correctName);
				_lookupTable.Add(correctName, friendlyName);
				return true;
			}
			return false;
		}

		public string GetFriendlyName(string correctName)
		{
			if (!_lookupTable.TryGetValue(correctName, out string value))
				throw new Exception($"Could not get {correctName} out of the list.");
			return value;
		}

		private string CanonicalName(string correctName)
		{
			var index = correctName.LastIndexOf("_");

			var result = correctName.Substring(
				index + 1,
				correctName.Length - index - 1);

			return char.ToLowerInvariant(result[0]) + result.Substring(1);
		}
	}
	public interface IDatabaseMetadata
	{
		void ReloadMetadata();
		IEnumerable<TableMetadata> GetTableMetadatas();
	}

	public sealed class DatabaseMetadata : IDatabaseMetadata
	{
		private readonly DbContext _dbContext;
		private readonly ITableNameLookup _tableNameLookup;

		private string _databaseName;
		private IEnumerable<TableMetadata> _tables;

		public DatabaseMetadata(DbContext dbContext, ITableNameLookup tableNameLookup)
		{
			_dbContext = dbContext;
			_tableNameLookup = tableNameLookup;

			_databaseName = _dbContext.Database.GetDbConnection().Database;

			if (_tables == null)
				ReloadMetadata();
		}

		public IEnumerable<TableMetadata> GetTableMetadatas()
		{
			if (_tables == null)
				return new List<TableMetadata>();

			return _tables;
		}

		public void ReloadMetadata()
		{
			_tables = FetchTableMetaData();
		}

		private IReadOnlyList<TableMetadata> FetchTableMetaData()
		{
			var metaTables = new List<TableMetadata>();

			foreach (var entityType in _dbContext.Model.GetEntityTypes())
			{
				var tableName = entityType.Relational().TableName;

				metaTables.Add(new TableMetadata
				{
					TableName = tableName,
					AssemblyFullName = entityType.ClrType.FullName,
					Columns = GetColumnsMetadata(entityType)
				});

				_tableNameLookup.InsertKeyName(tableName);
			}

			return metaTables;
		}

		private IReadOnlyList<ColumnMetadata> GetColumnsMetadata(IEntityType entityType)
		{
			var tableColumns = new List<ColumnMetadata>();

			foreach (var propertyType in entityType.GetProperties())
			{
				var relational = propertyType.Relational();
				tableColumns.Add(new ColumnMetadata
				{
					ColumnName = relational.ColumnName,
					DataType = relational.ColumnType
				});
			}

			return tableColumns;
		}
	}

	public class TableType : ObjectGraphType<object>
	{
		public QueryArguments TableArgs
		{
			get; set;
		}

		private IDictionary<string, Type> _databaseTypeToSystemType;
		protected IDictionary<string, Type> DatabaseTypeToSystemType
		{
			get
			{
				if (_databaseTypeToSystemType == null)
				{
					_databaseTypeToSystemType = new Dictionary<string, Type> {
					{ "uniqueidentifier", typeof(string) },
					{ "char", typeof(string) },
					{ "nvarchar", typeof(string) },
					{ "int", typeof(int) },
					{ "decimal", typeof(decimal) },
					{ "bit", typeof(bool) }
				};
				}
				return _databaseTypeToSystemType;
			}
		}

		public TableType(TableMetadata tableMetadata)
		{
			Name = tableMetadata.TableName;
			foreach (var tableColumn in tableMetadata.Columns)
			{
				InitGraphTableColumn(tableColumn);
			}
		}

		private void InitGraphTableColumn(ColumnMetadata columnMetadata)
		{
			var graphQLType = (ResolveColumnMetaType(columnMetadata.DataType)).GetGraphTypeFromType(true);
			var columnField = Field(
				graphQLType,
				columnMetadata.ColumnName
			);

			columnField.Resolver = new NameFieldResolver();
			FillArgs(columnMetadata.ColumnName);
		}

		private void FillArgs(string columnName)
		{
			if (TableArgs == null)
			{
				TableArgs = new QueryArguments(
					new QueryArgument<StringGraphType>()
					{
						Name = columnName
					}
				);
			}
			else
			{
				TableArgs.Add(new QueryArgument<StringGraphType> { Name = columnName });
			}

			TableArgs.Add(new QueryArgument<IdGraphType> { Name = "id" });
			TableArgs.Add(new QueryArgument<IntGraphType> { Name = "first" });
			TableArgs.Add(new QueryArgument<IntGraphType> { Name = "offset" });
		}

		private Type ResolveColumnMetaType(string dbType)
		{
			if (DatabaseTypeToSystemType.ContainsKey(dbType))
				return DatabaseTypeToSystemType[dbType];

			return typeof(string);
		}
	}

	public class NameFieldResolver : IFieldResolver
	{
		public object Resolve(ResolveFieldContext context)
		{
			var source = context.Source;

			if (source == null)
			{
				return null;
			}

			var name = char.ToUpperInvariant(context.FieldAst.Name[0]) + context.FieldAst.Name.Substring(1);
			var value = GetPropValue(source, name);

			if (value == null)
			{
				throw new InvalidOperationException($"Expected to find property {context.FieldAst.Name} on {context.Source.GetType().Name} but it does not exist.");
			}

			return value;
		}

		private static object GetPropValue(object src, string propName)
		{
			return src.GetType().GetProperty(propName).GetValue(src, null);
		}
	}

	public class MyFieldResolver : IFieldResolver
	{
		private TableMetadata _tableMetadata;
		private DbContext _dbContext;

		public MyFieldResolver(TableMetadata tableMetadata, DbContext dbContext)
		{
			_tableMetadata = tableMetadata;
			_dbContext = dbContext;
		}

		public object Resolve(ResolveFieldContext context)
		{
			var queryable = _dbContext.Query(_tableMetadata.AssemblyFullName);
			if (context.FieldName.Contains("_list"))
			{

				var first = context.Arguments["first"] != null ?
					context.GetArgument("first", int.MaxValue) :
					int.MaxValue;

				var offset = context.Arguments["offset"] != null ?
					context.GetArgument("offset", 0) :
					0;

				return queryable
					.Skip(offset)
					.Take(first)
					.ToDynamicList<object>();
			}
			else
			{
				var id = context.GetArgument<Guid>("id");
				return queryable.FirstOrDefault($"Id == @0", id);
			}
		}
	}

	public static class DbContextExtensions
	{
		public static IQueryable Query(this DbContext context, string entityName) =>
			context.Query(context.Model.FindEntityType(entityName).ClrType);

		static readonly MethodInfo SetMethod = typeof(DbContext).GetMethod(nameof(DbContext.Set));

		public static IQueryable Query(this DbContext context, Type entityType) =>
			(IQueryable)SetMethod.MakeGenericMethod(entityType).Invoke(context, null);
	}

	public class GraphQLQuery : ObjectGraphType<object>
	{
		private IDatabaseMetadata _dbMetadata;
		private ITableNameLookup _tableNameLookup;
		private DbContext _dbContext;

		public GraphQLQuery(
			DbContext dbContext,
			IDatabaseMetadata dbMetadata,
			ITableNameLookup tableNameLookup)
		{
			_dbMetadata = dbMetadata;
			_tableNameLookup = tableNameLookup;
			_dbContext = dbContext;

			Name = "Query";

			foreach (var metaTable in _dbMetadata.GetTableMetadatas())
			{
				var tableType = new TableType(metaTable);
				var friendlyTableName = _tableNameLookup.GetFriendlyName(metaTable.TableName);

				AddField(new FieldType
				{
					Name = friendlyTableName,
					Type = tableType.GetType(),
					ResolvedType = tableType,
					Resolver = new MyFieldResolver(metaTable, _dbContext),
					Arguments = new QueryArguments(
						tableType.TableArgs
					)
				});

				// lets add key to get list of current table
				var listType = new ListGraphType(tableType);
				AddField(new FieldType
				{
					Name = $"{friendlyTableName}_list",
					Type = listType.GetType(),
					ResolvedType = listType,
					Resolver = new MyFieldResolver(metaTable, _dbContext),
					Arguments = new QueryArguments(
						tableType.TableArgs
					)
				});
			}
		}
	}

	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDynamicGraphQL(this IServiceCollection services)
		{
			services.AddScoped<ITableNameLookup, TableNameLookup>();
			services.AddScoped<IDatabaseMetadata, DatabaseMetadata>();
			services.AddScoped((resolver) =>
			{
				var dbContext = resolver.GetRequiredService<AdventureWorksContext>();
				var metaDatabase = resolver.GetRequiredService<IDatabaseMetadata>();
				var tableNameLookup = resolver.GetRequiredService<ITableNameLookup>();

				var schema = new Schema { Query = new GraphQLQuery(dbContext, metaDatabase, tableNameLookup) };
				schema.Initialize();

				return schema;
			});

			return services;
		}
	}
}
