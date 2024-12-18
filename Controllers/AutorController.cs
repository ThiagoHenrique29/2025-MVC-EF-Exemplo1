using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF.Exemplo1.Models;
using System.Linq;
using X.PagedList.Extensions;

namespace MVC_EF.Exemplo1.Controllers
{
    public class AutorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string keyword, int? pagina)
        {
            const int registrosPorPagina = 10;

            // Filtrar autores por keyword, caso fornecido
            var query = _context.Autores.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a => a.AutorNome.Contains(keyword) ||
                                         a.AutorEmail.Contains(keyword));
                ViewBag.keyword = keyword;
            }

            // Projeta para o modelo e aplica a ordenação
            var autores = query
                .Select(a => new AutorListViewModel
                {
                    AutorID = a.AutorID,
                    AutorNome = a.AutorNome,
                    AutorEmail = a.AutorEmail,
                    AutorDataNascimento = a.AutorDataNascimento
                })
                .OrderBy(a => a.AutorNome);

            // Aplica paginação
            var modeloPaginado = autores.ToPagedList(pagina ?? 1, registrosPorPagina);

            // Retorna explicitamente o modelo correto para a View
            return View(modeloPaginado);
        }

        // Create: Exibe o formulário de criação
        public IActionResult Create()
        {
            return View();
        }

        // Create: Processa a criação de um novo autor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AutorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var autor = new Autor
                {
                    AutorNome = model.AutorNome,
                    AutorEmail = model.AutorEmail,
                    AutorDataNascimento = model.AutorDataNascimento
                };

                _context.Autores.Add(autor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // Edit: Exibe o formulário de edição
        public IActionResult Edit(int id)
        {
            var autor = _context.Autores
                .FirstOrDefault(a => a.AutorID == id);

            if (autor == null)
                return NotFound();

            var model = new AutorEditViewModel
            {
                AutorID = autor.AutorID,
                AutorNome = autor.AutorNome,
                AutorEmail = autor.AutorEmail,
                AutorDataNascimento = autor.AutorDataNascimento
            };

            return View(model);
        }

        // Edit: Processa a atualização de um autor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AutorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var autor = _context.Autores
                    .FirstOrDefault(a => a.AutorID == model.AutorID);

                if (autor == null)
                    return NotFound();

                autor.AutorNome = model.AutorNome;
                autor.AutorEmail = model.AutorEmail;
                autor.AutorDataNascimento = model.AutorDataNascimento;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // Delete: Exibe a confirmação para deletar o autor
        public IActionResult Delete(int id)
        {
            var autor = _context.Autores
                .FirstOrDefault(a => a.AutorID == id);

            if (autor == null)
                return NotFound();

            var model = new AutorListViewModel
            {
                AutorID = autor.AutorID,
                AutorNome = autor.AutorNome,
                AutorEmail = autor.AutorEmail,
                AutorDataNascimento = autor.AutorDataNascimento
            };

            return View(model);
        }

        // DeletePost: Processa a exclusão do autor
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            // Busca o autor com o ID fornecido
            var autor = _context.Autores
                .FirstOrDefault(a => a.AutorID == id);

            // Se não encontrar o autor, retorna NotFound()
            if (autor == null)
            {
                return NotFound();
            }

            // Remove o autor do contexto
            _context.Autores.Remove(autor);

            try
            {
                // Tenta salvar as alterações (exclusão) no banco de dados
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                // Trata a exceção caso haja erro (por exemplo, se o autor tiver dependências em outras entidades)
                Console.WriteLine(e);
                return StatusCode(500, "Erro ao excluir o autor. Ocorreu um problema ao tentar salvar as alterações.");
            }

            // Após a exclusão, redireciona para a lista de autores (Index)
            return RedirectToAction(nameof(Index));
        }

    }
}
