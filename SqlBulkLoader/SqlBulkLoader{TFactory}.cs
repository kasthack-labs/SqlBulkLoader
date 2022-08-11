namespace SqlBulkLoader;

using System.Collections.Concurrent;
using System.Data;

using Smart.Linq;
using Smart.Reflection;

public class SqlBulkLoader<TFactory>
        : IBulkLoader
    where TFactory : Providers.Interfaces.IBulkCopyFactory, new()
{
    private readonly ConcurrentDictionary<Type, Func<object?, object?>[]> accessorCache = new();

    private readonly SqlBulkLoaderConfig config;
    private readonly Providers.Interfaces.IBulkCopyFactory factory;

    public SqlBulkLoader(SqlBulkLoaderConfig config)
    {
        this.config = config;
        factory = new TFactory();
    }

    public async ValueTask LoadAsync<T>(string table, IEnumerable<T> source)
    {
        var accessors = accessorCache.GetOrAdd(typeof(T), CreateAccessors);
        using var reader = new BulkDataReader<T>(source, accessors);
        await using var loader = await factory.Create(config, table);
        await loader.WriteToServerAsync(reader).ConfigureAwait(false);
    }

    private Func<object?, object?>[] CreateAccessors(Type type)
    {
        return config.PropertySelector(type)
            .Select(x => DelegateFactory.Default.CreateGetter(x))
            .ExcludeNull()
            .ToArray();
    }
}
