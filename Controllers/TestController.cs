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
        [HttpGet(Name = "/getConso")]
        public List<Conso> getConso()
        {
            return Context.getConso();
        }
    }
}
