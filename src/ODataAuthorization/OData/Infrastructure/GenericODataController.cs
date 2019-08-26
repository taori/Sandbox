using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ODataAuthorization.Models;
using ODataAuthorization.OData.Authorization;
using ODataAuthorization.OData.ColumnFilter;

namespace ODataAuthorization.OData.Infrastructure
{
	[EnableQuery]
	[GenericODataControllerNamingConvention]
	[Produces("application/json")]
	[Route("api/odata/[controller]")]
	public class GenericODataController<TEntity> : ODataController
		where TEntity : class
	{
		public AdventureWorksContext Context { get; }

		public IAuthorizer Authorizer { get; }

		public IColumnSelector ColumnSelector { get; }

		public GenericODataController(AdventureWorksContext context, IAuthorizer authorizer, IColumnSelector columnSelector)
		{
			Context = context;
			Authorizer = authorizer;
			ColumnSelector = columnSelector;
		}

		[Route("[action]")]
		public ActionResult<IQueryable<TEntity>> List()
		{
			var items = Context.Set<TEntity>().AsQueryable();
			if (!items.Any())
				return NotFound(default);

			var authorized = Authorizer.Authorize(items, AuthorizationContext.FromType(AuthorizationType.List));
			if (!authorized.Any())
				return Unauthorized();
			
			return Ok(ColumnSelector.FromMultiple(authorized));
		}

		[Route("{id:int}")]
		public async Task<ActionResult<object>> Get(int id)
		{
			var entity = await Context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
			if (entity == null)
				return NotFound(default);

			if(Authorizer.Authorize(entity, AuthorizationContext.FromType(AuthorizationType.Get)) == default)
				return Unauthorized();

			return Ok(ColumnSelector.FromSingle(entity));
		}

		[Route("{id:guid}")]
		public async Task<ActionResult<object>> Get(Guid id)
		{
			var entity = await Context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
			if (entity == null)
				return NotFound(default);

			if(Authorizer.Authorize(entity, AuthorizationContext.FromType(AuthorizationType.Get)) == default)
				return Unauthorized();

			return Ok(ColumnSelector.FromSingle(entity));
		}

		[Route("{id:guid}")]
		[HttpDelete]
		public async Task<ActionResult<object>> Delete(Guid id)
		{
#warning this method is not tested yet
			var entity = await Context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
			if (entity == null)
				return NotFound(default);

			if (Authorizer.Authorize(entity, AuthorizationContext.FromType(AuthorizationType.Delete)) == default)
				return Unauthorized();

			Context.Set<TEntity>().Remove(entity);
			var result = await Context.SaveChangesAsync().ConfigureAwait(false);
			if (result > 1)
				return Ok(id);

			return StatusCode((int)HttpStatusCode.NotModified);
		}

		[Route("{id:int}")]
		[HttpDelete]
		public async Task<ActionResult<object>> Delete(int id)
		{
#warning this method is not tested yet
			var entity = await Context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
			if (entity == null)
				return NotFound(default);

			if(Authorizer.Authorize(entity, AuthorizationContext.FromType(AuthorizationType.Delete)) == default)
				return Unauthorized();

			Context.Set<TEntity>().Remove(entity);
			var result = await Context.SaveChangesAsync().ConfigureAwait(false);
			if (result > 1)
				return Ok(id);

			return StatusCode((int)HttpStatusCode.NotModified);
		}

		[Route("{id:guid}")]
		[HttpPut]
		public async Task<ActionResult<object>> Update(Guid id, [FromBody] TEntity item)
		{
#warning this method is not tested yet
			var entity = await Context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
			if (entity == null)
				return NotFound();

			if (Authorizer.Authorize(entity, AuthorizationContext.FromType(AuthorizationType.Update)) == default)
				return Unauthorized();

			Context.Set<TEntity>().Update(item);
			var result = await Context.SaveChangesAsync().ConfigureAwait(false);
			if (result > 1)
				return Ok(id);

			return StatusCode((int)HttpStatusCode.NotModified);
		}

		[Route("{id:int}")]
		[HttpPut]
		public async Task<ActionResult<object>> Update(int id, [FromBody] TEntity item)
		{
#warning this method is not tested yet
			var entity = await Context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
			if (entity == null)
				return NotFound();

			if (Authorizer.Authorize(entity, AuthorizationContext.FromType(AuthorizationType.Update)) == default)
				return Unauthorized();

			Context.Set<TEntity>().Update(item);
			var result = await Context.SaveChangesAsync().ConfigureAwait(false);
			if (result > 1)
				return Ok(id);

			return StatusCode((int) HttpStatusCode.NotModified);
		}

		[HttpPost]
		public async Task<ActionResult<object>> Insert([FromBody] TEntity item)
		{
#warning this method is not tested yet
			Context.Set<TEntity>().Add(item);

			if (Authorizer.Authorize(item, AuthorizationContext.FromType(AuthorizationType.Insert)) == default)
				return Unauthorized();

			var linkGenerator = this.HttpContext.RequestServices.GetService<LinkGenerator>();

			var result = await Context.SaveChangesAsync().ConfigureAwait(false);
			if (result == 0)
				return this.StatusCode((int)HttpStatusCode.NotModified);
			if (result == 1)
				return Created(linkGenerator.GetUriByAction(HttpContext, nameof(Get), $"{typeof(TEntity).Name}"), null);

			return this.BadRequest();
		}
	}
}