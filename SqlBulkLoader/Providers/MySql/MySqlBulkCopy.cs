namespace SqlBulkLoader.Providers.MySql;

using System.Data;

internal record MySqlBulkCopy(MySqlConnector.MySqlConnection Connection, MySqlConnector.MySqlBulkCopy BulkCopy) : Interfaces.IBulkCopy
{
    public async ValueTask WriteToServerAsync(IDataReader dataReader)
    {
        var open = Connection.State == ConnectionState.Open;
        if (!open)
        {
            await Connection.OpenAsync().ConfigureAwait(false);
        }

        await BulkCopy.WriteToServerAsync(dataReader).ConfigureAwait(false);
        if (!open)
        {
            await Connection.CloseAsync().ConfigureAwait(false);
        }
    }

    public ValueTask DisposeAsync() => Connection.DisposeAsync();
}
