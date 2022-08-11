namespace SqlBulkLoader.Providers.SqlServer;

using System.Data;

using global::SqlBulkLoader.Providers.Interfaces;

using Microsoft.Data.SqlClient;

internal record SqlServerBulkCopy(SqlConnection Connection, SqlBulkCopy BulkCopy) : IBulkCopy
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
            await Connection.CloseAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        ((IDisposable)BulkCopy).Dispose();
        await Connection.DisposeAsync().ConfigureAwait(false);
    }
}
