namespace PruebaTecnicaInterminedio.Repository.IRepository
{
    using PruebaTecnicaInterminedio.Models;
    using PruebaTecnicaInterminedio.Models.Dto;

    public interface IUsersRepository
    {
        Task<List<Usuario>> GetUsuario();
        Task<Usuario> GetUsuarioById(Usuario UsuarioId);
        Task<bool> UpdateUser(Usuario usuario);
        Task<bool> createUser(Usuario usuario);
        Task<bool> DeleteUSer(string emial);
        bool IsValidEmail(string email);
    }
}
