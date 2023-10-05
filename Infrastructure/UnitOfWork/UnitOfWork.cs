using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly VeterinariaContext _context;
    private ICita _Citas;
    private ICiudad _Ciudades;
    private ICliente _Clientes;
    private IClienteDireccion _ClienteDirecciones;
    private IClienteTelefono _ClienteTelefonos;
    private IDepartamento _Departamentos;
    private IMascota _Mascotas;
    private IPais _Paises;
    private IRaza _Razas;
    private IServicio _Servicios;

    public UnitOfWork(VeterinariaContext context)
    {
        _context = context;
    }

    public ICita Citas
    {
        get
        {
            if (_Citas == null)
            {
                _Citas = new CitaRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Citas;
        }
    }

    public ICiudad Ciudades
    {
        get
        {
            if (_Ciudades == null)
            {
                _Ciudades = new CiudadRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Ciudades;
        }
    }

    public ICliente Clientes
    {
        get
        {
            if (_Clientes == null)
            {
                _Clientes = new ClienteRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Clientes;
        }
    }

    public IClienteDireccion ClienteDirecciones
    {
        get
        {
            if (_ClienteDirecciones == null)
            {
                _ClienteDirecciones = new ClienteDireccionRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _ClienteDirecciones;
        }
    }

    public IClienteTelefono ClienteTelefonos
    {
        get
        {
            if (_ClienteTelefonos == null)
            {
                _ClienteTelefonos = new ClienteTelefonoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _ClienteTelefonos;
        }
    }

    public IDepartamento Departamentos
    {
        get
        {
            if (_Departamentos == null)
            {
                _Departamentos = new DepartamentoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Departamentos;
        }
    }

    public IMascota Mascotas
    {
        get
        {
            if (_Mascotas == null)
            {
                _Mascotas = new MascotaRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Mascotas;
        }
    }

    public IPais Paises
    {
        get
        {
            if (_Paises == null)
            {
                _Paises = new PaisRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Paises;
        }
    }

    public IRaza Razas
    {
        get
        {
            if (_Razas == null)
            {
                _Razas = new RazaRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Razas;
        }
    }

    public IServicio Servicios
    {
        get
        {
            if (_Servicios == null)
            {
                _Servicios = new ServicioRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _Servicios;
        }
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
