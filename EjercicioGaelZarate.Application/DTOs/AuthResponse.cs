namespace EjercicioGaelZarate.Application.DTOs
{
    public class AuthResponse
    {
        public Guid UserId { get; set; } // Corregido a Guid
        public string Username { get; set; }
        public string Token { get; set; }
    }
}