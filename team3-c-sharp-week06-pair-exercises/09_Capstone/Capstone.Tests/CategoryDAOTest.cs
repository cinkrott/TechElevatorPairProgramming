using Capstone.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Transactions;
using System.Collections.Generic;
using Capstone.Models;
using System.Data.SqlClient;
using System;

namespace Capstone.Tests
{
    [TestClass]
    public class CategoryDAOTest : ParentTest

    {
        [TestMethod]
        public void CanCreateCategoryObject()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(categoryDAO);
        }
        [TestMethod]

        public void GetAllCategoriesShouldReturnOneCategory()
        {
            //Arrange
            List<Category> categories = new List<Category>();

            //Act
            categories = categoryDAO.GetAllCategories();

            //Assert
            Assert.AreEqual(1, categories.Count);


        }

    }



}