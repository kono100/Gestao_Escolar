//using Microsoft.AspNetCore.Mvc;

//namespace Gestao.Controllers
//{
//    public class InstituicaoController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}


using Gestao.Data;
using Gestao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Controllers
{
    public class InstituicaoController : Controller
    {


        private readonly IESContext _context;

        public InstituicaoController(IESContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Instituicao.OrderBy
                (c => c.Nome).ToListAsync());
        }

        //Get: Instituicao/Create
        public IActionResult Create()
        {
            return View();
        }
        //POST : Instituicao/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Instituicao instituicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(instituicao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Erro", "não foi possivel inserir os dados.");
            }
            return View(instituicao);
        }


        //GET: Instituicao/Update

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await _context.Instituicao.SingleOrDefaultAsync
                (m => m.InstituicaoID == id);

            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }


        private bool InstituicaoExists(long? id)
        {
            return _context.Instituicao.Any(e => e.InstituicaoID == id);
        }






        //POST: Instituicao/Update
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(long? id, Instituicao instituicao)

        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituicao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!InstituicaoExists(instituicao.InstituicaoID))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            return View(instituicao);

        }


        //GET: Instituicao/Details

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await _context.Instituicao.SingleOrDefaultAsync
                (m => m.InstituicaoID == id);

            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }



        //GET: Instituicao/Delete

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await _context.Instituicao.
                SingleOrDefaultAsync(d => d.InstituicaoID == id);

            

            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }



        //POST: Instituicao/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var instituicao = await _context.Instituicao.SingleOrDefaultAsync
                (m => m.InstituicaoID == id);



            _context.Instituicao.Remove(instituicao);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Instituicao {instituicao.Nome.ToUpper()} foi removido";

            return RedirectToAction(nameof(Index));


        }



    }

}

