namespace SqlBulkLoader;

using System.Collections.Generic;
using System.Threading.Tasks;

public sealed class MySqlBulkLoader : IBulkLoader
{
    private readonly IBulkLoader loader;

    public MySqlBulkLoader(SqlBulkLoaderConfig config) => loader = new SqlBulkLoader<Providers.MySql.MySqlBulkCopyFactory>(config);

    public ValueTask LoadAsync<T>(string table, IEnumerable<T> source) => loader.LoadAsync(table, source);
}
