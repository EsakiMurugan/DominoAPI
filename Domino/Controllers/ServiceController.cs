using Domino.Data;
using Domino.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Domino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly DominodbContext _context;
        public ServiceController(DominodbContext context)
        {
            _context = context; 
        }
        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> Getcart()
        {
            if (_context.cart == null)
            {
                return NotFound();
            }
            return await _context.cart.ToListAsync();
        }
        [HttpGet]
        [Route("GetCartById")]
        //Get Cart by Cart ID
        public async Task<Cart> GetCartById(int id)
        {
            var c = await _context.cart.Include(x => x.pizza).Where(x => x.CartID == id).Select(x => x).FirstOrDefaultAsync();
            return c;
        }
        
        [HttpGet("{id}")]
        // View Cart by Customer ID (My Cart)
        public async Task<List<Cart>> ViewCart(int id)
        {
            List<Cart> cart = new List<Cart>();
            //var Id = (from i in _context.customers
            //          where i.CustomerID == id
            //          select i.CartTypeID).FirstOrDefault();
            var Id = _context.customers.Where(x => x.CustomerID == id).Select(x => x.CartTypeID).FirstOrDefault();
            cart = _context.cart.Include(x => x.pizza).Where(x => x.CartTypeID == Id).ToList();
            return cart;
        }
        [HttpPut("{id}")]
        //Edit quantity option for already added item in cart
        public async Task<ActionResult> PutCart(Cart cart)
        {
            var c = await _context.cart.FindAsync(cart.CartID);
            c.Quantity = cart.Quantity;
            c.UnitPrice = c.Quantity*(from i in _context.pizza where i.PizzaID == c.PizzaID select i.Price).SingleOrDefault();
            _context.cart.Update(c);
            await _context.SaveChangesAsync();
            return Ok(c);
        }
        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        //Delete Cart by Cart ID
        public async Task<IActionResult> DeleteCart(int? id)
        {
            var c = await _context.cart.Include(x => x.pizza).Where(x => x.CartID == id).Select(x => x).FirstOrDefaultAsync();
            _context.cart.Remove(c);
            await _context.SaveChangesAsync();
            return Ok(c);
        }
        private bool CartExists(int id)
        {
            return (_context.cart?.Any(e => e.CartID == id)).GetValueOrDefault();
        }
        [HttpPost]
        [Route("AddToCart")]
        public async Task<ActionResult<Cart>> AddToCart(Cart cart)
        {
            var ID = await (from i in _context.customers where i.CustomerID == cart.CustomerID select i.CartTypeID).FirstOrDefaultAsync();
            var check = await (from i in _context.cart
                               where i.CartTypeID == ID && i.PizzaID == cart.PizzaID
                               select i).SingleOrDefaultAsync();
            if (check == null)
            {
                var customer = await (from i in _context.customers
                                      where i.CustomerID == cart.CustomerID
                                      select i).SingleOrDefaultAsync();
                cart.CartTypeID = customer.CartTypeID;
                if (cart.CartTypeID == null)
                {
                    Guid obj = Guid.NewGuid();
                    //Console.WriteLine("New Guid is " + obj.ToString());
                    cart.CartTypeID = obj.ToString();
                    customer.CartTypeID = obj.ToString();
                }
                cart.UnitPrice = cart.Quantity * (from i in _context.pizza
                                                  where i.PizzaID == cart.PizzaID
                                                  select i.Price).SingleOrDefault();
            }
            else
            {
                check.Quantity += cart.Quantity;
                check.UnitPrice = check.Quantity * (from i in _context.pizza
                                                   where i.PizzaID == cart.PizzaID
                                                   select i.Price).SingleOrDefault();
                await _context.SaveChangesAsync();
                return check;
            }
            _context.cart.Add(cart);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCart", new { id = cart.CartID }, cart);
        }
       
        [HttpPut]
        [Route("UpdateTypeId")]
        public async Task<ActionResult<Customer>> UpdateTypeId(Customer c)
        {
            var update = await _context.customers.FindAsync(c.CustomerID);
            update.CartTypeID = null;
            _context.customers.Update(update);
            await _context.SaveChangesAsync();  
            return null;
        }
    }
}


