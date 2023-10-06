using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RazaController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RazaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Raza>>> Get()
        {
            var raza = await _unitOfWork.Razas.GetAllAsync();
            return Ok(raza);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Raza>> Get(int Id)
        {
            var raza = await _unitOfWork.Razas.GetByIdAsync(Id);
            if (raza == null)
            {
                return NotFound();
            }
            return raza;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Raza>> Post(Raza raza)
        {
            _unitOfWork.Razas.Add(raza);
            await _unitOfWork.SaveAsync();
            if (raza == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = raza.Id }, raza);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Raza>> Put(int id, [FromBody] Raza raza)
        {
            if (raza.Id == 0)
            {
                raza.Id = id;
            }
            if (raza.Id != id)
            {
                return NotFound();
            }
            _unitOfWork.Razas.Update(raza);
            await _unitOfWork.SaveAsync();
            return raza;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var raza = await _unitOfWork.Razas.GetByIdAsync(id);
            if (raza == null)
            {
                return NotFound();
            }
            _unitOfWork.Razas.Remove(raza);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}