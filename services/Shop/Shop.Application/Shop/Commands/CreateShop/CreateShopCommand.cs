using MediatR;
using Shared.Application.Common.Response;

namespace Shop.Application.Shop.Commands.CreateShop;

public record CreateShopCommand(
    string Name,
    string Description
) : IRequest<BaseResponse<string>>;
