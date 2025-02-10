namespace PruebaTecnicaInterminedio.Repository
{
    using Microsoft.EntityFrameworkCore;
    using PruebaTecnicaInterminedio.Data;
    using PruebaTecnicaInterminedio.Models;
    using PruebaTecnicaInterminedio.Models.Dto;
    using PruebaTecnicaInterminedio.Repository.IRepository;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public class UserRepository : IUsersRepository
    {
        //Acceder a la base de datos
        private readonly PruebaJuniorContext _pruebaJuniorContext;

        //Inyectar dependencias
        public UserRepository(PruebaJuniorContext pruebaJuniorContext)
        {
            _pruebaJuniorContext = pruebaJuniorContext;
        }

        public async Task<bool> createUser(Usuario usuario)
        {
            var exist = await _pruebaJuniorContext.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);
            if (exist != null) 
            {
               return false;
            }
            await _pruebaJuniorContext.Usuarios.AddAsync(usuario);
            return await _pruebaJuniorContext.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> DeleteUSer(string emial)
        {
            var exist = await _pruebaJuniorContext.Usuarios.Where(u => u.Email == emial).FirstOrDefaultAsync();
            if (exist == null)
            {
                return false;
            }
            _pruebaJuniorContext.Usuarios.Remove(exist);
            await _pruebaJuniorContext.SaveChangesAsync();
            return true;
        }

        public async Task <List<Usuario>> GetUsuario()
        {
            var list = await _pruebaJuniorContext.Usuarios.ToListAsync();
            if (list == null) 
            {
                list = new List<Usuario>();
            }
            return list;
        }

        public async Task<Usuario> GetUsuarioById(string userEmil)
        {
            var user =  await _pruebaJuniorContext.Usuarios.Where(u => u.Email == userEmil).FirstOrDefaultAsync();
            if (user == null) 
            {
                return new Usuario();
            }

            return user;
        }

        public async Task<bool> UpdateUser(UpdateUserDto updateUserDto)
        {
            var userupdate = await _pruebaJuniorContext.Usuarios.Where(u => u.Email == updateUserDto.OldEmail).FirstOrDefaultAsync();
            if (userupdate == null) 
            {
                return false;
            }

            userupdate.Active = updateUserDto.Active;
            userupdate.Email = updateUserDto.Email;
            userupdate.BurnsDay = updateUserDto.BurnsDay;
            userupdate.NameUser = updateUserDto.NameUser;

            await _pruebaJuniorContext.SaveChangesAsync();
            return true;
        }

        public  bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
