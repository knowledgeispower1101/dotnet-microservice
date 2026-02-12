using MediatR;
using Shared.Application.Common.Response;
using Shop.Application.Upload.Persistence;

namespace Shop.Application.Upload.Commands.UploadMedia;

public class UploadMediaCommandHandler(
    IObjectStorage objectStorage
) : IRequestHandler<UploadMediaCommand, BaseResponse<string>>
{
    private readonly IObjectStorage _objectStorage = objectStorage;
    public async Task<BaseResponse<string>> Handle(UploadMediaCommand command, CancellationToken cancellationToken)
    {
        Console.WriteLine(command.FileName + " " + command.Size);
        return BaseResponse<string>.Ok(
             "Your account has been successfully created",
             "Register success");
    }
}