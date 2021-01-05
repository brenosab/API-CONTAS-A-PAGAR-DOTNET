using System.Threading.Tasks;

namespace ApiContasPagar.Services.Interfaces
{
    public interface IServicosService
    {
        Task<decimal> GetSaldo();
    }
}