using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaController(
            ICategoriaRepository categoriaRepository,
            IUnitOfWork unitOfWork)
        {
            this._repository = categoriaRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("v1/categorias")]
        public async Task<IActionResult> GetAllAsync()
        {
            var categoriasLista = await _repository.GetAllAsync();

            List<CategoriaDto> categoriasDto = new List<CategoriaDto>();

            foreach (Categoria categoria in categoriasLista)
            {
                var categoriaDto = new CategoriaDto()
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                };

                categoriasDto.Add(categoriaDto);
            }


            return Ok(categoriasDto);
        }

        [HttpGet("v1/categorias/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var categoria = await _repository.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();
            else
            {
                var categoriaDto = new CategoriaDto()
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                };

                return Ok(categoriaDto);
            }
        }

        [HttpPost("v1/categorias")]
        public async Task<IActionResult> PostAsync([FromBody] CategoriaViewModel model)
        {
            var categoria = new Categoria
            {
                Nome = model.Nome,

            };

            _repository.Save(categoria);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "Categorias " + categoria.Produtos + " foram adicionados com sucesso!"
            });
        }

        [HttpDelete("v1/categorias/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var categoriaDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if (categoriaDel == false)
                return NotFound();
            else
                return Ok(id);
        }

        [HttpPatch("v1/categorias/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] CategoriaViewModel model)
        {
            var categoria = await _repository.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();
            else
            {
                categoria.Nome = model.Nome;

                _repository.Update(categoria);
                await _unitOfWork.CommitAsync();

                var categoriaDto = new CategoriaDto()
                {
                    Nome = model.Nome,
                };

                return Ok(categoriaDto);
            }
        }
    }
}