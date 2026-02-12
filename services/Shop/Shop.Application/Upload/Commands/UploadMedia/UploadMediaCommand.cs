
using MediatR;
using Shared.Application.Common.Response;

namespace Shop.Application.Upload.Commands.UploadMedia;

public record UploadMediaCommand(
    Stream File,
    string FileName,
    string ContentType,
    long Size
) : IRequest<BaseResponse<string>>;
