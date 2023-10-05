using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Cita : BaseEntity
{
    public DateTime FechaCita { get; set; }
    public TimeSpan HoraCita { get; set; }
    public int IdClienteFk { get; set; }
    public Cliente Clientes { get; set; }
    public int IdMascotaFk { get; set; }
    public Mascota Mascotas { get; set; }
    public int IdServicioFk { get; set; }
    public Servicio Servicios { get; set; }
}
