using ApiContasPagar.Models;
using ApiContasPagar.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ApiContasPagar.ViewModels;
using System.Data.SqlClient;
using ApiContasPagar.Repositories.Script;

namespace ApiContasPagar.Repositories
{
    public class ReceitaRepository : IReceitaRepository
    {
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 5;

        IConfiguration _configuration;
        private string _connectionstring;

        public ReceitaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionstring = _configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
        }

        public async Task<Receita> Get(string id)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connectionstring))
                {
                    var receita = await conexao.QueryAsync<Receita>(ReceitaScript.Get, new { id });
                    return receita.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> Post(Receita receita)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connectionstring))
                {
                    string recebido = receita.Recebido == true ? "S" : "N";
                    var result = await conexao.QueryAsync<Receita>(ReceitaScript.Insert, new
                    {
                        receita.Descricao,
                        receita.Valor,
                        recebido
                    });
                    return "Sucesso!!!";
                }   
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> Delete(long id)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connectionstring))
                {
                    if (!ReceitaExists(id))
                    {
                        return ("Receita não encontrada");
                    }
                    var result = await conexao.QueryAsync<Receita>(ReceitaScript.Delete, new { id });
                    return "Sucesso!!!";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> Put(long id, Receita receita)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connectionstring))
                {
                    if (!ReceitaExists(id))
                    {
                        return ("Receita não encontrada");
                    }
                    string recebido = receita.Recebido == true ? "S" : "N";
                    var result = await conexao.QueryAsync<Receita>(ReceitaScript.Update, new 
                    { 
                        id,
                        receita.Descricao,
                        receita.Valor,
                        recebido
                    });
                    return "Sucesso!!!";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool ReceitaExists(long id)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionstring))
            {
                return conexao.QueryAsync<string>("SELECT ID FROM Receita WHERE ID = @id", new { id }).Result.Any();
            }
        }
    
        public async Task<ReceitaViewModel> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connectionstring))
                {
                    var totalCount = await conexao.QueryAsync<int>(ReceitaScript.GetAllTotalCount);
                    long pageCount = 1;

                    pageIndex = pageIndex == 0 ? DefaultPageIndex : pageIndex;
                    pageSize = pageSize == 0 ? DefaultPageSize : pageSize;
                    pageCount = (totalCount.FirstOrDefault() / pageSize) + ((totalCount.FirstOrDefault() % pageSize) != 0 ? 1 : 0);

                    int offset = pageSize * (pageIndex - 1);
                    int _pageSize = pageSize * pageIndex;

                    MetaData metaData = new MetaData
                    {
                        totalCount = totalCount.FirstOrDefault(),
                        pageNumber = pageIndex == 0 ? 1 : pageIndex,
                        pageCount = pageCount,
                        hasNextPage = ((pageIndex == pageCount) || (pageIndex > pageCount)) ? false : true,
                        hasPreviousPage = ((pageIndex == 1) || (pageIndex > pageCount)) ? false : true
                    };

                    var receitas = await conexao.QueryAsync<Receita>(ReceitaScript.GetAll, new 
                    { 
                        PageSize = _pageSize, 
                        Offset = offset 
                    });
                    return new ReceitaViewModel { Receitas = receitas.ToList(), MetaData = metaData };
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}