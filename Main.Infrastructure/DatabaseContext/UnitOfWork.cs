
using Domain.Model;
using Main.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace Main.Infrastructure;

public class UnitOfWork: IUnitOfWork
{
    private readonly IdentityAppDbContext _context;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork (IdentityAppDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync () => _currentTransaction = await _context.Database.BeginTransactionAsync ();
    public async Task CommitAsync ()
    {
        if ( _currentTransaction != null )
        {
            await _currentTransaction.CommitAsync ();
            await _currentTransaction.DisposeAsync ();
        }
    }
    public async Task RollbackAsync ()
    {
        if ( _currentTransaction != null )
        {
            await _currentTransaction.RollbackAsync ();
            await _currentTransaction.DisposeAsync ();
        }
    }
    public async Task<int> SaveChangesAsync () => await _context.SaveChangesAsync ();
    public void Dispose () => _context.Dispose ();
}