using System;
using System.Threading.Tasks;

namespace Orleans.Indexing.Tests.Cluster.Fakes;

/// <summary>
///     Implements a fake timer entry to facilitate unit testing.
/// </summary>
public class FakeTimerEntry : IDisposable
{
    private readonly FakeTimerRegistry owner;
    private readonly TaskScheduler scheduler;

    public FakeTimerEntry(FakeTimerRegistry owner,
        TaskScheduler scheduler,
        Grain grain,
        Func<object, Task> asyncCallback,
        object state,
        TimeSpan dueTime,
        TimeSpan period)
    {
        this.scheduler = scheduler;
        this.owner = owner;

        Grain = grain;
        AsyncCallback = asyncCallback;
        State = state;
        DueTime = dueTime;
        DuePeriod = period;
    }

    public Grain Grain { get; }
    public Func<object, Task> AsyncCallback { get; }
    public object State { get; }
    public TimeSpan DueTime { get; }
    public TimeSpan DuePeriod { get; }

    public void Dispose()
    {
        try
        {
            owner.Remove(this);
        }
        catch (Exception)
        {
            // noop
        }
    }

    /// <summary>
    ///     Ticks the timer action within the activation context.
    /// </summary>
    public async Task TickAsync()
    {
        await await Task.Factory.StartNew(AsyncCallback, State, default, TaskCreationOptions.None, scheduler);
    }
}