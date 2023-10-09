using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class CitaDto
{
    public int Id { get; set; }
    public DateTime FechaCita { get; set; }
    public TimeSpan HoraCita { get; set; }
    public string NombreCliente { get; set; }
    public string NombreMascota { get; set; }
    public string NombreServicio { get; set; }
}
