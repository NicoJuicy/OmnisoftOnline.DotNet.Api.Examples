using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;

using Omnisoft.Api.Connector.Services;
namespace Mvc.Controllers
{




    public class HomeController : Controller
    {
        public Omnisoft.Api.Connector.Services.ClientService clientService;
        //public Omnisoft.Api.Connector.Services.InvoiceService invoiceService;
        public Omnisoft.Api.Connector.Services.OrderService orderService;
        public Omnisoft.Api.Connector.Services.SimpleArticleService articleService;

        public HomeController()
        {
            clientService = new Omnisoft.Api.Connector.Services.ClientService("43cbdc18-0fd1-403f-bcf6-37f4adcbf045", "http://dev-api.omnisoftonline.be");
            // invoiceService = new Omnisoft.Api.Connector.Services.InvoiceService("43cbdc18-0fd1-403f-bcf6-37f4adcbf045", "http://dev-api.omnisoftonline.be");
            orderService = new Omnisoft.Api.Connector.Services.OrderService("43cbdc18-0fd1-403f-bcf6-37f4adcbf045", "http://dev-api.omnisoftonline.be");
            articleService = new Omnisoft.Api.Connector.Services.SimpleArticleService("43cbdc18-0fd1-403f-bcf6-37f4adcbf045", "http://dev-api.omnisoftonline.be");
        }

        public async Task<ActionResult> Index()
        {

            var clients = await clientService.AsyncList();
            return View("Clients", clients);
        }



        public async Task<ActionResult> AddClient()
        {

            return View(new Omnisoft.Domain.DTO.Client() { PrijsCode = 1, Active = true, Adres_Land = "BE", TaalCode = "NL", TaxCode = "3", TaxType = "B" });
        }

        [HttpPost]
        public async Task<ActionResult> AddClient(Omnisoft.Domain.DTO.Client client)
        {
            var returnedClient = await clientService.AsyncCreate(client);

            Console.WriteLine(returnedClient.Id);

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Orders()
        {
            return View(await orderService.AsyncList());
        }

        public async Task<ActionResult> AddOrder()
        {
            return View(new Omnisoft.Domain.DTO.Order());
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(Omnisoft.Domain.DTO.Order order)
        {
            var clients = await clientService.AsyncList();
            var articles = await articleService.AsyncList();
            var orders = await orderService.AsyncList();
            order = order.Randomize(clients.ToList(), orders.ToList(), articles.ToList());

            await orderService.AsyncCreate(order);
            return RedirectToAction("Orders");
        }


    }


    public static class RandomizeGenerator
    {

        public static Omnisoft.Domain.DTO.Order Randomize(this Omnisoft.Domain.DTO.Order order,
            List<Omnisoft.Domain.DTO.Client> clients,
            List<Omnisoft.Domain.DTO.Order> orders,
            List<Omnisoft.Domain.DTO.Article> articles)
        {
            //random client
            if (true)
            {
                order.Id = -1;
                int length = clients.Count();
                int randIndex = (int)((new Random()).Next(length));

                var randomClient = clients.ToList()[randIndex];
                order.ClientId = randomClient.Id;
                order.ClientName = randomClient.Naam;
                order.ClientPriceCode = randomClient.PrijsCode; // use default 1 ( when an article has multiple prices), normally has just 1 price
                order.CreatedOn = System.DateTime.UtcNow;
                order.Number = orders.Max(el => el.Number) + 1;
                order.OrderStateCode = "F"; // F for invoice, CN for creditnote
                order.TaxType = "B"; // B for VAT required, E for export, D for delivery in IC countries, I for Intracommunicatair, M for medecontractant, ...
                order.UniqueNumber = orders.Max(el => el.Number) + 1;
                order.IsPayed = false;
                order.ClientPriceCode = 1;
                order.OrderDate = System.DateTime.UtcNow;


                //add random articles to the invoice
                if (articles.Count() > 0)
                {
                    int randArtIndex = (int)((new Random()).Next(articles.Count()));
                    for (int i = 0; i < randArtIndex; i++)
                    {
                        var article = articles[randArtIndex];
                        order.OrderLines.Add(new Omnisoft.Domain.DTO.OrderLine()
                        {
                            ArticleDetailId = article.DetailId, // an article has 2 ids, but this is the most important one.
                            Amount = i,
                            ArticleCode = article.Code,
                            Comment = "",
                            Description = article.DescriptionNL,
                            VATPercentage1 = (decimal)21.0, //optional
                            UnitPrice = 50, //Price without tax
                            Unit = "Piece",
                            VATCode1 = "3" // 3 = 21%, 0 = 0%, 1 = 6%, 2 = 12% .
                        });
                    }
                }
                else
                {
                    throw new NotImplementedException("No articles available in the current dossier");
                }



            }

            return order;
        }
    }


}