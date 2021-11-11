using BierStore.Infraestrutura;
using BierStore.InputModels;
using BierStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BierStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcasRepositorio _marcasRepositorio;
        public MarcasController(IMarcasRepositorio marcasRepositorio)
        {
            _marcasRepositorio = marcasRepositorio;
        }

        /// <summary>
        /// Endpoint para inserção de uma marca.
        /// </summary>
        /// <remarks>
        /// {
        ///     "nome": "Teste inserção marca."
        /// }
        /// </remarks>
        /// <param name="inputModel"></param>
        /// <response code="201">Marca criada com sucesso.</response>
        /// <response code="400">Falha ao criar uma marca.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna a marca criada.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IncluirMarca([FromBody] NovoMarcaInputModel inputModel)
        {
            var marcaCerveja = Marca.Criar(inputModel.Nome);
            if (await _marcasRepositorio.IncluirMarcaAsync(marcaCerveja) is var resultado && resultado == null)
                return BadRequest(resultado);

            return CreatedAtAction(nameof(RecuperarMarcaPorId), new { id = marcaCerveja.Id }, marcaCerveja);
        }

        /// <summary>
        /// Endpoint para recuperar uma marca por id.
        /// </summary>
        /// <remarks>
        /// {
        ///     "id": 1,
        ///     "nome": "Teste inserção marca.",
        ///     "dataInclusao": "2021-08-02T21:58:56.587",
        ///     "dataUltimaAlteracao": "2021-08-02T21:58:56.587"
        /// }
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Marca recuperada com sucesso.</response>
        /// <response code="404">Falha ao recuperar a marca por Id.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna a marca por Id.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RecuperarMarcaPorId(int id)
        {
            if (await _marcasRepositorio.RecuperarMarcaPorIdAsync(id) is var resultado && resultado == null)
                return NotFound(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Endpoint para recuperar um lista de cervejas por id da marca.
        /// </summary>
        /// <remarks>
        ///[
        ///    {
        ///        "id": 2,
        ///        "nome": "Polar",
        ///        "tipoDaCerveja": "Pilsen",
        ///        "origem": "Brasil",
        ///        "categoriaAlcoolica": "Com Álcool",
        ///        "preco": 3.40
        ///     },
        ///     {
        ///         "id": 2,
        ///         "nome": "Polar",
        ///         "tipoDaCerveja": "Extra Malte",
        ///         "origem": "Brasil",
        ///         "categoriaAlcoolica": "Com Álcool",
        ///         "preco": 3.80
        ///     }
        ///]
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Lista de cervejas por marca recuperada com sucesso.</response>
        /// <response code="404">Falha ao recuperar as cervejas por marca.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna uma lista de cervejas por marca.</returns>
        [HttpGet("Cervejas")]
        public async Task<IActionResult> RecuperarCervejasPorMarca([FromQuery] int id)
        {
            if (await _marcasRepositorio.RecuperarTodasAsCervejasPorMarcaAsync(id) is var resultado && resultado.Count() == 0)
                return NotFound(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Endpoint para recuperar todas as marca.
        /// </summary>
        /// <remarks>
        /// {
        ///     "id": 0,
        ///     "nome": "Teste inserção marca.",
        ///     "dataInclusao": "2021-08-02T21:58:56.587",
        ///     "dataUltimaAlteracao": "2021-08-02T21:58:56.587"
        /// },
        /// {
        ///    "id": 1,
        ///    "nome": "Teste inserção marca.",
        ///    "dataInclusao": "2021-08-02T21:58:56.587",
        ///    "dataUltimaAlteracao": "2021-08-02T21:58:56.587"
        ///  }
        /// </remarks>
        /// <response code="200">Marcas recuperadas com sucesso.</response>
        /// <response code="404">Falha ao recuperar todas as marcas.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna todas as marca disponíveis.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RecuperarTodasMarcas()
        {
            if (await _marcasRepositorio.RecuperarTodasMarcasAsync() is var resultado && resultado.Count() == 0)
                return NotFound(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Endpoint para atualização de uma marca por id.
        /// </summary>
        /// <remarks>
        /// {
        ///     "nome": "Teste atualização marca."
        /// }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <response code="200">Marca atualizada com sucesso.</response>
        /// <response code="400">Falha ao atualizar a marca.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna um booleano (true = atualizado / false = falha ao atualizar).</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarMarca(int id, [FromBody] AtualizarMarcaInputModel inputModel)
        {
            var marcaCerveja = Marca.Atualizar(id, inputModel.Nome, inputModel.DataUltimaAlteracao);
            if (await _marcasRepositorio.AtualizarMarcaAsync(id, marcaCerveja) is var marcaAtualizada && marcaAtualizada == false)
                return BadRequest(marcaAtualizada);

            return Ok(marcaAtualizada);
        }

        /// <summary>
        /// Endpoint para remover uma marca por id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Marca removida com sucesso.</response>
        /// <response code="400">Falha ao remover a marca.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna um booleano (true = removido / false = falha ao remover).</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoverMarca(int id)
        {
            if (await _marcasRepositorio.RemoverMarcaAsync(id) is var removerMarca && removerMarca == false)
                return BadRequest(removerMarca);

            return Ok(removerMarca);
        }
    }
}
