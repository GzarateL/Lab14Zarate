namespace EjercicioGaelZarate.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITicketRepository Tickets { get; }
        // Aquí agregarías IRoleRepository, IResponseRepository, etc.

        Task<int> CompleteAsync();
    }
}