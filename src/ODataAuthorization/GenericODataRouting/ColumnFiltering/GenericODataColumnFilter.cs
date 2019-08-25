using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ODataAuthorization.GenericODataRouting.ColumnFiltering
{
	internal class GenericODataColumnFilter<TEntity> : IODataColumnFilter<TEntity>
	{
		public IServiceProvider ServiceProvider { get; }

		private readonly string SelectExpression;

		public GenericODataColumnFilter(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
			SelectExpression = BuildExpression();
		}

		private string BuildExpression()
		{
			var whitelists = ServiceProvider.GetServices<IODataColumnWhitelist<TEntity>>();
			var blacklists = ServiceProvider.GetServices<IODataColumnBlacklist<TEntity>>();
			var columnMappings = GetColumnMappings().ToDictionary(d => d.propertyName, d => d.serializationName);

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

		private IEnumerable<(string propertyName, string serializationName)> GetColumnMappings()
		{
			return typeof(TEntity).GetProperties().Select(d => (d.Name, ToCamelCase(d.Name)));
		}

		private string ToCamelCase(string source)
		{
			return source.Substring(0, 1).ToLowerInvariant() + source.Substring(1);
		}

		/// <inheritdoc />
		public IQueryable Apply(IQueryable<TEntity> source)
		{
			return source.Select(SelectExpression);
		}

		/// <inheritdoc />
		public object Apply(TEntity source)
		{
			return new[] { source }.AsQueryable().Select(SelectExpression);
		}
	}
}