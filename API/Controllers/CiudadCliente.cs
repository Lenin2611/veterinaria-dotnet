using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CiudadController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public CiudadController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Ciudad>>> Get()
    {
        var ciudad = await _unitOfWork.Ciudades.GetAllAsync();
        return Ok(ciudad);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Ciudad>> Get(int Id)
    {
        var ciudad = await _unitOfWork.Ciudades.GetByIdAsync(Id);
        if (ciudad == null)
        {
            return NotFound();
        }
        return ciudad;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ciudad>> Post(Ciudad ciudad)
    {
        _unitOfWork.Ciudades.Add(ciudad);
        await _unitOfWork.SaveAsync();
        if (ciudad == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = ciudad.Id }, ciudad);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Ciudad>> Put(int id, [FromBody] Ciudad ciudad)
    {
        if (ciudad.Id == 0)
        {
            ciudad.Id = id;
        }
        if (ciudad.Id != id)
        {
            return NotFound();
        }
        _unitOfWork.Ciudades.Update(ciudad);
        await _unitOfWork.SaveAsync();
        return ciudad;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var ciudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
        if (ciudad == null)
        {
            return NotFound();
        }
        _unitOfWork.Ciudades.Remove(ciudad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
