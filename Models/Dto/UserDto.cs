namespace PruebaTecnicaInterminedio.Models.Dto
{
    public class UserDto
    {
        public string GuidUser { get; set; } = null!;

        public string NameUser { get; set; } = null!;

        public DateTime? BurnsDay { get; set; }

        public bool? Active { get; set; }

        public string? Email { get; set; }
    }

    public class CreateUserDto
    {
        public string NameUser { get; set; } = null!;

        public DateTime BurnsDay { get; set; }

        public bool Active { get; set; }

        public string Email { get; set; }
    }

    public class UpdateUserDto 
    {
        public string? Email { get; set; }
    }

    public class DeleteUserDto
    {
        public string? Email { get; set; }
    }

    public class Respuesta 
    {
        public string? Names { get; set; }

        public string? Email { get; set; }

        public DateTime? Burnsday { get; set; }

        public int Active { get; set; }
    }
}
