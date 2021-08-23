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
            return list;
        }
    }
}
