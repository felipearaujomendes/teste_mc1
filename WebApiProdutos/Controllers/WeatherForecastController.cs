using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApiProdutos.Model;
using WebApiProdutos.Repository;

namespace WebApiProdutos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        ProdutoRepository data = null;
        public WeatherForecastController(IConfiguration config)
        {
            data = new ProdutoRepository(config);
        }
        [HttpGet]
        public List<Produtos> Get()
        {
            return data.ListaProdutos();
        }
        [HttpGet("{sku}")]
        public Produtos GetById(long sku)
        {

            return data.ProdutoPorId(sku);

        }

        [HttpPost]
        public void Post(Produto produto)
        {
            data.InsereProduto(produto);
        }


        [HttpDelete]
        public void Delete(long sku)
        {
            data.DeletaProduto(sku);
        }
    }
}
