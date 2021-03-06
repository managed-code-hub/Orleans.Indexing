using ManagedCode.Storage.Azure;
using ManagedCode.Storage.Azure.Options;
using ManagedCode.Storage.Core;
using Orleans.Indexing.Lucene.Services;

namespace Orleans.Indexing.Tests.Cluster.Fakes;

public class FakeServices
{
    public static LuceneWithStorageIndexService FakeLuceneWithStorageIndexService { get; }
    public static IStorage FakeStorage { get; }

    static FakeServices()
    {
        AzureStorageOptions options = new()
        {
            ConnectionString =
                "",
            Container = "lucene",
        };

        FakeStorage = new AzureStorage(options);
        FakeLuceneWithStorageIndexService = new LuceneWithStorageIndexService(FakeStorage);
    }
}