using BierStore.Models;
using BierStore.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BierStore.Infraestrutura
{
    public interface IMarcasRepositorio
    {
        Task<Marca> IncluirMarcaAsync(Marca marca);
        Task<Marca> RecuperarMarcaPorIdAsync(int id);
        Task<IEnumerable<Marca>> RecuperarTodasMarcasAsync();
        Task<IEnumerable<RecuperarCervejaPorMarcaViewModel>> RecuperarTodasAsCervejasPorMarcaAsync(int id);
        Task<bool> AtualizarMarcaAsync(int id, Marca marca);
        Task<bool> RemoverMarcaAsync(int id);
    }
}