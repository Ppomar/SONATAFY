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
        public List<Category> GetCategories()
        {
            var list = CategoryRepository.GetAll();
            
            // Get string date with format
            list = list.Select(x => { 
                x.Created = x.CreatedDate.ToString("yyyy/MM/dd hh:mm tt"); 
                return x; }
            ).ToList();

            return list;
        }

        public string CreateCategory(Category category)
        {
            category.CreatedBy = Environment.UserName;
            category.CreatedDate = DateTime.Now;

            var issaved = CategoryRepository.CreateCategory(category);
            var response = issaved ? "OK" : "NOK";

            return response;
        }

        public string EditCategory(Category category)
        {
            category.CreatedBy = Environment.UserName;
            category.CreatedDate = DateTime.Now;

            var issaved = CategoryRepository.EditCategory(category);
            var response = issaved ? "OK" : "NOK";

            return response;
        }

        public string DeleteCategory(int id)
        {            
            var issaved = CategoryRepository.DeleteCategory(id);
            var response = issaved ? "OK" : "NOK";

            return response;
        }
    }
}
