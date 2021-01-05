using ApiContasPagar.Models;
using System.Collections.Generic;

namespace ApiContasPagar.ViewModels
{
    public class ReceitaViewModel
    {
        public List<Receita> Receitas { get; set; }
        public MetaData MetaData { get; set; }
    }
}