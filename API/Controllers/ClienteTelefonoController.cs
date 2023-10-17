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

public class ClienteTelefonoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteTelefonoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteTelefonoDto>>> Get()
    {
        var clienteTelefonos = await _unitOfWork.ClienteTelefonos.GetAllAsync();
        return _mapper.Map<List<ClienteTelefonoDto>>(clienteTelefonos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteTelefonoDto>> Get(int id)
    {
        var clienteTelefono = await _unitOfWork.ClienteTelefonos.GetByIdAsync(id);
        if (clienteTelefono == null)
        {
            return NotFound();
        }
        return _mapper.Map<ClienteTelefonoDto>(clienteTelefono);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteTelefonoDto>> Post(ClienteTelefonoDto clienteTelefonoDto)
    {
        var clienteTelefono = _mapper.Map<ClienteTelefono>(clienteTelefonoDto);
        _unitOfWork.ClienteTelefonos.Add(clienteTelefono);
        await _unitOfWork.SaveAsync();
        if (clienteTelefono == null)
        {
            return BadRequest();
        }
        clienteTelefonoDto.Id = clienteTelefono.Id;
        return CreatedAtAction(nameof(Post), new { id = clienteTelefonoDto.Id }, clienteTelefonoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteTelefonoDto>> Put(int id, [FromBody] ClienteTelefonoDto clienteTelefonoDto)
    {
        if (clienteTelefonoDto.Id == 0)
        {
            clienteTelefonoDto.Id = id;
        }
        if (clienteTelefonoDto.Id != id)
        {
            return NotFound();
        }
        var clienteTelefono = _mapper.Map<ClienteTelefono>(clienteTelefonoDto);
        clienteTelefonoDto.Id = clienteTelefono.Id;
        _unitOfWork.ClienteTelefonos.Update(clienteTelefono);
        await _unitOfWork.SaveAsync();
        return clienteTelefonoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var clienteTelefono = await _unitOfWork.ClienteTelefonos.GetByIdAsync(id);
        if (clienteTelefono == null)
        {
            return NotFound();
        }
        _unitOfWork.ClienteTelefonos.Remove(clienteTelefono);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
