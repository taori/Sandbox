namespace ODataAuthorization.GenericODataRouting.Authorization
{
	public class ODataAuthorizationContext : IODataAuthorizationContext
	{
		/// <inheritdoc />
		public ODataAuthorizationType AuthorizationType { get; private set; }

		public static ODataAuthorizationContext FromType(ODataAuthorizationType type)
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