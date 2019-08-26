using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using ODataAuthorization.OData.Infrastructure;

namespace ODataAuthorization.OData.ColumnFilter
{
	public class DefaultColumnSelector : IColumnSelector
	{
		public ImmutableArray<IWhitelist> Whitelists { get; }

		public ImmutableArray<IBlacklist> Blacklists { get; }

		public DefaultColumnSelector(IEnumerable<IWhitelist> whitelists, IEnumerable<IBlacklist> blacklists)
		{
			Whitelists = whitelists.ToImmutableArray();
			Blacklists = blacklists.ToImmutableArray();
		}

		/// <inheritdoc />
		public IQueryable FromMultiple<TEntity>(IQueryable<TEntity> source)
		{
			return source.Select(GetExpression<TEntity>());
		}

		/// <inheritdoc />
		public object FromSingle<TEntity>(TEntity source)
		{
			return new[] { source }.AsQueryable().Select(GetExpression<TEntity>());
		}

		private string GetExpression<TEntity>()
		{
			var whitelists = GetWhitelists<TEntity>();
			var blacklists = GetBlacklists<TEntity>();
			var columnMappings = GetColumnMappings<TEntity>().ToDictionary(d => d.propertyName, d => d.serializationName);

			var allowed = whitelists.SelectMany(d => d.GetPropertyNames()).ToHashSet();
			if (allowed.Count == 0)
				allowed = new HashSet<string>(columnMappings.Select(d => d.Key));

			var disallowed = blacklists.SelectMany(d => d.GetPropertyNames());
			var filtered = allowed
				.Where(d => !disallowed.Contains(d))
				.Select(propertyName => (found: columnMappings.TryGetValue(propertyName, out var serializationValue), serializationValue, propertyName));

			var expression = $"new ({string.Join(',', filtered.Where(d => d.found).Select(d => $"{d.propertyName} as {d.serializationValue}"))})";
			return expression;
		}

		private IEnumerable<IBlacklist<TEntity>> GetBlacklists<TEntity>()
		{
			return Blacklists.Where(d => d is IBlacklist<TEntity>).Cast<IBlacklist<TEntity>>();
		}

		private IEnumerable<IWhitelist<TEntity>> GetWhitelists<TEntity>()
		{
			return Whitelists.Where(d => d is IWhitelist<TEntity>).Cast<IWhitelist<TEntity>>();
		}

		private IEnumerable<(string propertyName, string serializationName)> GetColumnMappings<TEntity>()
		{
			return typeof(TEntity).GetProperties().Select(d => (d.Name, ToCamelCase(d.Name)));
		}

		private string ToCamelCase(string source)
		{
			return source.Substring(0, 1).ToLowerInvariant() + source.Substring(1);
		}
	}
}