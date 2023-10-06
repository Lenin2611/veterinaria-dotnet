using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaisController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaisController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Pais>>> Get()
        {
            var pais = await _unitOfWork.Paises.GetAllAsync();
            return Ok(pais);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pais>> Get(int Id)
        {
            var pais = await _unitOfWork.Paises.GetByIdAsync(Id);
            if (pais == null)
            {
                return NotFound();
            }
            return pais;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pais>> Post(Pais pais)
        {
            _unitOfWork.Paises.Add(pais);
            await _unitOfWork.SaveAsync();
            if (pais == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = pais.Id }, pais);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pais>> Put(int id, [FromBody] Pais pais)
        {
            if (pais.Id == 0)
            {
                pais.Id = id;
            }
            if (pais.Id != id)
            {
                return NotFound();
            }
            _unitOfWork.Paises.Update(pais);
            await _unitOfWork.SaveAsync();
            return pais;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var pais = await _unitOfWork.Paises.GetByIdAsync(id);
            if (pais == null)
            {
                return NotFound();
            }
            _unitOfWork.Paises.Remove(pais);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}