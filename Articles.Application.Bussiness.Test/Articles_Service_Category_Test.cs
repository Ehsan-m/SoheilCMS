﻿using System;
using System.Collections.Generic;
using System.Linq;
using Articles.Application.BussinessService;
using Articles.Contracts;
using Articles.Data.DataRepository;
using Articles.DomainModel;
using Articles.IOC.Bootstraper;
using FrameWork.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace Articles.Application.Bussiness.Test
{
    [TestClass]
    public class Articles_Service_Category_Test
    {
        ICategoryService serviceMain;
        [TestInitialize]
        public void Initialize()
        {
            using (var kernel = new StandardKernel(new DataAccessModule(), new ArticleServiceModule()))
            {
                serviceMain = kernel.Get<ICategoryService>();
            }
        }


        [TestMethod]
        public void Articles_Service_Category_Create_Test()
        {
            Mock<ICategoryRepository> rep = new Mock<ICategoryRepository>();

            rep.Setup(a => a.GetNextId()).Returns(1);


            // Category model = new Category("Test","Test",true,"");
            CategoryModel model = new CategoryModel()
            {
                IsParent = true,
                LineAge = "Test",
                Name = "Test",
                PostCount = 0,
                ParentId = 1,
                Slug = "Test"
            };
            model.Id = rep.Object.GetNextId();

            var cat = model.ToCategory();
            rep.Setup(a => a.Create(cat)).Returns(EntityAction.Added);
            //rep.Verify(a => a.Create(cat));
           // rep.SetupSet(a => a.Create(cat)).Verifiable();



            ICategoryService service = new CategoryService(rep.Object);

            var result = service.Create(model);


            Assert.AreEqual(result, EntityAction.None);
        }
        [TestMethod]
        public void Articles_Service_Category_Update_Test()
        {
            Mock<ICategoryRepository> rep = new Mock<ICategoryRepository>();



            CategoryModel model = new CategoryModel();
            
            model.Id = rep.Object.GetNextId();
            var current = model.ToCategory();
            rep.Setup(a => a.Update(current)).Returns(EntityAction.Updated);



            ICategoryService service = new CategoryService(rep.Object);

            var result = service.Update(model);


            Assert.AreEqual(result, EntityAction.None);
        }

        [TestMethod]
        public void Articles_Service_Category_Get_Test()
        {
            Mock<ICategoryRepository> rep = new Mock<ICategoryRepository>();

            rep.Setup(a => a.GetNextId()).Returns(1);


            Category model = new Category("Test", "Test", true, "");
            model.Id = rep.Object.GetNextId();
            rep.Setup(a => a.Get(1)).Returns(model);



            ICategoryService service = new CategoryService(rep.Object);

            var result = service.Get(1);


            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Articles_Service_Category_Where_Test()
        {



            var result = serviceMain.Where(a => a.Id == 1).ToList();


            Assert.IsTrue(result.Count == 0);
        }
        [TestMethod]
        public void Articles_Service_Category_Delete_Test()
        {
            Mock<ICategoryRepository> rep = new Mock<ICategoryRepository>();

            rep.Setup(a => a.Delete(1)).Returns(EntityAction.Deleted);


            ICategoryService service = new CategoryService(rep.Object);

            var result = service.Delete(1);


            Assert.AreEqual(EntityAction.Deleted, result);
        }
    }
}
