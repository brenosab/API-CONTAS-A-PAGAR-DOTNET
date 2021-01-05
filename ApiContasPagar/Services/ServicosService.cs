using ApiContasPagar.Models;
using ApiContasPagar.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ApiContasPagar.Services
{
    public class ServicosService : IServicosService
    {
        private readonly IDespesaService _despesaService;
        private readonly IReceitaService _receitaService;

        public ServicosService(IDespesaService despesaService, IReceitaService receitaService)
        {
            _despesaService = despesaService;
            _receitaService = receitaService;
        }

        public async Task<decimal> GetSaldo()
        {
            try
            {
                var despesas = await _despesaService.GetAll();
                var receitas = await _receitaService.GetAll();
                decimal valorDespesa = 0;
                decimal valorReceita = 0;
                
                foreach(Despesa despesa in despesas)
                    valorDespesa += despesa.Valor;

                foreach (Receita receita in receitas)
                    valorReceita += receita.Valor;

                return valorDespesa + valorReceita;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}