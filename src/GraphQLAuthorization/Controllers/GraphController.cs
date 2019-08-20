using System;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLAuthorization.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GraphController : ControllerBase
	{
		private readonly Schema graphQLSchema;

		public GraphController(Schema schema)
		{
			graphQLSchema = schema;
		}

		[HttpGet]
		[Route("get/{query}")]
		public async Task<string> Get(string query)
		{
			var result = await new DocumentExecuter().ExecuteAsync(
				new ExecutionOptions()
				{
					Schema = graphQLSchema,
					Query = query
				}
			).ConfigureAwait(false);

			if (result.Errors?.Count > 0)
			{
				return string.Join("<br/>", result.Errors.Select(d => d.ToString()));
			}

			return new DocumentWriter(true).Write(result);
		}
	}
}