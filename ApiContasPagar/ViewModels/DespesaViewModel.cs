using ApiContasPagar.Models;
using System.Collections.Generic;

namespace ApiContasPagar.ViewModels
{
    public class DespesaViewModel
    {
        public List<Despesa> Despesas { get; set; }
        public MetaData MetaData { get; set; }
    }
}