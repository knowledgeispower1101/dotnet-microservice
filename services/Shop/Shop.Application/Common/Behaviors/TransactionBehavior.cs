// using MediatR;
// using Shop.Application.Common.Interfaces;

// namespace Shop.Application.Common.Behaviors;

// public class TransactionBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork)
//     : IPipelineBehavior<TRequest, TResponse>
//     where TRequest : IRequest<TResponse>
// {
//     private readonly IUnitOfWork _unitOfWork = unitOfWork;

//     public async Task<TResponse> Handle(
//         TRequest request,
//         RequestHandlerDelegate<TResponse> next,
//         CancellationToken cancellationToken)
//     {
//         if (request is not ITransactionalRequest)
//         {
//             return await next();
//         }

//         await _unitOfWork.BeginTransactionAsync(cancellationToken);

//         try
//         {
//             var response = await next();
//             await _unitOfWork.CommitAsync(cancellationToken);
//             return response;
//         }
//         catch
//         {
//             await _unitOfWork.RollbackAsync(cancellationToken);
//             throw;
//         }
//     }
// }
