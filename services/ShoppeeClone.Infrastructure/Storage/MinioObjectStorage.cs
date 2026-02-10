
using Minio;
using Minio.DataModel.Args;
using ShoppeeClone.Application.Upload.Persistence;

namespace ShoppeeClone.Infrastructure.Storage;

public sealed class MinioObjectStorage(IMinioClient minio) : IObjectStorage
{
    private readonly IMinioClient _minio = minio;

    public async Task UploadAsync(
        string bucket,
        string objectName,
        Stream data,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        await EnsureBucketExists(bucket, cancellationToken);

        var args = new PutObjectArgs()
            .WithBucket(bucket)
            .WithObject(objectName)
            .WithStreamData(data)
            .WithObjectSize(data.Length)
            .WithContentType(contentType);

        await _minio.PutObjectAsync(args, cancellationToken);
    }

    public async Task<Stream> GetAsync(
        string bucket,
        string objectName,
        CancellationToken cancellationToken = default)
    {
        var memory = new MemoryStream();

        var args = new GetObjectArgs()
            .WithBucket(bucket)
            .WithObject(objectName)
            .WithCallbackStream(stream => stream.CopyTo(memory));

        await _minio.GetObjectAsync(args, cancellationToken);

        memory.Position = 0;
        return memory;
    }

    public async Task DeleteAsync(
        string bucket,
        string objectName,
        CancellationToken cancellationToken = default)
    {
        var args = new RemoveObjectArgs()
            .WithBucket(bucket)
            .WithObject(objectName);

        await _minio.RemoveObjectAsync(args, cancellationToken);
    }

    private async Task EnsureBucketExists(
        string bucket,
        CancellationToken cancellationToken)
    {
        var exists = await _minio.BucketExistsAsync(
            new BucketExistsArgs().WithBucket(bucket),
            cancellationToken
        );

        if (!exists)
        {
            await _minio.MakeBucketAsync(
                new MakeBucketArgs().WithBucket(bucket),
                cancellationToken
            );
        }
    }
}
