using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MascotaController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public MascotaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Mascota>>> Get()
        {
            var mascota = await _unitOfWork.Mascotas.GetAllAsync();
            return Ok(mascota);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Mascota>> Get(int Id)
        {
            var mascota = await _unitOfWork.Mascotas.GetByIdAsync(Id);
            if (mascota == null)
            {
                return NotFound();
            }
            return mascota;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Mascota>> Post(Mascota mascota)
        {
            _unitOfWork.Mascotas.Add(mascota);
            await _unitOfWork.SaveAsync();
            if (mascota == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = mascota.Id }, mascota);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Mascota>> Put(int id, [FromBody] Mascota mascota)
        {
            if (mascota.Id == 0)
            {
                mascota.Id = id;
            }
            if (mascota.Id != id)
            {
                return NotFound();
            }
            _unitOfWork.Mascotas.Update(mascota);
            await _unitOfWork.SaveAsync();
            return mascota;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var mascota = await _unitOfWork.Mascotas.GetByIdAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }
            _unitOfWork.Mascotas.Remove(mascota);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}