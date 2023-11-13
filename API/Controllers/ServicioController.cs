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

public class ServicioController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServicioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ServicioDto>>> Get()
    {
        var servicio = await _unitOfWork.Servicios.GetAllAsync();
        return _mapper.Map<List<ServicioDto>>(servicio);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServicioDto>> Get(int id)
    {
        var servicio = await _unitOfWork.Servicios.GetByIdAsync(id);
        if (servicio == null)
        {
            return NotFound();
        }
        return _mapper.Map<ServicioDto>(servicio);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServicioDto>> Post(ServicioDto servicioDto)
    {
        var servicio = _mapper.Map<Servicio>(servicioDto);
        _unitOfWork.Servicios.Add(servicio);
        await _unitOfWork.SaveAsync();
        if (servicio == null)
        {
            return BadRequest();
        }
        servicioDto.Id = servicio.Id;
        return CreatedAtAction(nameof(Post), new { id = servicioDto.Id }, servicioDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServicioDto>> Put(int id, [FromBody] ServicioDto resultDto)
    {
        var exists = await _unitOfWork.Servicios.GetByIdAsync(id);
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
        // The context is already tracking result, so no need to attach it
        await _unitOfWork.SaveAsync();
        // Return the updated entity
        return _mapper.Map<ServicioDto>(exists);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var servicio = await _unitOfWork.Servicios.GetByIdAsync(id);
        if (servicio == null)
        {
            return NotFound();
        }
        _unitOfWork.Servicios.Remove(servicio);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
