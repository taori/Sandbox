using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataAuthorization.Models;

namespace ODataAuthorization.GenericODataRouting
{
	[EnableQuery]
	[GenericODataControllerNamingConvention]
	[Produces("application/json")]
	[Route("api/odata/[controller]")]
	public class GenericODataController<TEntity> : ODataController
		where TEntity : class
	{
		public AdventureWorksContext Context { get; }

		public GenericODataController(AdventureWorksContext context)
		{
			Context = context;
		}

		public IQueryable<TEntity> All() => Context.Set<TEntity>().AsQueryable();

		[Route("{id}")]
		public Task<TEntity> Id(int id) => Context.Set<TEntity>().FindAsync(id);
	}
}