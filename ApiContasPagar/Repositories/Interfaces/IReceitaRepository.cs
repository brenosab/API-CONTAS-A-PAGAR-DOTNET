using ApiContasPagar.Models;
using ApiContasPagar.ViewModels;
using System.Threading.Tasks;

namespace ApiContasPagar.Repositories.Interfaces
{
    public interface IReceitaRepository
    {
        Task<ReceitaViewModel> GetAll(int pageIndex, int pageSize);
        Task<Receita> Get(string id);
        Task<string> Post(Receita receita);
        Task<string> Delete(long id);
        Task<string> Put(long id, Receita receita);
    }
}