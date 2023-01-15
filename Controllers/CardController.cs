using CDC.Data;
using CDC.Models;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Card card, string[]? color)
        {
            if (color == null || color.Length == 0)
            {
                card.Color = "Incolor";
            }
            else
            {
                card.Color = string.Join(",", color);
            }

            // Verifica se o elemento já existe no banco de dados
            var cardData = _context.Cards
                .Where(bd => bd.Color == card.Color && bd.Type == card.Type && bd.Name == card.Name && bd.Set == card.Set && bd.IsFoil == card.IsFoil && bd.Condition == card.Condition)
                .FirstOrDefault();

            if (cardData == null)
            {
                // verifica se o modelo é válido
                if (ModelState.IsValid)
                {
                    _context.Add(card);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                // envia a notificação para a interface
                TempData["Mensagem"] = "A carta informada já existe no banco de dados.";
            }
            return View(card);
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
