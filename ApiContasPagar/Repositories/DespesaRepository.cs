using ApiContasPagar.Models;
using ApiContasPagar.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ApiContasPagar.Repositorio.Script;
using ApiContasPagar.ViewModels;
using System.Data.SqlClient;

namespace ApiContasPagar.Repositories
{
    public class DespesaRepository : IDespesaRepository
    {
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 5;

        IConfiguration _configuration;

        public DespesaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Despesa> Get(string id)
        {
            try
            {
                string stringConnection = _configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
                using (SqlConnection conexao = new SqlConnection(stringConnection))
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
        public async Task<string> Post(Despesa despesa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(
                    _configuration.GetConnectionString("BaseIndicadores")))
                {
                    var result = await conexao.QueryAsync<Despesa>("SELECT * FROM Despesa WHERE ID = @Id", new
                    {
                        despesa.Descricao,
                        despesa.Valor,
                        despesa.Data,
                        despesa.Pago
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
                using (SqlConnection conexao = new SqlConnection(
                  _configuration.GetConnectionString("BaseIndicadores")))
                {
                    if (DespesaExists(id))
                    {
                        return ("Despesa não encontrada");
                    }
                    var result = await conexao.QueryAsync<Despesa>("SELECT * FROM Despesa WHERE ID = @Id", new { id });
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
            using (SqlConnection conexao = new SqlConnection(
                    _configuration.GetConnectionString("BaseIndicadores")))
            {
                return conexao.QueryAsync<string>("SELECT ID FROM USUARIO WHERE ID = :Id", new { Id = id }).Result.Any();
            }
        }

    
        public async Task<DespesaViewModel> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                //var conn = this.GetConnection();
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                //if (conn.State == ConnectionState.Open)
                //{
                //    string sqlTotalCount = DespesaScript.GetAllTotalCount;
                //    var totalCount = await conn.QueryAsync<int>(sqlTotalCount);
                //    long pageCount = 1;

                //    pageIndex = pageIndex == 0 ? DefaultPageIndex : pageIndex;
                //    pageSize = pageSize == 0 ? DefaultPageSize : pageSize;
                //    pageCount = (totalCount.FirstOrDefault() / pageSize) + ((totalCount.FirstOrDefault() % pageSize) != 0 ? 1 : 0);

                //    int offset = pageSize * (pageIndex - 1);
                //    int _pageSize = pageSize * pageIndex;
                    
                //    MetaData metaData = new MetaData
                //    {
                //        totalCount = totalCount.FirstOrDefault(),
                //        pageNumber = pageIndex == 0 ? 1 : pageIndex,
                //        pageCount = pageCount,
                //        hasNextPage = ((pageIndex == pageCount) || (pageIndex > pageCount)) ? false : true,
                //        hasPreviousPage = ((pageIndex == 1) || (pageIndex > pageCount)) ? false : true
                //    };

                //    string sql = DespesaScript.GetAll;
                //    var despesas = await conn.QueryAsync<Despesa>(sql, new { PageSize = _pageSize, Offset = offset });

                //    if (SetEvent(Eventos.GET)) return new DespesaViewModel { Users = despesas.ToList(), MetaData = metaData };
                //}
                return new DespesaViewModel { };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<string> Put(long id, Despesa despesa)
        {
            throw new NotImplementedException();
        }
    }
}