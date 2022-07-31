using Microsoft.AspNetCore.Mvc;
using TomyChimmy.Models;
using TomyChimmy.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using TomyChimmy.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TomyChimmy.Controllers
{
    public class ShopController : Controller
    {
        //add db conection
        private readonly TomyChimmyDbContext _context;
        private readonly UserManager<User> _userManager;

        public ShopController(TomyChimmyDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Get: /Shop
        public IActionResult Index()
        {
            //Retorna una lista de categorias para que el usuario navegue
            var categories = _context.FoodTypes.OrderBy(c => c.Detalle).ToList();
            return View(categories);
        }

        //Get: /browse/catName
        public IActionResult Browse(string detalle)
        {
            //Store the selected category name in the ViewBag so we can display in the view Heading

            ViewBag.FoodType = detalle;

            //get the list of products for the selected category and pass the list to the View
            var products = _context.Foods.Where(p => p.FoodType.Detalle == detalle).OrderBy(p => p.Descripción).ToList();
            return View(products);
        }


        //Get: /ProductDetails/prodName
        public IActionResult ProductDetails(string products)
        {
            //Use a SingleOrDefault to find 1 exact match or a null object
            var selectedproduct = _context.Foods.SingleOrDefault(p => p.Descripción == products);
            return View(selectedproduct);
        }

        //Post: AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int Cantidad, int ID_Comidas)
        {
            //Create a variable for products in cart

            //Identify product price
            var product = _context.Foods.SingleOrDefault(p => p.ID_Comidas == ID_Comidas);
            var price = product.PrecioUnitario;
            //Determine Username
            var cartUsername = GetCartUserName();

            //Check IF this User's product already exist in the cart. If so, Update the quantity
            var cartItem = _context.Carts.SingleOrDefault(c => c.ID_Comidas == ID_Comidas && c.Username == cartUsername);
            if (cartItem == null)
            {
                //Create and save a new Cart Object
                var cart = new Cart
                {
                    ID_Comidas = ID_Comidas,
                    Cantidad = Cantidad,
                    PreciodeCarro = price * Cantidad,
                    Username = cartUsername
                };
                _context.Carts.Add(cart);
            }
            else
            {
                cartItem.Cantidad += Cantidad; //Add the new quantity to the existing quantity
                cartItem.PreciodeCarro += price * Cantidad;
                _context.Update(cartItem);
            }

             _context.SaveChanges();


            //Show the Cart page
            return RedirectToAction("Cart");
        }

        //Chek or set Cart username

        private string GetCartUserName()
        {
            //Check are we alredy stored with username in the User's session
            if (HttpContext.Session.GetString("CartUserName") != User.Identity.Name)
            {
                //Initialize an empty string variable that will later add to the Session object
                var cartUsername = "";

                //If no, Username in session there are no items in the cart yet, is user logged in?
                //If yes, use their email for the session variable
                if (User.Identity.IsAuthenticated)
                {
                    cartUsername = User.Identity.Name;
                }

                else
                {
                    //If no, use the GUID class to make a new ID and stor that in the Session
                    cartUsername = Guid.NewGuid().ToString();
                }
                //Next, store the CartUsername in a session var
                HttpContext.Session.SetString("CartUserName", cartUsername);
            }

            return HttpContext.Session.GetString("CartUserName");
        }

        public IActionResult Cart()
        {
            //Figure out who the user is
            var cartUsername = GetCartUserName();
            //Query the DB to get the User's cart Items
            var cartItems = _context.Carts.Include(c => c.Food).Where(c => c.Username == cartUsername).ToList();
            //Load a view to pass the cart items for display
            return View(cartItems);
        }

        public IActionResult RemoveFromCart(string id)
        {
            //get the object the user wants to delete
            var cartItem = _context.Carts.SingleOrDefault(c => c.CartId == id);

            //delete the object
            _context.Carts.Remove(cartItem);
            _context.SaveChanges();
            //redirect to updated cart page where deleted item is gone
            return RedirectToAction("Cart");
        }

        [Authorize]
        public IActionResult Checkout()//Set
        {
            //Check if the user has been shopping anonymously now they are logged in
            MigrateCart();

            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago");
            ViewData["Status_ID"] = new SelectList(_context.Statuses, "Status_ID", "Descripcion");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            //Nuevo objetivo, hacer que los id_producto del carrito coincidan con los de un queue detail asi es como se realizaría la venta de todo, luego borrar carrito si es posible

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout([Bind("Pedido_ID,UserId,Method_Id,FechaFactura,Subtotal,ValorImpuesto,Total,Nombres,Apellidos,Dirección,Status_ID")] Queue queue)//Get
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            queue.Nombres = user.Nombres;
            queue.Apellidos = user.Apellidos;
            queue.Dirección = user.Dirección;
            //autofill the date, User, and total properties instead of the user inputing these values
            queue.FechaFactura = DateTime.Now;
            queue.UserId = user.Id;
            var cartItems = _context.Carts.Where(c => c.Username == User.Identity.Name);
            decimal cartTotal = (from c in cartItems
                                 select c.PreciodeCarro).Sum();
            decimal cartImp = Math.Round(Convert.ToDecimal(((double)cartTotal) * 0.18), 2);
            decimal cartTotalImp = cartTotal + cartImp;
            queue.Subtotal = cartTotal;
            queue.ValorImpuesto = cartImp;
            queue.Total = cartTotalImp;
            //Will need and Extension to the .net core session object to store the Queue Object
            //HttpContext.Session.SetString("cartImp", cartImp.ToString());
            //HttpContext.Session.SetString("cartTotal", cartTotal.ToString());
            //HttpContext.Session.SetString("cartTotalImp", cartTotalImp.ToString());

            //We now have the session to the complex object

            HttpContext.Session.SetObject("Queue", queue);

            return RedirectToAction("SaleOrder");
        }

        private void MigrateCart()
        {
            //if user has shopped without an account. Attach their items to their user name
            if (HttpContext.Session.GetString("CartUserName") != User.Identity.Name)
            {
                var cartUsername = HttpContext.Session.GetString("CartUserName");
                //get the user's cart items
                var cartItems = _context.Carts.Where(c => c.Username == cartUsername);
                //loop through the cart items and update the username for each one
                foreach (var item in cartItems)
                {
                    item.Username = User.Identity.Name;
                    _context.Update(item);
                }
                _context.SaveChanges();

                //Update the session variable from a GUID to the user's email
                HttpContext.Session.SetString("CartUserName", User.Identity.Name);
            }
        }

        public IActionResult SaleOrder()
        {
            var queue = HttpContext.Session.GetObject<Queue>("Queue");

            _context.Add(queue);
            _context.SaveChanges();

            var cartUsername = HttpContext.Session.GetString("CartUserName");
            var cartItems = _context.Carts.Where(c => c.Username == cartUsername).ToList();

            foreach(var item in cartItems)
            {
                Models.Food articulos = _context.Foods.Find(item.ID_Comidas);
                var ValorUnitario = articulos.PrecioUnitario;
                //Create a new queueDetail
                var queueDetail = new QueueDetail
                {
                    Pedido_ID = queue.Pedido_ID,
                    ID_Comidas = item.ID_Comidas,
                    Cantidad = item.Cantidad,
                    ValorUnitario = ValorUnitario,
                    ValorTotal = item.PreciodeCarro
                };                                
                //Add to the database
                _context.QueueDetails.Add(queueDetail);                
            }
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                _context.Carts.Remove(item);
            }
            _context.SaveChanges();
           
            return RedirectToAction("Details", "Queues", new {id = queue.Pedido_ID});
        }

        /*public async Task<IActionResult> SaleOrder([Bind("InvoiceDetail_ID,ID_Comidas,Cantidad,ValorUnitario,ValorTotal,Invoice_ID")] QueueDetail queueDetail)
          {
              var cartUsername = HttpContext.Session.GetString("CartUserName");
              var cartItems = _context.Carts.Where(c => c.Username == cartUsername).ToList();
              //We check every cart and IF the username coincides with the identity name we convert that cart to a QueueDetail and the DELETE the cart
              foreach (var item in cartItems)
              {
                  queueDetail.Pedido_ID = queue.Pedido_ID;
                  queueDetail.Cantidad = item.Cantidad;
                  queueDetail.ID_Comidas = item.ID_Comidas;
                  Models.Food articulos = _context.Foods.Find(item.ID_Comidas);
                  queueDetail.ValorUnitario = articulos.PrecioUnitario;
                  queueDetail.ValorTotal = item.PreciodeCarro;

              }

              return View(queueDetail);
          }*/
    }
}
