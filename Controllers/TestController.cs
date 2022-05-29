using Microsoft.AspNetCore.Mvc;
using ApiCuistot.Models;

namespace ApiCuistot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController: Controller
    {
        private readonly DatabaseContext Context;

        public TestController(DatabaseContext context)
        {
            Context = context;
        }
        [Route("/getConso")]
        [HttpGet]
        public List<Conso> getConso()
        {
            return Context.getConso();
        }
        [Route("/getOrder")]
        [HttpGet]
        public List<Commande> getOrder()
        {
            return Context.getOrder();
        }

        [Route("/getContenir")]
        [HttpGet]
        public List<Contenir> getContenir()
        {
            return Context.getContenir();
        }

        [Route("/insertConso")]
        [HttpPost]
        public int insertConso(string nom)
        {
            return Context.insertConso(nom);
        }

        [Route("/startNewOrder")]
        [HttpPost]
        public int startNewOrder(string nom)
        {
            return Context.startNewOrder(nom);
        }

        [Route("/createOrder")]
        [HttpPost]
        public int createOrder(int idcommande, int idconso, string etat)
        {
            return Context.createOrder(idcommande, idconso, etat);
        }

        [Route("/updateCommandStatus")]
        [HttpPut]
        public int updateCommandStatus(int idcommande, int idconso, string etat)
        {
            return Context.updateCommandStatus(idcommande, idconso, etat);
        }
    }
}
    

