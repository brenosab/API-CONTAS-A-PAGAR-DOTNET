using ApiContasPagar.Models;
using ApiContasPagar.ViewModels;
using System.Threading.Tasks;

namespace ApiContasPagar.Services.Interfaces
{
    public interface IReceitaService
    {
        Task<ReceitaViewModel> GetAll(int pageIndex, int pageSize);
        Task<Receita> Get(string id);
        Task<string> Post(Receita receita);
        Task<string> Delete(long id);
        Task<string> Put(long id, Receita receita);
    }
}