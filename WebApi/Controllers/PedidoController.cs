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
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _repository;
        private readonly IProdutoRepository _produtorepository;
        private readonly IUnitOfWork _unitOfWork;

        public PedidoController(
            IPedidoRepository pedidoRepository,
            IUnitOfWork unitOfWork,
            IProdutoRepository produtoRepository
            )
        {
            this._repository = pedidoRepository;
            this._produtorepository = produtoRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("v1/pedidos")]
        public async Task<IActionResult> GetAllAsync()
        {
            var pedidosLista = await _repository.GetAllAsync();

            List<PedidoDto> pedidosDto = new List<PedidoDto>();

            foreach (Pedido pedido in pedidosLista)
            {
                var pedidoDto = new PedidoDto()
                {
                    Id = pedido.Id,
                    DataPedido = pedido.DataPedido,
                    ValorTotal = pedido.ValorTotal,
                    ClienteId = pedido.ClienteId,
                    VendedorId = pedido.VendedorId,
                    PagamentoId = pedido.PagamentoId
                    
                };

                pedidosDto.Add(pedidoDto);
            }

            return Ok(pedidosDto);
        }

        [HttpGet("v1/pedidos/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var pedido = await _repository.GetByIdAsync(id);

            if (pedido == null)
                return NotFound();
            else
            {
                var pedidoDto = new PedidoDto()
                {
                    Id = pedido.Id,
                    DataPedido = pedido.DataPedido,
                    ValorTotal = pedido.ValorTotal,
                    ClienteId = pedido.ClienteId,
                    VendedorId = pedido.VendedorId,
                    PagamentoId = pedido.PagamentoId
                };

                return Ok(pedidoDto);
            }
        }

        [HttpPost("v1/pedidos")]
        public async Task<IActionResult> PostAsync([FromBody] PedidoViewModel model)
        {
            var listaProdutos = new List<Produto>();

            foreach (var produto in model.InserirProdutos)
            {
               var produtoBusca = await _produtorepository.GetByIdAsync(produto.ProdutoId); 

               listaProdutos.Add(produtoBusca);  
            }

            var pedido = new Pedido
            {
                DataPedido = model.DataPedido,
                ValorTotal = model.ValorTotal,
                ClienteId = model.ClienteId,
                VendedorId = model.VendedorId,
                PagamentoId = model.PagamentoId,
                Produtos = listaProdutos
            };
            
            _repository.Save(pedido);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "Pedido " + pedido.Id + " foi adicionado!"
            });
        }

        [HttpDelete("v1/pedidos/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var pedidoDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if (pedidoDel == false)
                return NotFound();
            else
                return Ok(id);
        }

        [HttpPatch("v1/pedidos/{id:int}")] 
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] PedidoViewModel model)
        {
            var pedido = await _repository.GetByIdAsync(id);

            if (pedido == null)
                return NotFound();
            else
            {
                pedido.DataPedido = model.DataPedido;
                pedido.ValorTotal = model.ValorTotal;
                pedido.ClienteId = model.ClienteId;
                pedido.VendedorId = model.VendedorId;
                pedido.PagamentoId = model.PagamentoId;

                _repository.Update(pedido);
                await _unitOfWork.CommitAsync();

                var pedidoDto = new PedidoDto()
                {
                    DataPedido = pedido.DataPedido,
                    ValorTotal = pedido.ValorTotal,
                    ClienteId = pedido.ClienteId,
                    VendedorId = pedido.VendedorId,
                    PagamentoId = pedido.PagamentoId
                };

                return Ok(pedidoDto);
            }
        }
    }
    
}