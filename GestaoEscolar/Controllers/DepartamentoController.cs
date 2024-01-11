using Gestao.Data;
using Gestao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Controllers
{
    public class DepartamentoController : Controller
    {


        private readonly IESContext _context;

        public DepartamentoController(IESContext context)
        {
            this._context = context;
        }


        // GET LIST
        public async Task<IActionResult> Index()
        {
            var departamentos = await _context.Departamento
                .Include(i=> i.Instituicao)
                .OrderBy(d => d.Nome)
                .ToListAsync();
            return View(departamentos);

        }




        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Departamento.OrderBy
        //        (c => c.Nome).ToListAsync());
        //}




        //Get: Departamento/Create
        public IActionResult Create()
        {
            var instituicoes = _context.Instituicao.OrderBy(i=> i.Nome).ToList();
            instituicoes.Insert(0, new Instituicao()
        {
            InstituicaoID = 0,
                Nome = "Selecione a Instituição"
        });

        ViewBag.Instituicoes = instituicoes;
            return View();
        }


        //POST : Departamento/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(departamento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Erro", "não foi possivel inserir os dados.");
            }
            return View(departamento);
        }



        //GET: Departamento/Update

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento.SingleOrDefaultAsync
                (m => m.DepartamentoID == id);

            if (departamento == null)
            {
                return NotFound();
            }

            ViewBag.Instituicoes = new SelectList(_context.Instituicao.OrderBy(b => b.Nome),
                "InstituicaoID", "Nome", departamento.fk_InstituicaoID);

            return View(departamento);
        }


        private bool DepartamentoExists(long? id)
        {
            return _context.Departamento.Any(e => e.DepartamentoID == id);
        }






        //POST: Departamento/Update
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(long? id, Departamento departamento)

        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!DepartamentoExists(departamento.DepartamentoID))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));

            } return View(departamento);

        }




        //GET: Departamento/Details

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento.SingleOrDefaultAsync
                (m => m.DepartamentoID == id);

            _context.Instituicao.Where(i => departamento.fk_InstituicaoID == i.InstituicaoID).Load();

            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }






            //GET: Departamento/Delete

            public async Task<IActionResult> Delete(long? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var departamento = await _context.Departamento.
                    SingleOrDefaultAsync(d => d.DepartamentoID == id);

                _context.Instituicao.Where(i => departamento.fk_InstituicaoID == i.InstituicaoID).Load();
                


                if (departamento == null)
                {
                    return NotFound();
                }
                return View(departamento);
            }






        //POST: Departamento/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var departamento = await _context.Departamento.SingleOrDefaultAsync
                (m => m.DepartamentoID == id);
            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Departamento {departamento.Nome.ToUpper()} foi removido";

            return RedirectToAction(nameof(Index));


        }



    }

    }

