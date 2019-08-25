namespace ODataAuthorization.GenericODataRouting.Authorization
{
	public class ODataAuthorizationContext : IODataAuthorizationContext
	{
		/// <inheritdoc />
		public AuthorizationType AuthorizationType { get; private set; }

		public static ODataAuthorizationContext FromType(AuthorizationType type)
		{
			var context = new ODataAuthorizationContext();
			context.AuthorizationType = type;
			return context;
		}

		private ODataAuthorizationContext()
		{
		}
	}
}