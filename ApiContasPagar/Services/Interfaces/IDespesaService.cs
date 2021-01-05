using ApiContasPagar.Models;
using ApiContasPagar.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiContasPagar.Services.Interfaces
{
    public interface IDespesaService
    {
        Task<DespesaViewModel> GetAll(int pageIndex, int pageSize);
        Task<IEnumerable<Despesa>> GetAll();
        Task<Despesa> Get(string id);
        Task<string> Post(Despesa despesa);
        Task<string> Delete(long id);
        Task<string> Put(long id, Despesa despesa);
    }
}