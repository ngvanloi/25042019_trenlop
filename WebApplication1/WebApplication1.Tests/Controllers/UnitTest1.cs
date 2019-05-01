using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WebApplication1.Controllers;
using WebApplication1.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class TestVLTeaControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new CS4PEEntities();
            var controller = new VLTeaController();

            var result = controller.Index();
            var view = result as ViewResult;

            Assert.IsNotNull(view);
            var model = view.Model as List<BubleTea>;
            Assert.IsNotNull(model);
            Assert.AreEqual(db.BubleTeas.Count(), (model as List<BubleTea>).Count);
        }

        [TestMethod]
        public void TestDetail()
        {
            var db = new CS4PEEntities();
            var item = db.BubleTeas.First();
            var controller = new VLTeaController();

            var result = controller.Details(item.id);
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            var model = view.Model as BubleTea;
            Assert.IsNotNull(model);
            Assert.AreEqual(item.id, model.id);

            result = controller.Details(0);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));

        }

        [TestMethod]
        public void TestCreate()
        {
            var controller = new VLTeaController();
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestCreateP()
        {
            var db = new CS4PEEntities();
            var model = new BubleTea { Name = "Tra sua VL", Price = 25000, Topping = "tran chau trang" };
            var controller = new VLTeaController();
            var result = controller.Create(model);
            var redirect = result as RedirectToRouteResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.RouteValues["action"]);

            var item = db.BubleTeas.Find(model.id);
            Assert.IsNotNull(item);
            Assert.AreEqual(model.Name, item.Name);
            Assert.AreEqual(model.Price, item.Price);
            Assert.AreEqual(model.Topping, item.Topping);
        }


        [TestMethod]
        public void TestEdit()//Covered 83,33%
        {
            var db = new CS4PEEntities();
            var item = db.BubleTeas.First();
            var controller = new VLTeaController();

            var result = controller.Edit(item.id);
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            var model = view.Model as BubleTea;
            Assert.IsNotNull(model);
            Assert.AreEqual(item.id, model.id);

            var result_0 = controller.Edit(0);
            Assert.IsInstanceOfType(result_0, typeof(HttpStatusCodeResult));
            Assert.IsInstanceOfType(result_0, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestEditP()//Covered 54,55%
        {
            var db = new CS4PEEntities();
            var model = new BubleTea { Name = "Tra sua VLU", Price = 25000, Topping = "tran chau trang" };
            var controller = new VLTeaController();
            var result = controller.Edit(model);
            //var redirect = result as RedirectToRouteResult;
            //Assert.IsNotNull(redirect);
            //Assert.AreEqual("Index", redirect.RouteValues["action"]);

            //var item = db.BubleTeas.Find(model.id);
            //Assert.IsNotNull(item);
            //Assert.AreEqual(model.Name, item.Name);
            //Assert.AreEqual(model.Price, item.Price);
            //Assert.AreEqual(model.Topping, item.Topping);
        }

        [TestMethod]
        public void TestDelete()//Covered 83,33%
        {
            var db = new CS4PEEntities();
            var controller = new VLTeaController();
            var item = db.BubleTeas.First();
            var result = controller.Delete(item.id);
            var view = result as ViewResult;
            Assert.IsNotNull(view);

            var model = db.BubleTeas.Find(item.id) as BubleTea;
            Assert.IsNotNull(model);
            Assert.AreEqual(item.id, model.id);

            var model_0 = controller.Delete(0);
            Assert.IsInstanceOfType(model_0, typeof(HttpStatusCodeResult));
            Assert.IsInstanceOfType(model_0, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestDeleteConfirmed()//Covered 100%
        {
            var db = new CS4PEEntities();
            var controller = new VLTeaController();

            var item = db.BubleTeas.First();
            var result = controller.DeleteConfirmed(item.id) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);



        }



    }
}
