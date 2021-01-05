using ApiContasPagar.Repositories.Interfaces;
using System.Threading.Tasks;
using ApiContasPagar.Services.Interfaces;
using System;
using ApiContasPagar.Models;
using ApiContasPagar.ViewModels;
using System.Collections.Generic;

namespace ApiContasPagar.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly IDespesaRepository _repository;
        public DespesaService(IDespesaRepository repository)
        {
            _repository = repository;
        }

        public async Task<DespesaViewModel> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                return await _repository.GetAll(pageIndex, pageSize);
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
                return await _repository.GetAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Despesa> Get(string id)
        {
            try
            {
                return await _repository.Get(id);
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
                return await _repository.Post(despesa);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<string> Delete(long id)
        {
            try
            {
                return await _repository.Delete(id);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<string> Put(long id, Despesa despesa)
        {
            try
            {
                return await _repository.Put(id, despesa);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}