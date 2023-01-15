using CDC.Data;
using CDC.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using NToastNotify;

namespace CDC.Controllers
{
    public class CardController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly CDCContext _context;

        //injeção de dependência
        public CardController(CDCContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
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

            var cardData = _context.Cards
                .Where(bd => bd.Color == card.Color && bd.Type == card.Type && bd.Name == card.Name && bd.Set == card.Set && bd.IsFoil == card.IsFoil && bd.Condition == card.Condition)
                .FirstOrDefault();

            if (cardData == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(card);
                    _context.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Carta cadastrada");
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                _toastNotification.AddErrorToastMessage("A carta informada já existe no banco de dados.");
            }
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = _context.Cards.FirstOrDefault(bd => bd.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = _context.Cards.FirstOrDefault(bd => bd.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditComfirmed(int? id, Card card, string[]? color)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (color == null || color.Length == 0)
            {
                card.Color = "Incolor";
            }
            else
            {
                card.Color = string.Join(",", color);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    _toastNotification.AddSuccessToastMessage("Carta editada");
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!_context.Cards.Any(item => item.Id == card.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = _context.Cards.FirstOrDefault(bd => bd.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (_context.Cards == null)
            {
                return Problem("Entity set 'NamesPace.Modelo' is null.");
            }
            var card = _context.Cards.Find(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
            }

            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Carta removida");
            return RedirectToAction(nameof(Index));
        }
    }
}
