using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Servicio : BaseEntity
{
    public string NombreServicio { get; set; }
    public double PrecioServicio { get; set; }
    public ICollection<Cita> Citas { get; set; }

}
