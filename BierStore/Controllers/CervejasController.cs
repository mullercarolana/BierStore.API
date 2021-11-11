using System.Linq;
using System.Threading.Tasks;
using BierStore.Infraestrutura;
using BierStore.InputModels;
using BierStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BierStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CervejasController : ControllerBase
    {
        private readonly ICervejasRepositorio _cervejasRepositorio;

        public CervejasController(ICervejasRepositorio cervejasRepositorio)
        {
            _cervejasRepositorio = cervejasRepositorio;
        }

        /// <summary>
        /// Endpoint para inserção de uma cerveja.
        /// </summary>
        /// <remarks>
        /// {
        ///     "idMarca": 1,
        ///     "nome": "Teste inserção cerveja",
        ///     "origem": "Brasil",
        ///     "categoriaAlcoolica": "Com Álcool",
        ///     "preco": 32.00
        /// }
        /// </remarks>
        /// <param name="inputModel"></param>
        /// <response code="201">Cerveja criada com sucesso.</response>
        /// <response code="400">Falha ao criar uma cerveja.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna a cerveja criada.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IncluirCerveja([FromBody] NovoCervejaInputModel inputModel)
        {
            var incluirCerveja = Cerveja.Criar(inputModel.IdMarca, inputModel.Nome, inputModel.Origem, inputModel.CategoriaAlcoolica, inputModel.Preco);
            if (await _cervejasRepositorio.IncluirCervejaAsync(incluirCerveja) is var resultado && resultado == null)
                return BadRequest(resultado);

            return CreatedAtAction(nameof(RecuperarCervejaPorId), new { id = incluirCerveja.Id }, incluirCerveja);
        }

        /// <summary>
        /// Endpoint para recuperar uma cerveja por id.
        /// </summary>
        /// <remarks>
        /// {
        ///     "id": 1,
        ///     "idMarca": 1,
        ///     "nomeDaMarca": "Teste recuperção cerveja.",
        ///     "tipoCerveja": "Teste recuperção cerveja.",
        ///     "origem": "Brasil",
        ///     "categoriaAlcoolica": "Com Álcool",
        ///     "preco": 35.00,
        ///     "dataInclusao": "2021-08-02T21:58:56.587",
        ///     "dataUltimaAlteracao": "2021-08-02T21:58:56.587"
        /// }
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Cerveja recuperada com sucesso.</response>
        /// <response code="404">Falha ao recuperar a cerveja por id.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna a cerveja por Id.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RecuperarCervejaPorId(int id)
        {
            if (await _cervejasRepositorio.RecuperarCervejaPorIdAsync(id) is var resultado && resultado == null)
                return NotFound(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Endpoint para recuperar todas as cervejas.
        /// </summary>
        /// <remarks>
        /// {
        ///     "id": 0,
        ///     "idMarca": 1,
        ///     "nomeDaMarca": "Teste recuperção cerveja.",
        ///     "tipoCerveja": "Teste recuperção cerveja.",
        ///     "origem": "Brasil",
        ///     "categoriaAlcoolica": "Com Álcool",
        ///     "preco": 35.00,
        ///     "dataInclusao": "2021-08-02T21:58:56.587",
        ///     "dataUltimaAlteracao": "2021-08-02T21:58:56.587"
        /// },
        /// {
        ///     "id": 1,
        ///     "idMarca": 1,
        ///     "nomeDaMarca": "Teste recuperção cerveja.",
        ///     "tipoCerveja": "Teste recuperção cerveja.",
        ///     "origem": "Brasil",
        ///     "categoriaAlcoolica": "Com Álcool",
        ///     "preco": 40.00,
        ///     "dataInclusao": "2021-08-02T21:58:56.587",
        ///     "dataUltimaAlteracao": "2021-08-02T21:58:56.587"
        ///  }
        /// </remarks>
        /// <response code="200">Cervejas recuperadas com sucesso.</response>
        /// <response code="404">Falha ao recuperar todas as cervejas.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna todas as cervejas disponíveis.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RecuperarTodasCervejas()
        {
            if (await _cervejasRepositorio.RecuperarTodasCervejasAsync() is var resultado && resultado.Count() == 0)
                return NotFound(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Endpoint para atualização de uma cerveja por id.
        /// </summary>
        /// <remarks>
        /// {
        ///     "idMarca": 1,
        ///     "nome": "Teste alteração cerveja",
        ///     "origem": "Alemanha",
        ///     "categoriaAlcoolica": "Sem Álcool",
        ///     "preco": 32.00
        /// }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <response code="200">Cerveja atualizada com sucesso.</response>
        /// <response code="400">Falha ao atualizar a cerveja.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna um booleano (true = atualizado / false = falha ao atualizar).</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarCerveja(int id, [FromBody] AtualizarCervejaInputModel inputModel)
        {
            var cerveja = Cerveja.Atualizar(id, inputModel.IdMarca, inputModel.Nome, inputModel.Origem, inputModel.CategoriaAlcoolica, inputModel.Preco, inputModel.DataUltimaAlteracao);
            if (await _cervejasRepositorio.AtualizarCervejaAsync(id, cerveja) is var marcaAtualizada && marcaAtualizada == false)
                return BadRequest(marcaAtualizada);
            return Ok(marcaAtualizada);
        }

        /// <summary>
        /// Endpoint para remover uma cerveja por id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Cerveja removida com sucesso.</response>
        /// <response code="400">Falha ao remover a cerveja.</response>
        /// <response code="500">Ocorreu um erro interno no serviço.</response>
        /// <returns>Retorna um booleano (true = removido / false = falha ao remover).</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoverCerveja(int id)
        {
            if (await _cervejasRepositorio.RemoverCervejaAsync(id) is var removerCerveja && removerCerveja == false)
                return BadRequest(removerCerveja);
            return Ok(removerCerveja);
        }
    }
}
