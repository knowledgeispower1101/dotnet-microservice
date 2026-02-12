namespace Shop.Application.Upload.Persistence;

public interface IObjectStorage
{
    Task UploadAsync(
        string bucket,
        string objectName,
        Stream data,
        string contentType,
        CancellationToken cancellationToken = default
    );

    Task<Stream> GetAsync(
        string bucket,
        string objectName,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(
        string bucket,
        string objectName,
        CancellationToken cancellationToken = default
    );
}
