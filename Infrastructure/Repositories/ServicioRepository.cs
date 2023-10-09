using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ServicioRepository : GenericRepository<Servicio>, IServicioRepository
{
    private readonly VeterinariaContext _context;

    public ServicioRepository(VeterinariaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Servicio>> GetAllAsync()
    {
        return await _context.Servicios
                    .Include(d => d.Citas)
                    .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Servicio> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Servicios as IQueryable<Servicio>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreServicio.ToLower().Contains(search));
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
