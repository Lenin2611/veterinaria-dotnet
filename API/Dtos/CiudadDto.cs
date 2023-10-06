using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class CiudadDto
{
    public int Id { get; set; }
    public string NombreCiudad { get; set; }
    public string NombreDepartamento { get; set; }
    public string NombrePais { get; set; }
}
