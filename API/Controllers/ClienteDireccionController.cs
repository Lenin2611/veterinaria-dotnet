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

public class ClienteDireccionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteDireccionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteDireccionDto>>> Get()
    {
        var clienteDireccion = await _unitOfWork.ClienteDirecciones.GetAllAsync();
        return _mapper.Map<List<ClienteDireccionDto>>(clienteDireccion);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDireccionDto>> Get(int id)
    {
        var clienteDireccion = await _unitOfWork.ClienteDirecciones.GetByIdAsync(id);
        if (clienteDireccion == null)
        {
            return NotFound();
        }
        return _mapper.Map<ClienteDireccionDto>(clienteDireccion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDireccionDto>> Post(ClienteDireccionDto clienteDireccionDto)
    {
        var clienteDireccion = _mapper.Map<ClienteDireccion>(clienteDireccionDto);
        _unitOfWork.ClienteDirecciones.Add(clienteDireccion);
        await _unitOfWork.SaveAsync();
        if (clienteDireccion == null)
        {
            return BadRequest();
        }
        clienteDireccionDto.Id = clienteDireccion.Id;
        return CreatedAtAction(nameof(Post), new { id = clienteDireccionDto.Id }, clienteDireccionDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDireccionDto>> Put(int id, [FromBody] ClienteDireccionDto clienteDireccionDto)
    {
        if (clienteDireccionDto.Id == 0)
        {
            clienteDireccionDto.Id = id;
        }
        if (clienteDireccionDto.Id != id)
        {
            return NotFound();
        }
        var clienteDireccion = _mapper.Map<ClienteDireccion>(clienteDireccionDto);
        clienteDireccionDto.Id = clienteDireccion.Id;
        _unitOfWork.ClienteDirecciones.Update(clienteDireccion);
        await _unitOfWork.SaveAsync();
        return clienteDireccionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var clienteDireccion = await _unitOfWork.ClienteDirecciones.GetByIdAsync(id);
        if (clienteDireccion == null)
        {
            return NotFound();
        }
        _unitOfWork.ClienteDirecciones.Remove(clienteDireccion);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
