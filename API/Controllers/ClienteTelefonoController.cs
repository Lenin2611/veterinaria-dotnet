using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClienteTelefonoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteTelefonoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteTelefono>>> Get()
        {
            var ClienteTelefono = await _unitOfWork.ClienteTelefonos.GetAllAsync();
            return Ok(ClienteTelefono);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteTelefono>> Get(int Id)
        {
            var clienteTelefono = await _unitOfWork.ClienteTelefonos.GetByIdAsync(Id);
            if (clienteTelefono == null)
            {
                return NotFound();
            }
            return clienteTelefono;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteTelefono>> Post(ClienteTelefono clienteTelefono)
        {
            _unitOfWork.ClienteTelefonos.Add(clienteTelefono);
            await _unitOfWork.SaveAsync();
            if (clienteTelefono == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = clienteTelefono.Id }, clienteTelefono);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteTelefono>> Put(int id, [FromBody] ClienteTelefono clienteTelefono)
        {
            if (clienteTelefono.Id == 0)
            {
                clienteTelefono.Id = id;
            }
            if (clienteTelefono.Id != id)
            {
                return NotFound();
            }
            _unitOfWork.ClienteTelefonos.Update(clienteTelefono);
            await _unitOfWork.SaveAsync();
            return clienteTelefono;
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
}