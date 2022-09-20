using Domino.Data;
using Domino.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Domino.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly DominodbContext db;
        public PizzaController(DominodbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Pizza>>> GetAllPizza()
        {
            try
            {
                return await db.pizza.ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetById(int id)
        {
            try
            {
                var result = await db.pizza.FindAsync(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Pizza>> AddPizza(Pizza p)
        {
            try
            {
                await db.pizza.AddAsync(p);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetAllPizza",p);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Pizza>> EditPizza(Pizza p)
        {
            try
            {
                db.pizza.Update(p);
                await db.SaveChangesAsync();
                return p;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pizza>> DeletePizza(int id)
        {
            try
            {
                var p = await db.pizza.FindAsync(id);

                if (p == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }
                db.pizza.Remove(p);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetAllPizza", p);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
