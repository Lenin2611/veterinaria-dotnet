using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class ClienteTelefonoDto
{
    public int Id { get; set; }
    public int NumeroTelefono { get; set; }
    public string IdClienteFk { get; set; }
}
