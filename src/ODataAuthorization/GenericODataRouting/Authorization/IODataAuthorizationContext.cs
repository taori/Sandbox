namespace ODataAuthorization.GenericODataRouting.Authorization
{
	public interface IODataAuthorizationContext
	{
		ODataAuthorizationType AuthorizationType { get; }
	}
}