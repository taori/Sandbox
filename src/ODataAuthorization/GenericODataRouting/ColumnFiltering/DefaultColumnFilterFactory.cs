using System;
using Microsoft.Extensions.DependencyInjection;

namespace ODataAuthorization.GenericODataRouting.ColumnFiltering
{
	internal class DefaultColumnFilterFactory : IODataColumnFilterFactory
	{
		public IServiceProvider ServiceProvider { get; }

		public DefaultColumnFilterFactory(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
		}

		/// <inheritdoc />
		public IODataColumnFilter<TEntity> Create<TEntity>()
		{
			var registeredFilter = ServiceProvider.GetService<IODataColumnFilter<TEntity>>();
			return registeredFilter ?? GenericFilter<TEntity>();
		}

		private IODataColumnFilter<TEntity> GenericFilter<TEntity>()
		{
			var constructor = typeof(GenericODataColumnFilter<>).MakeGenericType(typeof(TEntity)).GetConstructor(new[] { typeof(IServiceProvider) });
			if (constructor == null)
				throw new ArgumentNullException(nameof(constructor));
			return constructor.Invoke(new object[] { ServiceProvider }) as IODataColumnFilter<TEntity>;
		}
	}
}