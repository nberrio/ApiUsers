namespace PruebaTecnicaInterminedio.Repository.IRepository
{
    using PruebaTecnicaInterminedio.Models;
    using PruebaTecnicaInterminedio.Models.Dto;

    public interface IUsersRepository
    {
        Task<List<Usuario>> GetUsuario();
        Task<Usuario> GetUsuarioById(string correo);
        Task<bool> UpdateUser(UpdateUserDto updateUserDto);
        Task<bool> createUser(Usuario usuario);
        Task<bool> DeleteUSer(string emial);
        bool IsValidEmail(string email);
    }
}
