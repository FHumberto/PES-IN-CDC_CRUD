using CDC.Data;

using Microsoft.AspNetCore.Mvc;

namespace CDC.Controllers
{
    public class CardController : Controller
    {
        private readonly CDCContext _context;

        //injeção de dependência
        public CardController(CDCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Cards.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
