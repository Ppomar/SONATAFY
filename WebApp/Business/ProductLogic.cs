using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAccess;

namespace Business
{
    public class ProductLogic
    {
        public List<Product> GetAll()
        {
            var list = ProductRepository.GetAll();

            // Get string date with format
            list = list.Select(x => {
                x.Created = x.CreatedDate.ToString("yyyy/MM/dd hh:mm tt");
                return x;
            }
            ).ToList();

            return list;
        }

        public List<Product> FilterProducts(string productName)
        {
            var list = ProductRepository.FilterProducts(productName);

            // Get string date with format
            list = list.Select(x => {
                x.Created = x.CreatedDate.ToString("yyyy/MM/dd hh:mm tt");                
                return x;
            }
            ).ToList();

            return list;
        }

        private static decimal CalculateTaxe(int categoryId, decimal price)
        {
            decimal taxe = 0;
            switch (categoryId)
            {
                case 4:
                    taxe = Decimal.Round((price * Convert.ToDecimal("0.10")), 2);
                    break;
                default:
                    break;
            }

            return taxe;

        }

        public Product GetById(int id)
        {
            var list = ProductRepository.GetById(id);

            // Get string date with format
            var product = list.Select(x => {
                x.Created = x.CreatedDate.ToString("yyyy/MM/dd hh:mm tt");
                x.Taxe = Decimal.Round(CalculateTaxe(x.CategoryId, x.Price), 1);
                return x;
            }
            ).FirstOrDefault();

            return product;
        }

        public string Create(Product product)
        {
            product.CreatedBy = Environment.UserName;
            product.CreatedDate = DateTime.Now;

            var issaved = ProductRepository.Create(product);
            var response = issaved ? "OK" : "NOK";

            return response;
        }

        public string Edit(Product product)
        {
            product.CreatedBy = Environment.UserName;
            product.CreatedDate = DateTime.Now;

            var issaved = ProductRepository.Edit(product);
            var response = issaved ? "OK" : "NOK";

            return response;
        }

        public string Delete(int id)
        {
            var issaved = ProductRepository.Delete(id);
            var response = issaved ? "OK" : "NOK";

            return response;
        }
    }
}
