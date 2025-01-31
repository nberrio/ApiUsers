namespace PruebaTecnicaInterminedio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public partial class Usuario
{
    [Key]
    public int? Id { get; set; }

    public string GuidUser { get; set; } = null!;

    public string NameUser { get; set; } = null!;

    public DateTime? BurnsDay { get; set; }

    public bool? Active { get; set; }

    public string? Email { get; set; }
}
