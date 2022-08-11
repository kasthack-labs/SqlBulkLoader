namespace SqlBulkLoader.Providers.Interfaces;

public interface IBulkCopyFactory
{
    ValueTask<IBulkCopy> Create(SqlBulkLoaderConfig config, string tableName);
}
