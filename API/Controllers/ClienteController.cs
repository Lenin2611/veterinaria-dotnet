using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            var Cliente = await _unitOfWork.Clientes.GetAllAsync();
            return Ok(Cliente);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cliente>> Get(int Id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(Id);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cliente>> Post(Cliente cliente)
        {
            _unitOfWork.Clientes.Add(cliente);
            await _unitOfWork.SaveAsync();
            if (cliente == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cliente>> Put(int id, [FromBody] Cliente cliente)
        {
            if (cliente.Id == 0)
            {
                cliente.Id = id;
            }
            if (cliente.Id != id)
            {
                return NotFound();
            }
            _unitOfWork.Clientes.Update(cliente);
            await _unitOfWork.SaveAsync();
            return cliente;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            _unitOfWork.Clientes.Remove(cliente);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}