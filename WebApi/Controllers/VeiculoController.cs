using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{        
    [Route("api/")]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoRepository repository;
        private readonly IUnitOfWork _unitOfWork;

        public VeiculoController(
            IVeiculoRepository veiculoRepository,
            IUnitOfWork unitOfWork)
        {
            this.repository = veiculoRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("veiculos")]
        public async Task<IActionResult> GetAllAsync()
        {
            var veiculosList = await repository.GetAllAsync();

            List<VeiculoDTO> veiculosDTO = new List<VeiculoDTO>();
            
            foreach(Veiculo veiculo in veiculosList)
            {
                var veiculoDTO = new VeiculoDTO()
            {
                Id = veiculo.Id,
                Nome = veiculo.Nome,
                Placa = veiculo.Placa,
                Ano = veiculo.Ano,
                Cor = veiculo.Cor
            };
            
            veiculosDTO.Add(veiculoDTO);
        }

        return Ok(veiculosDTO);
    }

        [HttpGet("veiculos/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var veiculo = await repository.GetByIdAsync(id);

            if (veiculo == null)
                return NotFound();
            else
            {
                var veiculoDTO = new VeiculoDTO()
            {
                Id = veiculo.Id,
                Nome = veiculo.Nome,
                Placa = veiculo.Placa,
                Ano = veiculo.Ano,
                Cor = veiculo.Cor
            };
            
            return Ok(veiculoDTO);
        }
    }

        [HttpPost("veiculos")]
        public async Task<IActionResult> PostAsync([FromBody] VeiculoCreate model)
        {
            var veiculo = new Veiculo()
            {
                Nome = model.Nome,
                Placa = model.Placa,
                Ano = model.Ano,
                Cor = model.Cor
            };

            repository.Save(veiculo);
            await _unitOfWork.CommitAsync();

            return Ok(new
            {
                message = "Veiculo " + veiculo.Nome + " foi registrado com sucesso!"
            });
        }

        [HttpDelete("veiculos/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var veiculoDeleted = repository.Delete(id);
            await _unitOfWork.CommitAsync();

            if (veiculoDeleted == false)
                return NotFound();
            else
                return Ok(new
            {
                message = "Veiculo deletado do sistema com sucesso!",
                id = id
            });
        }

        [HttpPatch("veiculos/{id:int}")] //vai editar um veiculo de acordo com o id informado e com os dados alterados
        public async Task<IActionResult> PutAsync([FromRoute]int id, [FromBody] VeiculoUpdate model)
        {
            var veiculo = await repository.GetByIdAsync(id);

            if (veiculo == null) return NotFound();
            else
            {
                veiculo.Cor = model.Cor;

                repository.Update(veiculo);
                await _unitOfWork.CommitAsync();
                return Ok(veiculo);
            }
        }
    }
}