using EjercicioGaelZarate.Domain.Entities;
using EjercicioGaelZarate.Domain.Interfaces;
using EjercicioGaelZarate.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EjercicioGaelZarate.Infrastructure.Persistence.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketeroDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(Guid userId) // Corregido a Guid
        {
            return await _context.Tickets
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
    }
}