using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAccess;

namespace Business
{
    public class CategoryLogic
    {
        public List<Category> GetAll()
        {
            var list = CategoryRepository.GetAll();
            
            // Get string date with format
            list = list.Select(x => { 
                x.Created = x.CreatedDate.ToString("yyyy/MM/dd hh:mm tt"); 
                return x; }
            ).ToList();

            return list;
        }

        public string Create(Category category)
        {
            category.CreatedBy = Environment.UserName;
            category.CreatedDate = DateTime.Now;

            var issaved = CategoryRepository.Create(category);
            var response = issaved ? "OK" : "NOK";

            return response;
        }

        public string Edit(Category category)
        {
            category.CreatedBy = Environment.UserName;
            category.CreatedDate = DateTime.Now;

            var issaved = CategoryRepository.Edit(category);
            var response = issaved ? "OK" : "NOK";

            return response;
        }

        public string Delete(int id)
        {            
            var issaved = CategoryRepository.Delete(id);
            var response = issaved ? "OK" : "NOK";

            return response;
        }
    }
}
