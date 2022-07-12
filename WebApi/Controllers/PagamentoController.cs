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
    [Route("[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PagamentoController(
            IPagamentoRepository pagamentoRepository,
            IUnitOfWork unitOfWork)
        {
            this._repository = pagamentoRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("v1/pagamentos")]
        public async Task<IActionResult> GetAllAsync()
        {
            var pagamentosLista = await _repository.GetAllAsync();

            List<PagamentoDto> pagamentosDto = new List<PagamentoDto>();

            foreach (Pagamento pagamento in pagamentosLista)
            {
                var pagamentoDto = new PagamentoDto()
                {
                    Id = pagamento.Id,
                    Tipo = pagamento.Tipo
                };
                
                pagamentosDto.Add(pagamentoDto);
            }

            return Ok(pagamentosDto);
        }

        [HttpGet("v1/pagamentos/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var pagamento = await _repository.GetByIdAsync(id);

            if (pagamento == null)
                return NotFound();
            else
            {
                var pagamentoDto = new PagamentoDto()
                {
                    Id = pagamento.Id,
                    Tipo = pagamento.Tipo
                };
                return Ok(pagamentoDto);
            }
        }

        [HttpPost("v1/pagamentos")]
        public async Task<IActionResult> PostAsync([FromBody] PagamentoViewModel model)
        {
            var pagamento = new Pagamento()
            {
                Tipo = model.Tipo
            };
            
            _repository.Save(pagamento);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "O tipo de pagamento " + pagamento.Tipo + " foi adicionado!"
            });
        }
        
        [HttpDelete("v1/pagamentos/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var pagamentoDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if (pagamentoDel == false)
                return NotFound();
            else
                return Ok(id);
        }
        
        [HttpPatch("v1/pagamentos/{id:int}")] 
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] PagamentoViewModel model)
        {
            var pagamento = await _repository.GetByIdAsync(id);

            if (pagamento == null)
                return NotFound();
            else
            {
                pagamento.Tipo = model.Tipo;

                _repository.Update(pagamento);
                await _unitOfWork.CommitAsync();

                var pagamentoDto = new PagamentoDto()
                {
                    Id = pagamento.Id,
                    Tipo = pagamento.Tipo
                };

                return Ok(pagamentoDto);
            }
        }
    }
}