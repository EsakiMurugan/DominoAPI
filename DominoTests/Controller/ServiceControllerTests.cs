using Domino.Controllers;
using Domino.Data;
using Domino.Model;
using DominoTests.InMemory;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DominoTests.Controller
{
    public class ServiceControllerTests
    {
        [Fact]
        public async Task GetCartById()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var serviceController = new ServiceController(dbContext);
            Cart cart = new Cart()
            {
                CartID = 500,
                CartTypeID = "123451",
                PizzaID = 1,
                CustomerID = 201,
                Quantity = 1,
                UnitPrice = 10
            };
            //Act
            var result = await serviceController.GetCartById(500);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Cart>();
        }
       [Fact]
        public async Task ViewCart()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var serviceController = new ServiceController(dbContext);
            Customer customers = new Customer()
            {
                CustomerID = 201,
                CustomerName = "Esaki1",
                MobileNumber = 8508022066,
                EmailID = "esakimurugan1997@gmail.com",
                Address = "Nellai1",
                Password = "@Abcde123",
                CartTypeID = "null"
            };
            //Act
            var result = await serviceController.ViewCart(201);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Cart>>();
            //result.Should().BeEquivalentTo(cart);
        }
        [Fact]
        public async Task DeleteCart()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var serviceController = new ServiceController(dbContext);
            var CartID = 500;
            //Act
            var result = await serviceController.DeleteCart(CartID);
            //Assert
            result.Should().BeNull();
            dbContext.cart.Should().HaveCount(3);
        }
        [Fact]
        public async Task AddToCart_EditPizzaQuantity()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var serviceController = new ServiceController(dbContext);
            Cart cart = new Cart()
            {
                //CartID = 503,
                //CartTypeID = "123454",
                PizzaID = 14,
                CustomerID = 204,
                Quantity = 4,
                //UnitPrice = 40
            };
            //Act
            var result = await serviceController.AddToCart(cart);
            //Assert
            result.Value.Should().NotBeNull();
        }
        [Fact]
        public async Task AddToCart_AddNewPizza()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var serviceController = new ServiceController(dbContext);
            Cart cart = new Cart()
            {
                //CartID = 503,
                //CartTypeID = "123454",
                PizzaID = 13,
                CustomerID = 204,
                Quantity = 2,
                //UnitPrice = 40
            };
            //Act
            var result = await serviceController.AddToCart(cart);
            //Assert
            result.Value.Should().NotBeNull();
        }
        [Fact]
        public async Task AddToCart_NewCustomer()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var serviceController = new ServiceController(dbContext);
            Cart cart = new Cart()
            {
                //CartID = 503,
                //CartTypeID = "123454",
                PizzaID = 13,
                CustomerID = 205,
                Quantity = 2,
                //UnitPrice = 40
            };
            dbContext.customers.Add(
                      new Customer()
                      {
                          CustomerID = 205,
                          CustomerName = "Esaki",
                          MobileNumber = 8508022065,
                          EmailID = "esakimurugan1997@gmail.com",
                          Address = "Nellai",
                          Password = "@Abcde123",
                          CartTypeID = null
                      });
            await dbContext.SaveChangesAsync();
            //Act
           var result = await serviceController.AddToCart(cart);
            //Assert
            result.Value.Should().NotBeNull();
        }
        [Fact]
        public async Task PutCart()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var serviceController = new ServiceController(dbContext);
            var ID = 503;
            Cart Cart = new Cart()
            {
                CartID = ID,
                Quantity = 2,
                UnitPrice = 80
            };
            //Act
            var cart = await dbContext.cart.FindAsync(ID);
            dbContext.Entry<Cart>(cart).State = EntityState.Detached;
            var result = await serviceController.PutCart(Cart);
            //Assert
            result.Value.UnitPrice.Should().Be(Cart.UnitPrice);
            //dbContext.pizza.Should().HaveCount(4);
        }
        [Fact]
        public async Task UpdateTypeId()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var serviceController = new ServiceController(dbContext);
            var ID = 201;
            Customer customers = new Customer()
            {
                CustomerID = ID,
                CustomerName = "Esaki1",
                MobileNumber = 8508022066,
                EmailID = "esakimurugan1997@gmail.com",
                Address = "Nellai1",
                Password = "@Abcde123",
                CartTypeID = "123451"
            };
            //Act
            var Mock = await dbContext.customers.FindAsync(ID);
            dbContext.Entry<Customer>(Mock).State = EntityState.Detached;
            var result = await serviceController.UpdateTypeId(customers);
            //Assert
            result.Value.Should().NotBeNull();
            result.Value.CartTypeID.Should().Be(null);
        }
    }
}
