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
    private ICitaRepository _Citas;
    private ICiudadRepository _Ciudades;
    private IClienteRepository _Clientes;
    private IClienteDireccionRepository _ClienteDirecciones;
    private IClienteTelefonoRepository _ClienteTelefonos;
    private IDepartamentoRepository _Departamentos;
    private IMascotaRepository _Mascotas;
    private IPaisRepository _Paises;
    private IRazaRepository _Razas;
    private IServicioRepository _Servicios;

    public UnitOfWork(VeterinariaContext context)
    {
        _context = context;
    }

    public ICitaRepository Citas
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

    public ICiudadRepository Ciudades
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

    public IClienteRepository Clientes
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

    public IClienteDireccionRepository ClienteDirecciones
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

    public IClienteTelefonoRepository ClienteTelefonos
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

    public IDepartamentoRepository Departamentos
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

    public IMascotaRepository Mascotas
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

    public IPaisRepository Paises
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

    public IRazaRepository Razas
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

    public IServicioRepository Servicios
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
