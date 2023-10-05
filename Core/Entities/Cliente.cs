using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Cliente : BaseEntity
{
    public string NombreCliente { get; set; }
    public string ApellidoCliente { get; set; }
    public string EmailCliente { get; set; }
    public ICollection<Cita> Citas { get; set; }
    public ICollection<ClienteTelefono> ClienteTelefonos { get; set; }
    public ICollection<Mascota> Mascotas { get; set; }
    public ClienteDireccion ClienteDirecciones { get; set; }
}
