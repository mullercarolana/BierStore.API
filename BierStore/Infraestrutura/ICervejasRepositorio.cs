using BierStore.Models;
using BierStore.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BierStore.Infraestrutura
{
    public interface ICervejasRepositorio
    {
        Task<Cerveja> IncluirCervejaAsync(Cerveja cerveja);
        Task<CervejaViewModel> RecuperarCervejaPorIdAsync(int id);
        Task<IEnumerable<CervejaViewModel>> RecuperarTodasCervejasAsync();
        Task<bool> AtualizarCervejaAsync(int id, Cerveja cerveja);
        Task<bool> RemoverCervejaAsync(int id);
    }
}
