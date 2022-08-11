namespace SqlBulkLoader;

using System.Data;

public sealed class BulkDataReader<T> : IDataReader
{
    private readonly IEnumerator<T> source;

    private readonly Func<object?, object?>[] accessors;

    public int FieldCount => accessors.Length;

    public int Depth => throw new NotSupportedException();

    public bool IsClosed => false;

    public int RecordsAffected => -1;

    public object this[int i] => GetValue(i);

    public object this[string name] => throw new NotSupportedException();

    public BulkDataReader(IEnumerable<T> source, Func<object?, object?>[] accessors)
    {
        this.source = source.GetEnumerator();
        this.accessors = accessors;
    }

    public void Dispose()
    {
        source.Dispose();
    }

    public void Close()
    {
    }

    public bool Read() => source.MoveNext();

    public bool NextResult() => throw new NotSupportedException();

    public bool IsDBNull(int i) => throw new NotSupportedException();

    public object GetValue(int i) => accessors[i](source.Current!)!;

    public int GetValues(object[] values)
    {
        for (var i = 0; i < accessors.Length; i++)
        {
            values[i] = GetValue(i);
        }

        return accessors.Length;
    }

    public IDataReader GetData(int i) => throw new NotSupportedException();

    public DataTable GetSchemaTable() => throw new NotSupportedException();

    public string GetDataTypeName(int i) => throw new NotSupportedException();

    public Type GetFieldType(int i) => throw new NotSupportedException();

    public string GetName(int i) => throw new NotSupportedException();

    public int GetOrdinal(string name) => throw new NotSupportedException();

    public bool GetBoolean(int i) => (bool)GetValue(i);

    public byte GetByte(int i) => (byte)GetValue(i);

    public long GetBytes(int i, long fieldOffset, byte[]? buffer, int bufferOffset, int length) => throw new NotSupportedException();

    public char GetChar(int i) => (char)GetValue(i);

    public long GetChars(int i, long fieldOffset, char[]? buffer, int bufferOffset, int length) => throw new NotSupportedException();

    public DateTime GetDateTime(int i) => (DateTime)GetValue(i);

    public decimal GetDecimal(int i) => (decimal)GetValue(i);

    public double GetDouble(int i) => (double)GetValue(i);

    public float GetFloat(int i) => (float)GetValue(i);

    public Guid GetGuid(int i) => (Guid)GetValue(i);

    public short GetInt16(int i) => (short)GetValue(i);

    public int GetInt32(int i) => (int)GetValue(i);

    public long GetInt64(int i) => (long)GetValue(i);

    public string GetString(int i) => (string)GetValue(i);
}
