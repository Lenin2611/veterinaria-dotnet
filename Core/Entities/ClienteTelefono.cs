using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class ClienteTelefono : BaseEntity
{
    public int NumeroTelefono { get; set; }
    public int IdClienteFk { get; set; }
    public Cliente Clientes { get; set; }
}
