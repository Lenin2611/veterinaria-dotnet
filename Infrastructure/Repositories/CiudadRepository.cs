using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CiudadRepository : GenericRepository<Ciudad>, ICiudadRepository
{
    private readonly VeterinariaContext _context;

    public CiudadRepository(VeterinariaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Ciudad>> GetAllAsync()
    {
        return await _context.Ciudades
                    .Include(c => c.ClienteDirecciones)
                    .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Ciudad> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Ciudades as IQueryable<Ciudad>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreCiudad.ToLower().Contains(search));
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
