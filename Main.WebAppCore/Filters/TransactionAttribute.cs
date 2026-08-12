using Domain.Model;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Main.WebAppCore.Filters;

public class TransactionAttribute: Attribute, IAsyncActionFilter
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionAttribute> _logger;

    public TransactionAttribute (IUnitOfWork unitOfWork,ILogger<TransactionAttribute> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task OnActionExecutionAsync (ActionExecutingContext context,ActionExecutionDelegate next)
    {
        _logger.LogInformation ("Starting database transaction envelope.");
        await _unitOfWork.BeginTransactionAsync ();

        var executedContext = await next();

        if ( executedContext.Exception != null && !executedContext.ExceptionHandled )
        {
            _logger.LogWarning ("Exception detected inside MVC action. Rolling back transaction.");
            await _unitOfWork.RollbackAsync ();
        }
        else
        {
            try
            {
                // Save pending changes from all repositories and commit atomically
                _ = await _unitOfWork.SaveChangesAsync ();
                await _unitOfWork.CommitAsync ();
                _logger.LogInformation ("Transaction successfully saved and committed.");
            }
            catch ( Exception ex )
            {
                _logger.LogError (ex,"Failed to commit transaction. Forcing a rollback.");
                await _unitOfWork.RollbackAsync ();
                throw;
            }
        }
    }
}
