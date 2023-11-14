using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CitaController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CitaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CitaDto>>> Get()
    {
        var citas = await _unitOfWork.Citas.GetAllAsync();
        return _mapper.Map<List<CitaDto>>(citas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CitaDto>> Get(int id)
    {
        var cita = await _unitOfWork.Citas.GetByIdAsync(id);
        if (cita == null)
        {
            return NotFound();
        }
        return _mapper.Map<CitaDto>(cita);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CitaDto>> Post(CitaDto citaDto)
    {
        var cita = _mapper.Map<Cita>(citaDto);
        if (citaDto.FechaCita == DateOnly.MinValue)
        {
            citaDto.FechaCita = DateOnly.FromDateTime(DateTime.Now);
            cita.FechaCita = DateOnly.FromDateTime(DateTime.Now);
        }
        if (citaDto.HoraCita == TimeOnly.MinValue)
        {
            citaDto.HoraCita = TimeOnly.FromDateTime(DateTime.Now);
            cita.HoraCita = TimeOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Citas.Add(cita);
        await _unitOfWork.SaveAsync();
        if (cita == null)
        {
            return BadRequest();
        }
        citaDto.Id = cita.Id;
        return CreatedAtAction(nameof(Post), new { id = citaDto.Id }, citaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CitaDto>> Put(int id, [FromBody] CitaDto resultDto)
    {
        var exists = await _unitOfWork.Citas.GetByIdAsync(id);
        if (exists == null)
        {
            return NotFound();
        }
        if (resultDto.Id == 0)
        {
            resultDto.Id = id;
        }
        if (resultDto.Id != id)
        {
            return BadRequest();
        }
        // Update the properties of the existing entity with values from resultDto
        _mapper.Map(resultDto, exists);
        if (resultDto.FechaCita == DateOnly.MinValue)
        {
            exists.FechaCita = DateOnly.FromDateTime(DateTime.Now);
        }
        if (resultDto.HoraCita == TimeOnly.MinValue)
        {
            exists.HoraCita = TimeOnly.FromDateTime(DateTime.Now);
        }
        // The context is already tracking result, so no need to attach it
        await _unitOfWork.SaveAsync();
        // Return the updated entity
        return _mapper.Map<CitaDto>(exists);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var cita = await _unitOfWork.Citas.GetByIdAsync(id);
        if (cita == null)
        {
            return NotFound();
        }
        _unitOfWork.Citas.Remove(cita);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}