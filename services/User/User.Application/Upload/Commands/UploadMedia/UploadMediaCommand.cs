
using MediatR;
using User.Application.Common.Interfaces;
using User.Application.Common.Response;

namespace User.Application.Upload.Commands.UploadMedia;

public record UploadMediaCommand(
    Stream File,
    string FileName,
    string ContentType,
    long Size
) : IRequest<BaseResponse<string>>, ITransactionalRequest;
