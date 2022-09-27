using Castle.Core.Resource;
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
    public class PaymentControllerTests
    {
        [Fact]
        public async Task Payment()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var paymentController = new PaymentController(dbContext);
            Payment payment = new Payment()
            {
                //PaymentID = 300,
                CustomerID = 201,
                CardNumber = 56789,
                //TotalAmount = 501
            };
            //Act
            var result = await paymentController.Payment(payment);
            var expected = dbContext.pizza.FindAsync(11).Result?.Stock;
            //Assert
            //var temp = dbContext.pizza;
            result.Value.Should().NotBeNull();
            expected.Should().Be(0);
         }
        [Fact]
        public async Task GetPaymentById()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var paymentController = new PaymentController(dbContext);
            Payment payment = new Payment()
            {
                PaymentID = 301,
                CustomerID = 201,
                CardNumber = 12345,
                TotalAmount = 510,
            };
            //Act
            var result = await paymentController.GetPaymentById(201);
            var expected = result.Value?[0].TotalAmount;
            //Assert
            result.Value.Should().NotBeNull();
            expected.Should().Be(payment.TotalAmount);
        }
        [Fact]
        public async Task Order()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var paymentController = new PaymentController(dbContext);
            Receipt receipt = new Receipt()
            {
                CustomerID = 201,
            };
            //Act
            var result = await paymentController.Order(receipt);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<Receipt>>();
            dbContext.receipts.Should().HaveCount(1);
        }
        [Fact]
        public async Task GetMyOrders()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var paymentController = new PaymentController(dbContext);
            Receipt receipt = new Receipt()
            {
                CustomerID = 201,
            };
            //Act
            await paymentController.Order(receipt);
            var result = await paymentController.GetMyOrders(201);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<List<Receipt>>>();
            result.Value.Should().HaveCount(1);
        }
    }
}
