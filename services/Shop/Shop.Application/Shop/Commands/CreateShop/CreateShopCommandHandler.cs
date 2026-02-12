using MediatR;
using Shared.Application.Common.Response;

namespace Shop.Application.Shop.Commands.CreateShop;

public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, BaseResponse<string>>
{
    public async Task<BaseResponse<string>> Handle(
        CreateShopCommand command,
        CancellationToken cancellationToken)
    {
        // Placeholder implementation
        await Task.CompletedTask;
        
        return BaseResponse<string>.Ok(
            $"Shop '{command.Name}' created successfully",
            "Shop created");
    }
}
