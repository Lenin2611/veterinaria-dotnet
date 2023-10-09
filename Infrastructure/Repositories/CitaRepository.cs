using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CitaRepository : GenericRepository<Cita>, ICitaRepository
{
    private readonly VeterinariaContext _context;

    public CitaRepository(VeterinariaContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Cita>> GetAllAsync()
    {
        return await _context.Citas.ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Cita> registros)> GetAllAsync( //Sobrecarga de metodos
        int pageIndex, //Cual pagina necesitamos ver
        int pageSize, //Cantidad de registros a visualizar por pagina
        string search // Pasar algun critetio de busqueda
    )
    {
        var query = _context.Citas as IQueryable<Cita>; //Consulta para obtener todos los registros en este caso Citaes
        if (!string.IsNullOrEmpty(search)) //nos permite referenciar si esta variable es nula 
        {
            query = query.Where(p => p.FechaCita.Equals(search));
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
