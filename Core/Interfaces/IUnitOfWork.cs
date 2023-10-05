using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IUnitOfWork
{
    public ICita Citas { get; }
    public ICiudad Ciudades { get; }
    public ICliente Clientes { get; }
    public IClienteDireccion ClienteDirecciones { get; }
    public IClienteTelefono ClienteTelefonos { get; }
    public IDepartamento Departamentos { get; }
    public IMascota Mascotas { get; }
    public IPais Paises { get; }
    public IRaza Razas { get; }
    public IServicio Servicios { get; }

    Task<int> SaveAsync();
}
