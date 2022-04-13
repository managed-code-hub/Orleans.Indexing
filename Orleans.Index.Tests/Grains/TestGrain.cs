using System.Threading.Tasks;
using Orleans.Index.Abstractions;
using Orleans.Index.Annotations;

namespace Orleans.Index.Tests.Grains;

public class MyClass
{
    [Index] public int Test { get; set; }

    [Index] public string TestString { get; set; }
}

public class TestGrain : IndexGrain, ITestGrain
{
    public TestGrain(IIndexService indexService) : base(indexService)
    {
        Class = new MyClass {Test = 3, TestString = "Test"};
    }

    [Index] public MyClass Class { get; set; }

    public override async Task OnActivateAsync()
    {
        Class = new MyClass {Test = 3, TestString = "Test"};

        await WriteIndexAsync();
    }
}