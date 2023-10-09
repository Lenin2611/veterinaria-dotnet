using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    private readonly VeterinariaContext _context;

    public ClienteRepository(VeterinariaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes
                    .Include(c => c.ClienteDirecciones)
                    .Include(c => c.Citas)
                    .Include(c => c.ClienteTelefonos)
                    .Include(c => c.Mascotas)
                    .ThenInclude(m => m.Citas)
                    .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Cliente> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Clientes as IQueryable<Cliente>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreCliente.ToLower().Contains(search));
        }
        query = query.OrderBy(p => p.Id);

        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.ClienteDirecciones)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
