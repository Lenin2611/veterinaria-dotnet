using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Mascota : BaseEntity
{
    public string NombreMascota { get; set; }
    public string EspecieMascota { get; set; }
    public DateOnly FechaNacimientoMascota { get; set; }
    public int IdClienteFk { get; set; }
    public Cliente Clientes { get; set; }
    public int IdRazaFk { get; set; }
    public Raza Razas { get; set; }
    public ICollection<Cita> Citas { get; set; }
}
