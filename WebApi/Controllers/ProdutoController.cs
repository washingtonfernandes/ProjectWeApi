using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProdutoController(
            IProdutoRepository produtoRepository,
            IUnitOfWork unitOfWork)
        {
            this._repository = produtoRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("v1/produtos")]
        public async Task<IActionResult> GetAllAsync()
        {
            var produtosLista = await _repository.GetAllAsync();

            List<ProdutoDto> produtosDto = new List<ProdutoDto>();

            foreach (Produto produto in produtosLista)
            {
                if (produto.QuantVenda == null)
                {
                    var produtoDto = new ProdutoDto()
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        Quantidade = produto.Quantidade,
                        CategoriaId = produto.CategoriaId,
                    };
                    produtosDto.Add(produtoDto);

                }else
                {
                    var produtoDto = new ProdutoDto()
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        QuantVenda = produto.QuantVenda,
                        CategoriaId = produto.CategoriaId,

                    };
                    produtosDto.Add(produtoDto);
                }
  
            }

            return Ok(produtosDto);
        }


        [HttpGet("v1/produtos/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var produto = await _repository.GetByIdAsync(id);

            if (produto == null)
                return NotFound();
            else
            {
               if (produto.QuantVenda == null)
                {
                    var produtoDto = new ProdutoDto()
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        Quantidade = produto.Quantidade,
                        CategoriaId = produto.CategoriaId,

                    };
                   return Ok(produtoDto);

                }else
                {
                    var produtoDto = new ProdutoDto()
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        QuantVenda = produto.QuantVenda,
                        Quantidade = produto.Quantidade,
                        CategoriaId = produto.CategoriaId,

                    };
                    return Ok(produtoDto);
                }
  
            }
        }


        [HttpPost("v1/produtos")]
        public async Task<IActionResult> PostAsync([FromBody] ProdutoViewModel model)
        {
            var produto = new Produto
            {
                Nome = model.Nome,
                Preco = model.Preco,
                QuantVenda = model.QuantVenda,
                Quantidade = model.Quantidade,
                CategoriaId = model.CategoriaId
            };

            _repository.Save(produto);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "Produto " + produto.Nome + " foi adicionado com sucesso!"
            });
        }


        [HttpDelete("v1/produtos/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var produtoDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if (produtoDel == false)
                return NotFound();
            else
                return Ok(id);
        }


        [HttpPatch("v1/produtos/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] ProdutoViewModel model)
        {
            var produto = await _repository.GetByIdAsync(id);

            if (produto == null)
                return NotFound();
            else
            {
                if (produto.QuantVenda == null)
                {
                    produto.Nome = model.Nome;
                    produto.Preco = model.Preco;
                    produto.Quantidade = model.Quantidade;
                    produto.CategoriaId = model.CategoriaId;
                }
                else
                {
                    produto.Nome = model.Nome;
                    produto.Preco = model.Preco;
                    produto.QuantVenda = model.QuantVenda;
                    produto.CategoriaId = model.CategoriaId;
                }

                _repository.Update(produto);
                await _unitOfWork.CommitAsync();

                if (produto.QuantVenda == null)
                {
                    var produtoDto = new ProdutoDto()
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        Quantidade = produto.Quantidade,
                        CategoriaId = produto.CategoriaId,

                    }; 
                    return Ok(produtoDto);
                }
                else
                {
                    var produtoDto = new ProdutoDto()
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        QuantVenda = produto.QuantVenda,
                        CategoriaId = produto.CategoriaId                         

                    }; 
                    return Ok(produtoDto);
                }
            }
        }
    }
}