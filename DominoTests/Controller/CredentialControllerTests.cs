using Domino.Controllers;
using Domino.Data;
using Domino.Model;
using DominoTests.InMemory;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DominoTests.Controller
{
    public class CredentialControllerTests
    {
        private readonly IConfiguration _configuration;
        public CredentialControllerTests(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [Fact]
        public async Task CustomerRegn()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var credentialController = new CredentialController(dbContext,_configuration);
            Customer customer = new Customer()
            {
                CustomerID = 205,
                CustomerName = "Esaki5",
                MobileNumber = 8508022070,
                EmailID = "esakimurugan1997@gmail.com",
                Address = "Nellai5",
                Password = "@Abcde123",
                CartTypeID = "12350"
            };
            //Act
            var result = await credentialController.CustomerRegn(customer);
            //Assert
            result.Value.Should().NotBeNull();
            dbContext.customers.Should().HaveCount(5);
        }
        [Fact]
        public async Task AdminLogin()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var credentialController = new CredentialController(dbContext, _configuration);
            Admin admin = new Admin()
            {
                //AdminID = 52,
                //AdminName = "ShopManager",
                EmailID = "pizzashop97@gmail.com",
                Password = "@Abcde123"
            };
            //Act
            var result = await credentialController.AdminLogin(admin);
            //Assert
            result.Value.Should().NotBeNull();
            dbContext.admin.Should().HaveCount(1);
        }



        [Fact]
        public async Task CustomerLogin()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var CredentialControllerTests = 
            
            
            var credentialController = new CredentialController(dbContext, _configuration);
            Customer customer = new Customer()
            {
                //AdminID = 52,
                //AdminName = "ShopManager",
                EmailID = "esakimurugan1997@gmail.com2",
                Password = "@Abcde123"
            };
            //Act
            var result = await credentialController.CustomerLogin(customer);
            //Assert
            result.Value.Should().NotBeNull();
            dbContext.admin.Should().HaveCount(1);
        }
        //[Fact]
        public async Task GetToken()
        {

        }
    }
}
