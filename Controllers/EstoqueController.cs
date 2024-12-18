using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_EF.Exemplo1.Models;

namespace MVC_EF.Exemplo1.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstoqueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista os livros com seu estoque atual
        public async Task<IActionResult> Index()
        {
            var livros = await _context.Livros.ToListAsync();

            var livrosComEstoque = livros.Select(l => new LivroEstoqueViewModel
            {
                LivroID = l.LivroID,
                Nome = l.LivroTitulo,
                QuantidadeEstoque = l.Estoque  // Utiliza a propriedade Estoque diretamente
            }).ToList();

            return View(livrosComEstoque);
        }

        // Método para adicionar 1 no estoque
        [HttpPost]
        public async Task<IActionResult> AdicionarEstoque(int livroId)
        {
            var livro = await _context.Livros.FindAsync(livroId);
            if (livro != null)
            {
                livro.Estoque += 1;  // Adiciona 1 no estoque
                _context.Livros.Update(livro); // Atualiza o livro no banco
                await _context.SaveChangesAsync(); // Salva as mudanças no banco
            }

            return RedirectToAction(nameof(Index));
        }

        // Método para tirar 1 do estoque
        [HttpPost]
        public async Task<IActionResult> RetirarEstoque(int livroId)
        {
            var livro = await _context.Livros.FindAsync(livroId);
            if (livro != null && livro.Estoque > 0)
            {
                livro.Estoque -= 1;  // Retira 1 do estoque
                _context.Livros.Update(livro); // Atualiza o livro no banco
                await _context.SaveChangesAsync(); // Salva as mudanças no banco
            }

            return RedirectToAction(nameof(Index));
        }

        // Método para editar a quantidade de estoque
        [HttpPost]
        public async Task<IActionResult> EditarEstoque(int livroId, int novaQuantidade)
        {
            var livro = await _context.Livros.FindAsync(livroId);
            if (livro != null)
            {
                livro.Estoque = novaQuantidade;  // Atualiza a quantidade do estoque
                _context.Livros.Update(livro); // Atualiza o livro no banco
                await _context.SaveChangesAsync(); // Salva as mudanças no banco
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
