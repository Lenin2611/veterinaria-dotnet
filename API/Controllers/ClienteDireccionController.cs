using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClienteDireccionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteDireccionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteDireccion>>> Get()
        {
            var ClienteDireccion = await _unitOfWork.ClienteDirecciones.GetAllAsync();
            return Ok(ClienteDireccion);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDireccion>> Get(int Id)
        {
            var clienteDireccion = await _unitOfWork.ClienteDirecciones.GetByIdAsync(Id);
            if (clienteDireccion == null)
            {
                return NotFound();
            }
            return clienteDireccion;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDireccion>> Post(ClienteDireccion clienteDireccion)
        {
            _unitOfWork.ClienteDirecciones.Add(clienteDireccion);
            await _unitOfWork.SaveAsync();
            if (clienteDireccion == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = clienteDireccion.Id }, clienteDireccion);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDireccion>> Put(int id, [FromBody] ClienteDireccion clienteDireccion)
        {
            if (clienteDireccion.Id == 0)
            {
                clienteDireccion.Id = id;
            }
            if (clienteDireccion.Id != id)
            {
                return NotFound();
            }
            _unitOfWork.ClienteDirecciones.Update(clienteDireccion);
            await _unitOfWork.SaveAsync();
            return clienteDireccion;
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
}