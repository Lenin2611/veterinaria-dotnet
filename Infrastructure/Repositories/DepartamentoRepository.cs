using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamentoRepository
{
    private readonly VeterinariaContext _context;

    public DepartamentoRepository(VeterinariaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Departamento>> GetAllAsync()
    {
        return await _context.Departamentos
                    .Include(d => d.Ciudades)
                    .ThenInclude(c => c.ClienteDirecciones)
                    .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Departamento> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Departamentos as IQueryable<Departamento>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreDepartamento.ToLower().Contains(search));
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
