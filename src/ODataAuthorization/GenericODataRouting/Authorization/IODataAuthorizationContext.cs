namespace ODataAuthorization.GenericODataRouting.Authorization
{
	public interface IODataAuthorizationContext
	{
		AuthorizationType AuthorizationType { get; }
	}
}