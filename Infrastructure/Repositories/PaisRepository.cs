using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaisRepository : GenericRepository<Pais>, IPaisRepository
{
    private readonly VeterinariaContext _context;

    public PaisRepository(VeterinariaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Pais>> GetAllAsync()
    {
        return await _context.Paises
                    .Include(p => p.Departamentos)
                    .ThenInclude(d => d.Ciudades)
                    .ThenInclude(c => c.ClienteDirecciones)
                    .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Pais> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search 
    )
    {
        var query = _context.Paises as IQueryable<Pais>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombrePais.ToLower().Contains(search));
        }
        query = query.OrderBy(p => p.Id);

        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.Departamentos)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
