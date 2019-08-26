namespace ODataAuthorization.OData.Authorization
{
	public class AuthorizationContext : IAuthorizationContext
	{
		/// <inheritdoc />
		public AuthorizationType AuthorizationType { get; private set; }

		public static AuthorizationContext FromType(AuthorizationType type)
		{
			var context = new AuthorizationContext();
			context.AuthorizationType = type;
			return context;
		}

		private AuthorizationContext()
		{
		}
	}
}