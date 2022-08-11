namespace SqlBulkLoader.Providers.Interfaces;

public interface IBulkCopy : IAsyncDisposable
{
    ValueTask WriteToServerAsync(System.Data.IDataReader dataReader);
}
