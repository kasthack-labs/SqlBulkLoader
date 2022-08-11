namespace SqlBulkLoader.Providers.SqlServer;

using Microsoft.Data.SqlClient;

internal class SqlServerBulkCopyFactory : Interfaces.IBulkCopyFactory
{
    public ValueTask<Interfaces.IBulkCopy> Create(SqlBulkLoaderConfig config, string tableName)
    {
        var connection = new SqlConnection(config.ConnectionString);
        var bulkCopy = new SqlBulkCopy(connection)
        {
            DestinationTableName = tableName,
        };
        return ValueTask.FromResult<Interfaces.IBulkCopy>(new SqlServerBulkCopy(connection, bulkCopy));
    }
}
