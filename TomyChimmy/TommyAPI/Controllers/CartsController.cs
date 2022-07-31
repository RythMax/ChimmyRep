using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TommyAPI.Data;
using TommyAPI.Extensions;
using TommyAPI.Models;

namespace TommyAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly TommyAPIContext _context;

        public CartsController(TommyAPIContext context)
        {
            _context = context;
        }


        //Esto deberá retornar una lista de categoría en un futuro
        [HttpGet("/FoodType")]
        public async Task<ActionResult<IEnumerable<FoodType>>> GetFoodTypes()
        {
            var categories = await _context.FoodTypes.OrderBy(c => c.Detalle).ToListAsync();
            return categories;
        }

        [HttpGet("/Food")]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods(string foodType)
        {
            var products = await _context.Foods.Where(f => f.FoodType.Detalle == foodType).OrderBy(f => f.Descripción).ToListAsync();
            return products;
        }

        //Debo hacer una funcion que me devuelva los productos que hay en la tienda

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        //Esta funcion debe retornar un carrito con la ID correcta, asi que debe quedarse igual
        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(string id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        //Esta función modifica un carrito ya existente, en otras palabras esta función también será llamada
        // PUT: api/Carts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(string id, Cart cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //Sin embargo esta funcion debe ser fuertemente alterada para poder hacer un carrito, esta funcion tambien debe
        //ser capaz de modificar el carrito si existe ese producto, llamando el modify puede ser un buen juego 
        // POST: api/Carts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Cart>> PostCart(int foodId, int foodQty) //  Cart cart
        {
            //Revisar si hay un carrito similar
            //Crear el objeto cart
            //Buscar la comida
            //agregar comida y cantidad al cart
            //agregar username al cart
            //guardar en BD
            //retornar el cart

            var product = _context.Foods.SingleOrDefault(p => p.ID_Comidas == foodId);
            var price = product.PrecioUnitario;
            var cartUsername = GetUsername();

            var cartItem = _context.Carts.SingleOrDefault(c => c.ID_Comidas == foodId && c.Username == cartUsername);
            if(cartItem == null)
            {
                var newCart = new Cart
                {
                    Cantidad = foodQty,
                    ID_Comidas = foodId,
                    Food = _context.Foods.FindAsync(foodId).Result,
                    Username = GetUsername(),
                    PreciodeCarro = _context.Foods.FindAsync(foodId).Result.PrecioUnitario * foodQty
                };

                try
                {
                    _context.Carts.Add(newCart);
                    await _context.SaveChangesAsync();
                    return Ok(newCart);
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
            else
            {
                cartItem.Cantidad += foodQty;
                cartItem.PreciodeCarro += _context.Foods.FindAsync(foodId).Result.PrecioUnitario * foodQty;
                _context.Update(cartItem);
                await _context.SaveChangesAsync();
                return Ok(cartItem);
            }
                        
        }
        
        //Hay que mantener esta funcion
        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cart>> DeleteCart(string id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return cart;
        }

        private bool CartExists(string id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }

        //Debo crear una funcion que filtre el carro por el username

        //probando algo, fallo, no usar Session
        //Fallo x2, no recibiras nada si usas User Identity Name, debes usar Claims
        //Asi se puede conseguir el username.[HttpGet("/Prueba")]
        //Para evitar el mismo problema debes poner este metodo privado, debido a que espera 
        //un link y eso hace explote
        private string GetUsername()
        {
            var userid = HttpContext.GetUserId();
            var user = _context.Users.SingleOrDefault(p => p.Id == userid);
            var username = user.UserName;
            return username;
        }

        [HttpGet("/api/Shop/GetUserCarts")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetUserCarts()
        {
            var username = GetUsername();
            var carts = await _context.Carts.Include(c => c.Food).Where(c => c.Username == username).ToListAsync();
            if (carts == null) { return NotFound(); }
            else { return carts; }
        }

        [HttpPost("/Queue")]
        public async Task<ActionResult<Queue>> Checkout(int methodtype)
        {
            var userid = HttpContext.GetUserId();
            var user = _context.Users.SingleOrDefault(p => p.Id == userid);
            var cartItems = _context.Carts.Where(c => c.Username == user.UserName);
            decimal cartTotal = (from c in cartItems
                                 select c.PreciodeCarro).Sum();
            decimal cartImp = Math.Round(Convert.ToDecimal(((double)cartTotal) * 0.18), 2);
            decimal cartTotalImp = cartTotal + cartImp;
            var queue = new Queue
            {
                Nombres = user.Nombres,
                Apellidos = user.Apellidos,
                Dirección = user.Dirección,
                FechaFactura = DateTime.Now,
                UserId = userid,
                Subtotal = cartTotal,
                ValorImpuesto = cartImp,
                Method_Id = methodtype,
                Status_ID = 1,
                Total = cartTotalImp
            };

            _context.Queues.Add(queue);
            await _context.SaveChangesAsync();

            var cartUsername = GetUsername();
            var cartItem = _context.Carts.Where(c => c.Username == cartUsername).ToList();

            foreach(var item in cartItem)
            {
                Food articulos = _context.Foods.Find(item.ID_Comidas);
                var ValorUnitario = articulos.PrecioUnitario;

                var queueDetail = new QueueDetail
                {
                    Pedido_ID = queue.Pedido_ID,
                    ID_Comidas = item.ID_Comidas,
                    Cantidad = item.Cantidad,
                    ValorUnitario = ValorUnitario,
                    ValorTotal = item.PreciodeCarro
                };
                _context.QueueDetails.Add(queueDetail);
            }
            await _context.SaveChangesAsync();

            foreach (var item in cartItem)
            {
                _context.Carts.Remove(item);
            }
            await _context.SaveChangesAsync();

            return Ok(queue);
        }


    }
}
