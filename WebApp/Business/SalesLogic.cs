using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAccess;

namespace Business
{
    public class SalesLogic
    {
        public Tuple<string, List<Product>> CreatePurchase(List<Product> products)
        {            
            var productsForTicket = new List<Product>();

            productsForTicket = products.GroupBy(
                pr => new { pr.Id, pr.Name }, (pr, prds) => new Product() {
                    Id = pr.Id,
                    Name = pr.Name,
                    Price = prds.Where(x => x.Id == pr.Id && x.Name == pr.Name).FirstOrDefault().Price,
                    Quantity = prds.Where(x => x.Id == pr.Id && x.Name == pr.Name).Count(),
                    Total = Decimal.Round(prds.Where(x => x.Id == pr.Id && x.Name == pr.Name).Select(y => { return y.Price; }).Sum(), 2),
                    Taxe = Decimal.Round(prds.Where(x => x.Id == pr.Id && x.Name == pr.Name).Select(y => { return y.Taxe; }).Sum(), 1),
                }
                ).ToList();

            productsForTicket = productsForTicket.Select(x => {
                x.CreatedBy = Environment.UserName;
                x.CreatedDate = DateTime.Now;

                return x;
            }).ToList();

            var issaved = SalesRepository.CreatePurchase(productsForTicket);
            var response = issaved ? "OK" : "NOK";           

            return new Tuple<string, List<Product>>(response, productsForTicket);
        }
    }
}
