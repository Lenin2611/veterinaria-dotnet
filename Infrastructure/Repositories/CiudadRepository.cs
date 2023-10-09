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
        return await _context.Ciudades.Include(c => c.ClienteDirecciones).ToListAsync();
    }
}
