using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataAuthorization.Models;

namespace ODataAuthorization.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ODataController : ControllerBase
	{
		public AdventureWorksContext DatabaseContext { get; }

		public ODataController(AdventureWorksContext databaseContext)
		{
			DatabaseContext = databaseContext;
		}

		[HttpGet]
		[EnableQuery]
		[Route("[action]")]
		public IQueryable<Address> Addresses()
		{
			return DatabaseContext.Address;
		}
	}
}