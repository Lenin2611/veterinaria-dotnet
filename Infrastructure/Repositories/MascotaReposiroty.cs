using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MascotaRepository : GenericRepository<Mascota>, IMascotaRepository
{
    private readonly VeterinariaContext _context;

    public MascotaRepository(VeterinariaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Mascota>> GetAllAsync()
    {
        return await _context.Mascotas
                    .Include(d => d.Citas)
                    .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Mascota> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Mascotas as IQueryable<Mascota>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreMascota.ToLower().Contains(search));
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
