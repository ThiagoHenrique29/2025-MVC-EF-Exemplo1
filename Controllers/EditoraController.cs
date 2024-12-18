using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MVC_EF.Exemplo1.Models;
using X.PagedList.Extensions;

namespace MVC_EF.Exemplo1.Controllers
{
    public class EditoraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EditoraController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string keyword, int? pagina)
        {
            const int registrosPorPagina = 10;

            // Filtrar editoras por keyword, caso fornecido
            var query = _context.Editoras.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(e => e.EditoraNome.Contains(keyword) ||
                                         e.EditoraCidade.Contains(keyword));
                ViewBag.keyword = keyword;
            }

            // Projeta para o modelo e aplica a ordenação
            var editoras = query
                .Select(e => new EditoraListViewModel
                {
                    EditoraID = e.EditoraID,
                    EditoraNome = e.EditoraNome,
                    EditoraCidade = e.EditoraCidade,
                    EditoraUF = e.EditoraUF
                })
                .OrderBy(e => e.EditoraNome);

            // Aplica paginação
            var modeloPaginado = editoras.ToPagedList(pagina ?? 1, registrosPorPagina);

            // Retorna explicitamente o modelo correto para a View
            return View(modeloPaginado);
        }

        // Create: Exibe o formulário de criação
        public IActionResult Create()
        {
            return View();
        }

        // Create: Processa a criação de uma nova editora
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditoraEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var editora = new Editora
                {
                    EditoraNome = model.EditoraNome,
                    EditoraLogradouro = model.EditoraLogradouro,
                    EditoraNumero = model.EditoraNumero,
                    EditoraComplemento = model.EditoraComplemento,
                    EditoraCidade = model.EditoraCidade,
                    EditoraUF = model.EditoraUF,
                    EditoraPais = model.EditoraPais,
                    EditoraCEP = model.EditoraCEP,
                    EditoraTelefone = model.EditoraTelefone
                };

                _context.Editoras.Add(editora);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var editora = _context.Editoras
                .FirstOrDefault(e => e.EditoraID == id);

            if (editora == null)
                return NotFound();

            var model = new EditoraEditViewModel
            {
                EditoraID = editora.EditoraID,
                EditoraNome = editora.EditoraNome,
                EditoraLogradouro = editora.EditoraLogradouro,
                EditoraNumero = editora.EditoraNumero,
                EditoraComplemento = editora.EditoraComplemento,
                EditoraCidade = editora.EditoraCidade,
                EditoraUF = editora.EditoraUF,
                EditoraPais = editora.EditoraPais,
                EditoraCEP = editora.EditoraCEP,
                EditoraTelefone = editora.EditoraTelefone
            };

            return View(model);
        }

        // Edit: Processa a atualização de uma editora
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditoraEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var editora = _context.Editoras
                    .FirstOrDefault(e => e.EditoraID == model.EditoraID);

                if (editora == null)
                    return NotFound();

                editora.EditoraNome = model.EditoraNome;
                editora.EditoraLogradouro = model.EditoraLogradouro;
                editora.EditoraNumero = model.EditoraNumero;
                editora.EditoraComplemento = model.EditoraComplemento;
                editora.EditoraCidade = model.EditoraCidade;
                editora.EditoraUF = model.EditoraUF;
                editora.EditoraPais = model.EditoraPais;
                editora.EditoraCEP = model.EditoraCEP;
                editora.EditoraTelefone = model.EditoraTelefone;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // Delete: Exibe a confirmação para deletar a editora
        public IActionResult Delete(int id)
        {
            var editora = _context.Editoras
                .FirstOrDefault(e => e.EditoraID == id);

            if (editora == null)
                return NotFound();

            var model = new EditoraListViewModel
            {
                EditoraID = editora.EditoraID,
                EditoraNome = editora.EditoraNome,
                EditoraCidade = editora.EditoraCidade,
                EditoraUF = editora.EditoraUF
            };

            return View(model);
        }

        // DeletePost: Processa a exclusão da editora
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var editora = _context.Editoras
                .FirstOrDefault(e => e.EditoraID == id);

            if (editora == null)
            {
                return NotFound();
            }

            _context.Editoras.Remove(editora);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
