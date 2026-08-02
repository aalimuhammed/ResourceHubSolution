namespace ResourceHub.Application.Interfaces
{
    public interface IResourceHubInternalService
    {
         Task ImportFromSapAsync(CancellationToken cancellationToken = default);
    }
}