using Domino.Controllers;
using Domino.Data;
using Domino.Model;
using DominoTests.InMemory;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoTests.Controller
{
    public class PizzaControllerTests
    {
        [Fact]
        public async Task GetAllPizza()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var pizzaController = new PizzaController(dbContext);
            //Act
            var result = await pizzaController.GetAllPizza();   
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<List<Pizza>>>();
            int actualresult = result.Value.Count();
            int expectedresult = 4;
            Assert.Equal(expectedresult, actualresult); 
        }
        [Fact]
        public async Task GetById()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var pizzaController = new PizzaController(dbContext);
            Pizza Pizza = new Pizza()
            {
                PizzaID = 11,
                PizzaName = "Margarita1",
                Price = 10,
                Stock = 1
            };
            //Act
            var result = await pizzaController.GetById(11);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<Pizza>>();
        }
        [Fact]
        public async Task AddPizza()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var pizzaController = new PizzaController(dbContext);
            Pizza Pizza = new Pizza()
            {
                PizzaID = 15,
                PizzaName = "Margarita5",
                Price = 50,
                Stock = 5
            };
            //Act
            var result = await pizzaController.AddPizza(Pizza);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<Pizza>>();
            result.Value.PizzaName.Should().BeEquivalentTo(Pizza.PizzaName);
            dbContext.pizza.Should().HaveCount(5);
        }
        [Fact]
        public async Task EditPizza()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var pizzaController = new PizzaController(dbContext);
            var ID = 12;
            Pizza Pizza = new Pizza()
            {
                PizzaID = ID,
                PizzaName = "Naepolitan2",
                Price = 20,
                Stock = 2
            };
            //Act
            var pizza = await dbContext.pizza.FindAsync(ID);    
            dbContext.Entry<Pizza>(pizza).State = EntityState.Detached;
            var result = await pizzaController.EditPizza(Pizza);
            //Assert
            result.Value.Should().BeEquivalentTo(Pizza);
            dbContext.pizza.Should().HaveCount(4);
        }
        [Fact]
        public async Task DeletePizza()
        {
            //Arrange
            var InMemory = new DominoInMemory();
            var dbContext = await InMemory.GetDatabaseContext();
            var pizzaController = new PizzaController(dbContext);
            var PizzaID = 14;
            //Act
            var result = await pizzaController.DeletePizza(PizzaID);
            //Assert
            result.Should().BeNull();
            dbContext.pizza.Should().HaveCount(3);
        }
    }
}
