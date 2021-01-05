using ApiContasPagar.Models;
using ApiContasPagar.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ApiContasPagar.Repositorio.Script;
using ApiContasPagar.ViewModels;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ApiContasPagar.Repositories
{
    public class DespesaRepository : IDespesaRepository
    {
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 5;

        IConfiguration _configuration;
        private string _connectionstring;

        public DespesaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionstring = _configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
        }

        public async Task<Despesa> Get(string id)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connectionstring))
                {
                    var despesa = await conexao.QueryAsync<Despesa>(DespesaScript.Get, new { id });
                    return despesa.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Despesa>> GetAll()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connectionstring))
                {
                    var despesa = await conexao.QueryAsync<Despesa>(@"SELECT	ID AS Id,
		                                                                        DESCRICAO AS Descricao,
		                                                                        VALOR AS Valor,
		                                                                        DATA_DESPESA AS Data,
                                                                                CAST(CASE WHEN PAGO = 'N' THEN 0 ELSE 1 END AS BIT) AS Pago
                                                                        FROM DESPESA");
                    return despesa;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> Post(Despesa despesa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connectionstring))
                {
                    string pago = despesa.Pago == true ? "S" : "N";
                    var result = await conexao.QueryAsync<Despesa>(DespesaScript.Insert, new
                    {
                        despesa.Descricao,
                        despesa.Valor,
                        pago
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
                    if (!DespesaExists(id))
                    {
                        return ("Despesa não encontrada");
                    }
                    var result = await conexao.QueryAsync<Despesa>(DespesaScript.Delete, new { id });
                    return "Sucesso!!!";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> Put(long id, Despesa despesa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connectionstring))
                {
                    if (!DespesaExists(id))
                    {
                        return ("Despesa não encontrada");
                    }
                    string pago = despesa.Pago == true ? "S" : "N";
                    var result = await conexao.QueryAsync<Despesa>(DespesaScript.Update, new 
                    { 
                        id,
                        despesa.Descricao,
                        despesa.Valor,
                        pago
                    });
                    return "Sucesso!!!";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool DespesaExists(long id)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionstring))
            {
                return conexao.QueryAsync<string>("SELECT ID FROM Despesa WHERE ID = @id", new { id }).Result.Any();
            }
        }
    
        public async Task<DespesaViewModel> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connectionstring))
                {
                    var totalCount = await conexao.QueryAsync<int>(DespesaScript.GetAllTotalCount);
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

                    var despesas = await conexao.QueryAsync<Despesa>(DespesaScript.GetAll, new 
                    { 
                        PageSize = _pageSize, 
                        Offset = offset 
                    });
                    return new DespesaViewModel { Despesas = despesas.ToList(), MetaData = metaData };
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}