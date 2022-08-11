namespace SqlBulkLoader;

using System.Collections.Generic;
using System.Threading.Tasks;

// implemented like this to keep compatibility with the previous version
public sealed class SqlBulkLoader : IBulkLoader
{
    private readonly IBulkLoader loader;

    public SqlBulkLoader(SqlBulkLoaderConfig config) => loader = new SqlBulkLoader<Providers.SqlServer.SqlServerBulkCopyFactory>(config);

    public ValueTask LoadAsync<T>(string table, IEnumerable<T> source) => loader.LoadAsync(table, source);
}
