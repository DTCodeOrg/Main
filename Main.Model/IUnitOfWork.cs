namespace Domain.Model;

public interface IUnitOfWork: IDisposable
{
    Task BeginTransactionAsync ();
    Task CommitAsync ();
    Task RollbackAsync ();
    Task<int> SaveChangesAsync ();
}