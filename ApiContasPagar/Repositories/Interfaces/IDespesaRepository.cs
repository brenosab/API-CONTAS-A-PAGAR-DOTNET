using ApiContasPagar.Models;
using ApiContasPagar.ViewModels;
using System.Threading.Tasks;

namespace ApiContasPagar.Repositories.Interfaces
{
    public interface IDespesaRepository
    {
        Task<DespesaViewModel> GetAll(int pageIndex, int pageSize);
        Task<Despesa> Get(string id);
        Task<string> Post(Despesa despesa);
        Task<string> Delete(long id);
        Task<string> Put(long id, Despesa despesa);
    }
}