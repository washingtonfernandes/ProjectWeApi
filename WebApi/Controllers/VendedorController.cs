using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Dtos;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/")]
    public class VendedorController : ControllerBase
    {
       private readonly IVendedorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public VendedorController(
            IVendedorRepository vendedorRepository,
            IUnitOfWork unitOfWork)
        {
            this._repository = vendedorRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("v1/vendedores")]
        public async Task<IActionResult> GetAllAsync()
        {
            var vendedoresLista = await _repository.GetAllAsync();

            List<VendedorDto> vendedoresDto = new List<VendedorDto>();

            foreach (Vendedor vendedor in vendedoresLista)
            {
                var vendedorDto = new VendedorDto()
                {
                    Id = vendedor.Id,
                    Nome = vendedor.Nome,
                    Bonificacao = vendedor.Bonificacao
                };

                vendedoresDto.Add(vendedorDto);
            }

            return Ok(vendedoresDto);
        }

        [HttpGet("v1/vendedores/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var vendedor = await _repository.GetByIdAsync(id);

            if (vendedor == null)
                return NotFound();
            else
            {
                var vendedorDto = new VendedorDto()
                {
                    Id = vendedor.Id,
                    Nome = vendedor.Nome,
                    Bonificacao = vendedor.Bonificacao
                };

                return Ok(vendedorDto);
            }
        }

        [HttpPost("v1/vendedores")]
        public async Task<IActionResult> PostAsync([FromBody] VendedorViewModel model)
        {
            var vendedor = new Vendedor
            {
                Nome = model.Nome,
                Bonificacao = model.Bonificacao
            };

            _repository.Save(vendedor);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "O vendedor " + vendedor.Nome + " foi cadastrado!"
            });
        }

        [HttpDelete("v1/vendedores/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var vendedorDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if (vendedorDel == false)
                return NotFound();
            else
                return Ok(id);
        }

        [HttpPatch("v1/vendedores/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] VendedorViewModel model)
        {
            var vendedor = await _repository.GetByIdAsync(id);

            if (vendedor == null)
                return NotFound();
            else
            {
                vendedor.Nome = model.Nome;
                vendedor.Bonificacao = model.Bonificacao;

                _repository.Update(vendedor);
                await _unitOfWork.CommitAsync();

                var vendedorDto = new VendedorDto()
                {
                    Id = vendedor.Id,
                    Nome = model.Nome,
                    Bonificacao = model.Bonificacao
                };

                return Ok(vendedorDto);
            }
        }
    }
}