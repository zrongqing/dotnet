namespace App.Common.Services;

public interface IAppServer
{
    public SynchronizationContext SynchronizationContext { get; }
}