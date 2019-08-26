namespace ODataAuthorization.OData.Authorization
{
	public interface IAuthorizationContext
	{
		AuthorizationType AuthorizationType { get; }
	}
}