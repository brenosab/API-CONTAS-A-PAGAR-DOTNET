using ApiContasPagar.Models;
using ApiContasPagar.Repositories.Interfaces;
using ApiContasPagar.Services.Interfaces;
using ApiContasPagar.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiContasPagar.Services
{
    public class ReceitaService : IReceitaService
    {
        private readonly IReceitaRepository _repository;
        public ReceitaService(IReceitaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ReceitaViewModel> GetAll(int pageIndex, int pageSize)
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

        public async Task<IEnumerable<Receita>> GetAll()
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

        public async Task<Receita> Get(string id)
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

        public async Task<string> Post(Receita receita)
        {
            try
            {
                return await _repository.Post(receita);
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
                return await _repository.Delete(id);
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
                return await _repository.Put(id, receita);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}