using MasVidaWebMVC.Controllers;
using MasVidaWebMVC.Models;
using MasVidaWebMVC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace MasVidaWebTest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void UserDetailsView()
        {
            var controller = new UsersController();
            var result = controller.Details(5547) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void UserDetailsViewData()
        {
            var controller = new UsersController();
            var result = controller.Details(5547) as ViewResult;
            var user = (User)result.ViewData.Model;
            Assert.AreEqual("Corradini", user.LastName);
        }

        [TestMethod]
        public void User_No_Deberia_Ser_Valido_Con_DNI_Letras()
        {
            //Arrange
            User user = new User
            {
                Name = "Manuel",
                LastName = "Perez",
                DNI = "DNISSDLSS",
                UserTypeID = 2005,
            };

            // Act
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(user, context,results,true);

            // Assert
            Assert.IsFalse(isValid);
        }
        [TestMethod]
        public void User_No_Deberia_Ser_Valido_Con_DNI_Mayor_O_Igual_A_100_millones()
        {
            //Arrange
            User user = new User
            {
                Name = "Manuel",
                LastName = "Perez",
                DNI = "123456789",
                UserTypeID = 2005,
            };

            // Act
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(user, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void User_No_Deberia_Ser_Valido_Sin_Nombre()
        {
            //Arrange
            User user = new User
            {
                LastName = "Perez",
                DNI = "12345678",
                UserTypeID = 2005,
            };

            // Act
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(user, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
        }


        [TestMethod]
        public void User_Deberia_Ser_Valido_Con_Todos_Los_Datos_Correctos()
        {
            //Arrange
            User user = new User
            {
                Name = "Manuel",
                LastName = "Perez",
                DNI = "12345678",
                UserTypeID = 2005,
            };

            // Act
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(user, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        //[TestMethod]
        //public void TestUsersLogin()
        //{
        //    var model = new LoginModel { Password = "123", UserName = "admin" };
        //    var returnUrl = "/Home/index";
        //    RedirectResult redirectResult;
        //    bool isAuthenticationCalled = false;
        //    bool isValidateUserCalled = false;

        //    using (MasVidaDataContext.Create())

        //    UsersController usersController = new UsersController();
        //}
    }
}
