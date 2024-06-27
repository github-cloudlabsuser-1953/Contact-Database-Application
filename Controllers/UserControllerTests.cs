using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _controller;
        private List<User> _userlist;

        [TestInitialize]
        public void Setup()
        {
            _controller = new UserController();
            _userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" },
                new User { Id = 3, Name = "Bob", Email = "bob@example.com" }
            };
            UserController.userlist = _userlist;
        }

        [TestMethod]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_userlist, result.Model);
        }

        [TestMethod]
        public void Details_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var userId = 2;

            // Act
            var result = _controller.Details(userId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_userlist.FirstOrDefault(u => u.Id == userId), result.Model);
        }

        [TestMethod]
        public void Details_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var userId = 4;

            // Act
            var result = _controller.Details(userId) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var user = new User { Id = 4, Name = "Alice", Email = "alice@example.com" };

            // Act
            var result = _controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Create_InvalidUser_ReturnsViewWithUser()
        {
            // Arrange
            var user = new User { Id = 4, Name = "", Email = "alice@example.com" };

            // Act
            var result = _controller.Create(user) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result.Model);
        }

        [TestMethod]
        public void Edit_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var userId = 2;

            // Act
            var result = _controller.Edit(userId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_userlist.FirstOrDefault(u => u.Id == userId), result.Model);
        }

        [TestMethod]
        public void Edit_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var userId = 4;

            // Act
            var result = _controller.Edit(userId) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var userId = 2;
            var user = new User { Id = userId, Name = "Jane Doe", Email = "jane.doe@example.com" };

            // Act
            var result = _controller.Edit(userId, user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Edit_InvalidUser_ReturnsViewWithUser()
        {
            // Arrange
            var userId = 2;
            var user = new User { Id = userId, Name = "", Email = "jane.doe@example.com" };

            // Act
            var result = _controller.Edit(userId, user) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result.Model);
        }

        [TestMethod]
        public void Delete_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var userId = 2;

            // Act
            var result = _controller.Delete(userId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_userlist.FirstOrDefault(u => u.Id == userId), result.Model);
        }

        [TestMethod]
        public void Delete_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var userId = 4;

            // Act
            var result = _controller.Delete(userId) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteConfirmed_ExistingUserId_RedirectsToIndex()
        {
            // Arrange
            var userId = 2;

            // Act
            var result = _controller.Delete(userId, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
