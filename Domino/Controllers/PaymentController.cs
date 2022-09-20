using Domino.Data;
using Domino.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Xml;

namespace Domino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly DominodbContext _context;
        public PaymentController(DominodbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("Payment")]
        //Cart Items are purchased and proceed to payment
        public async Task<ActionResult<Payment>> Payment(Payment payment)
        {
            Customer? customer = await (from i in _context.customers
                                       where i.CustomerID == payment.CustomerID
                                       select i).SingleOrDefaultAsync();
            List<Cart> cart = await (from i in _context.cart
                                     where i.CartTypeID == customer.CartTypeID
                                     select i).ToListAsync();
            payment.TotalAmount = 0;
            foreach (Cart c in cart)
            {
                payment.TotalAmount += c.UnitPrice;
                Pizza p = (from i in _context.pizza
                           where i.PizzaID == c.PizzaID
                           select i).Single();
                p.Stock -= c.Quantity;
            }
            _context.payments.Add(payment);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("GetPaymentById")]
        //Payment by Customer ID
        public async Task<ActionResult<List<Payment>>> GetPaymentById(int id)
        {

            var result = await (from i in _context.payments
                                where i.CustomerID == id
                                select i).ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpPost]
        [Route("Order")]
        //Receipt Table update
        public async Task<ActionResult<Receipt>> Order(Receipt r)
        {
            List<Cart> cart = new List<Cart>();
            var cust = await (from i in _context.customers
                              where i.CustomerID == r.CustomerID
                              select i).SingleOrDefaultAsync();
            var c = await (from i in _context.cart
                           where i.CartTypeID == cust.CartTypeID
                           select i).ToListAsync();
            foreach(var item in c)
            {
                Receipt receipt = new Receipt();
                receipt.CustomerID = item.CustomerID; 
                receipt.PizzaID = item.PizzaID;
                receipt.Quantity = item.Quantity;   
                receipt.UnitPrice = item.UnitPrice;
                receipt.ReceiptDate = DateTime.Today;
                _context.Add(receipt);
                await _context.SaveChangesAsync();
            }
            return Ok(); 
        }
        [HttpGet,Authorize]
        [Route("GetMyOrders")]
        //Get Order by Customer ID
        public async Task<ActionResult<List<Receipt>>> GetMyOrders(int id)
        {
            var History = await (from i in _context.receipts.Include(X => X.pizza)
                                 where i.CustomerID == id
                                 select i).ToListAsync();
            return Ok(History);
        }


    }
}
