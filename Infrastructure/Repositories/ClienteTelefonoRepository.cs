using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ClienteTelefonoRepository : GenericRepository<ClienteTelefono>, IClienteTelefonoRepository
{
    private readonly VeterinariaContext _context;

    public ClienteTelefonoRepository(VeterinariaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<ClienteTelefono>> GetAllAsync()
    {
        return await _context.ClienteTelefonos.ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<ClienteTelefono> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.ClienteTelefonos as IQueryable<ClienteTelefono>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NumeroTelefono.ToString().ToLower().Contains(search));
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
