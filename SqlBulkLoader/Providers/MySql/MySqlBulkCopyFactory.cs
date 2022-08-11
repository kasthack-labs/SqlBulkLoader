namespace SqlBulkLoader.Providers.MySql;
internal class MySqlBulkCopyFactory : Interfaces.IBulkCopyFactory
{
    public ValueTask<Interfaces.IBulkCopy> Create(SqlBulkLoaderConfig config, string tableName)
    {
        var connection = new MySqlConnector.MySqlConnection(config.ConnectionString);
        var bulkCopy = new MySqlConnector.MySqlBulkCopy(connection)
        {
            DestinationTableName = tableName,
        };
        return ValueTask.FromResult<Interfaces.IBulkCopy>(new MySqlBulkCopy(connection, bulkCopy));
    }
}
