using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RazaRepository : GenericRepository<Raza>, IRazaRepository
{
    private readonly VeterinariaContext _context;

    public RazaRepository(VeterinariaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Raza>> GetAllAsync()
    {
        return await _context.Razas
                    .Include(d => d.Mascotas)
                    .ThenInclude(m => m.Citas)
                    .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Raza> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Razas as IQueryable<Raza>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreRaza.ToLower().Contains(search));
        }
        query = query.OrderBy(p => p.Id);

        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
