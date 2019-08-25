namespace ODataAuthorization.GenericODataRouting.ColumnFiltering
{
	/// <summary>
	/// Creates column filters to limit the amount of data a user can obtain
	/// </summary>
	public interface IODataColumnFilterFactory
	{
		IODataColumnFilter<TEntity> Create<TEntity>();
	}
}