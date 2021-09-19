using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace M3webAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        string connectionString = @"Data Source=LAPTOP-K6388OC0\SQLEXPRESS; database=M3/LumberOne; Integrated Security=true;";


        // private readonly ILogger<ProductsController> _logger;
        // public ProductsController(ILogger<ProductsController> logger)
        // {
        //     _logger = logger;
        // }

        [HttpGet]
        [Route("/Products")]
        public Response GetProducts()
        {
            Response response = new Response();
            List<Products> products = new List<Products>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    products = Products.GetProducts(con);
                }

                response.result = "success";
                response.message = $"{products.Count()} rows selected.";
                response.products = products;
            }
            catch (Exception ex)
            {
                response.result = "failure";
                response.message = ex.Message;
            }

            return response;
        }

        // Search
        [HttpGet]
        [Route("/Products/SearchProducts")]
        public Response SearchProducts( string search)
        {
            Response response = new Response();
            List<Products> products = new List<Products>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    products = Products.SearchProducts(con, search);
                }

                response.result = "success";
                response.message = $"{products.Count()} rows selected.";
                response.products = products;
            }
            catch (Exception ex)
            {
                response.result = "failure";
                response.message = ex.Message;
            }

            return response;
        }






    }
}