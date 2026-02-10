
using MediatR;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Application.Common.Response;

namespace ShoppeeClone.Application.Upload.Commands.UploadMedia;

public record UploadMediaCommand(
    Stream File,
    string FileName,
    string ContentType,
    long Size
) : IRequest<BaseResponse<string>>, ITransactionalRequest;
