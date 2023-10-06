using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ServicioController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServicioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Servicio>>> Get()
        {
            var servicio = await _unitOfWork.Servicios.GetAllAsync();
            return Ok(servicio);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Servicio>> Get(int Id)
        {
            var servicio = await _unitOfWork.Servicios.GetByIdAsync(Id);
            if (servicio == null)
            {
                return NotFound();
            }
            return servicio;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Servicio>> Post(Servicio servicio)
        {
            _unitOfWork.Servicios.Add(servicio);
            await _unitOfWork.SaveAsync();
            if (servicio == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = servicio.Id }, servicio);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Servicio>> Put(int id, [FromBody] Servicio servicio)
        {
            if (servicio.Id == 0)
            {
                servicio.Id = id;
            }
            if (servicio.Id != id)
            {
                return NotFound();
            }
            _unitOfWork.Servicios.Update(servicio);
            await _unitOfWork.SaveAsync();
            return servicio;
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
}