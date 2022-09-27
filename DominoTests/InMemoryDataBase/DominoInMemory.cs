using Domino.Data;
using Domino.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoTests.InMemory
{
    public class DominoInMemory
    {
        public async Task<DominodbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DominodbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var databaseContext = new DominodbContext(options);
            databaseContext.Database.EnsureCreated();
            int id = 500;
            int Cid = 200;
            int Pid = 10;
            int pid = 300;
            for (int i = 1; i < 5; i++)
            {
                databaseContext.payments.Add(
                    new Payment()
                    {
                        PaymentID = pid + i,
                        CustomerID = Cid + i,
                        CardNumber = 12345 + i,
                        TotalAmount = 500 + (i * 10)
                    });
                databaseContext.cart.Add(
                    new Cart()
                    {
                        CartID = id++,
                        CartTypeID = "12345" + i,
                        PizzaID = Pid + i,
                        CustomerID = Cid + i,
                        Quantity = 1,
                        UnitPrice = i * 10
                    });
                databaseContext.customers.Add(
                   new Customer()
                   {
                       CustomerID = Cid + i,
                       CustomerName = "Esaki" + i,
                       MobileNumber = 8508022065 + i,
                       EmailID = "esakimurugan1997@gmail.com" + i,
                       Address = "Nellai" + i,
                       Password = "36c66b8d686f1ef9d5c97e48d53ab3c9",
                       CartTypeID = "12345" + i
                   });
                databaseContext.pizza.Add(
                   new Pizza()
                   {
                       PizzaID = Pid + i,
                       PizzaName = "Margarita" + i,
                       Price = i * 10,
                       Stock = i,
                   });
            }
            databaseContext.admin.Add(
                   new Admin()
                   {
                       AdminID = 51,
                       AdminName = "ShopManager",
                       EmailID = "pizzashop97@gmail.com",
                       Password = "@Abcde123"
                   });
            await databaseContext.SaveChangesAsync();
            return databaseContext;
        }
    }
}
