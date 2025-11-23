using EjercicioGaelZarate.Domain.Entities;
using EjercicioGaelZarate.Domain.Interfaces;
using EjercicioGaelZarate.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EjercicioGaelZarate.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TicketeroDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}