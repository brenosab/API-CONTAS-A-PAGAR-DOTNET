using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiContasPagar.Services.Interfaces;
using System;
using Microsoft.AspNetCore.Http;
using ApiContasPagar.Models;

namespace ApiContasPagar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : Controller
    {
        private readonly IDespesaService _service;
        public DespesaController(IDespesaService service)
        {
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult GetAll(int pageIndex, int pageSize)
        {
            try
            {
                return Ok(_service.GetAll(pageIndex, pageSize).Result);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                return Ok(_service.Get(id).Result);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Post(Despesa despesa)
        {
            try
            {
                var result = _service.Post(despesa).Result;
                return Ok(new { dados = result });
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Despesa despesa)
        {
            try
            {
                return Ok(await _service.Put(id, despesa));
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }
    }
}