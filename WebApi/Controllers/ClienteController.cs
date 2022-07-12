using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteController(
            IClienteRepository clienteRepository,
            IUnitOfWork unitOfWork)
        {
            this._repository = clienteRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("v1/clientes")]
        public async Task<IActionResult> GetAllAsync()
        {
            var clientesLista = await _repository.GetAllAsync();

            List<ClienteDto> clientesDto = new List<ClienteDto>();

            foreach (Cliente cliente in clientesLista)
            {
                var clienteDto = new ClienteDto()
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Cpf = cliente.Cpf,
                    DataCadastro = cliente.DataCadastro
                };

                clientesDto.Add(clienteDto);
            }

            return Ok(clientesDto);
        }

        [HttpGet("v1/clientes/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var cliente = await _repository.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();
            else
            {
                var clienteDto = new ClienteDto()
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Cpf = cliente.Cpf,
                    DataCadastro = cliente.DataCadastro
                };

                return Ok(clienteDto);
            }
        }

        [HttpPost("v1/clientes")]
        public async Task<IActionResult> PostAsync([FromBody] ClienteViewModelPost model)
        {
            var cliente = new Cliente
            {
                Nome = model.Nome,
                Cpf = model.Cpf,
                DataCadastro = model.DataCadastro
            };

            _repository.Save(cliente);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "O Cliente " + cliente.Nome + " foi adicionado com sucesso!"
            });
        }

        [HttpDelete("v1/clientes/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var clienteDel = _repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if (clienteDel == false)
                return NotFound();
            else
                return Ok(id);
        }

        [HttpPatch("v1/clientes/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] ClienteViewModelPatch model)
        {
            var cliente = await _repository.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();
            else
            {
                cliente.Nome = model.Nome;
                cliente.Cpf = model.Cpf;

                _repository.Update(cliente);
                await _unitOfWork.CommitAsync();

                var clienteDto = new ClienteDto()
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Cpf = cliente.Cpf,
                    DataCadastro = cliente.DataCadastro
                };
                return Ok(clienteDto);
            }
        }
    }
}