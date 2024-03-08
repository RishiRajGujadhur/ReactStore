using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Store.API.Controllers;

namespace Store.Store.UnitTests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {  
        private AccountController _accountController;
        // UserManager<User> userManager, TokenService tokenService, 
        // StoreContext context;
        
        [SetUp]
        public void Setup()
        {
        }

        // private AccountController CreateAccountController()
        // {
        //     return new AccountController(
        //         userManager.Object,
        //         tokenService.Object,
        //         context.Object);
        // }

        [Test]
        public async Task GetSavedAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            // Act
            // Assert
            //Assert.NotNull(result);
            //Assert.AreEqual(1, validationResults.Count);
        }
    }
}