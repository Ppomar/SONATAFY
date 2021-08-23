using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Business;

namespace DataAccessUnitTet
{
    [TestClass]
    public class CategoryTest
    {
        [TestMethod]
        public void CreateCategory()
        {
            var category = new Category();
            category.Name = "Book";
            category.Description = "all reading material, in the book category";

            var business = new CategoryLogic();
            var response = business.CreateCategory(category);
        }
    }
}
