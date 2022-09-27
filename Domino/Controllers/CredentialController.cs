using Domino.Data;
using Domino.Encode;
using Domino.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace Domino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DominodbContext db;
        public CredentialController(DominodbContext db, IConfiguration configuration)
        {
            this.db = db;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("CustomerRegn")]
        public async Task<ActionResult<Customer>> CustomerRegn(Customer? c)
        {
            var PasswordHash = Hashing.GetMd5Hash(c.Password);
            c.Password = PasswordHash;
            await db.customers.AddAsync(c);
            await db.SaveChangesAsync();
            return c;
        }
        [HttpPost]
        [Route("CustomerLogin")]
        public async Task<ActionResult<JWT>> CustomerLogin(Customer c)
        {
            var PasswordHash = Hashing.GetMd5Hash(c.Password);
            c.Password = PasswordHash;
            //c.CPassword = PasswordHash;

            JWT jwt = new JWT();
            try
            {
                var customer = (from i in db.customers
                                where i.EmailID == c.EmailID && i.Password == c.Password
                                select i).SingleOrDefault();
                if(customer != null)
                {
                    var authClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name , customer.EmailID),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    var token = GetToken(authClaims);
                    String s = new JwtSecurityTokenHandler().WriteToken(token);
                    jwt.customer = customer;
                    jwt.Token = s;
                    return jwt;
                }
                else
                {
                    return null;
                }
                return Unauthorized();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Wrong Entry");
            }
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                 issuer: _configuration["JWT:ValidIssuer"], //"https://localhost:7200"
                 audience: _configuration["JWT:ValidAudience"], //"User"
                 expires: DateTime.Now.AddMinutes(5),
                 claims: authClaims,
                 signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                 );
            return token;
        }
        [HttpPost]
        [Route("AdminLogin")]
        public async Task<ActionResult<Admin>> AdminLogin(Admin a)
        {
            try
            {
                var admin = (from i in db.admin
                             where i.EmailID == a.EmailID && i.Password == a.Password
                             select i).FirstOrDefault();

                if(admin == null)
                {
                    return BadRequest("Invalid Credentials");
                }
                return admin;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Wrong Entry");
            }
        }
       
    }
}
