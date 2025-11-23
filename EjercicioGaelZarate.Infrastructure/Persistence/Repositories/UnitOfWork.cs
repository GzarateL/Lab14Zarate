using EjercicioGaelZarate.Domain.Interfaces;
using EjercicioGaelZarate.Infrastructure.Persistence.Context;

namespace EjercicioGaelZarate.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TicketeroDbContext _context;
        public IUserRepository Users { get; }
        public ITicketRepository Tickets { get; }

        public UnitOfWork(TicketeroDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Tickets = new TicketRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}