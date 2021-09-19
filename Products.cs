using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;


namespace M3webAPI
{

    public class Products
    {
        

        public int ProductID { get; set; }
        public string SKU { get; set; }
        public string Product_Name { get; set; }
        public string Unit_Cost { get; set; }
        public string Available_Sizes { get; set; }
        public int Units_InStock { get; set; }
        public string Product_Available { get; set; }
        public string Image_OfProduct { get; set; }

        public static List<Products> GetProducts(SqlConnection con)
        {
            List<Products> product = new List<Products>();

            SqlCommand cmd = new SqlCommand("SELECT ProductID, SKU, Product_Name, Unit_Cost, Available_Sizes," +
            " Units_InStock, Product_Available, Img_OfProduct FROM Product", con);

                cmd.CommandType = CommandType.Text;
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Products p = new Products();
                p.ProductID = Convert.ToInt32(rdr["ProductID"]);
                p.SKU = rdr["SKU"].ToString();
                p.Product_Name = rdr["Product_Name"].ToString();
                p.Unit_Cost = rdr["Unit_Cost"].ToString();
                p.Available_Sizes = rdr["Available_Sizes"].ToString();
                p.Units_InStock = Convert.ToInt32(rdr["Units_InStock"]);
                p.Product_Available = rdr["Product_Available"].ToString();
                p.Image_OfProduct = rdr["Img_OfProduct"].ToString();
                product.Add(p);
            }

            return product;
         }

         // Search
        public static List<Products> SearchProducts(SqlConnection con, string search)
        {
            List<Products> products = new List<Products>();

            SqlCommand cmd = new SqlCommand("with pg as (select ProductID, Product_Name, Unit_Cost, Available_Sizes, " + 
            "Units_InStock, Product_Available from Product where (Product_Name like @Search + '%')", con);
            cmd.CommandType = System.Data.CommandType.Text;

            
            cmd.Parameters.Add("@Search", SqlDbType.VarChar);

            cmd.Parameters["@Search"].Value = search == null ? "" : search;

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Products p = new Products();
                p.ProductID = Convert.ToInt32(rdr["ProductID"]);
                p.Product_Name = rdr["Product_Name"].ToString();
                p.Unit_Cost = rdr["Unit_Cost"].ToString();
                p.Available_Sizes = rdr["Available_Sizes"].ToString();
                p.Units_InStock = Convert.ToInt32(rdr["Units_InStock"]);
                p.Product_Available = rdr["Product_Available"].ToString();
                products.Add(p);
            }

            return products;
        }
           

    }
}