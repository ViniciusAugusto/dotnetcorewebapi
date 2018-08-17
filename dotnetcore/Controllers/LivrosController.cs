using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetcore.Models;

namespace dotnetcore.Controllers
{
	[Route("api/[controller]")]
	public class LivrosController : Controller
    {
		private readonly LivrosServiceContext _context;

		public LivrosController(LivrosServiceContext context)
        {
			_context = context;
			if (_context.Livros.Count() == 0) {
				_context.Livros.Add(new Livros {Titulo = "Teste", Autor = "Autor Teste", Ano = 2018, Edicao = "Edição Teste"});
				_context.SaveChanges();        
			}
        }

		[HttpGet]
		public IEnumerable<Livros> GetAll(){
			return _context.Livros.OrderBy(l => l.Titulo).ToList();
		}

		[HttpGet("{id}", Name = "GetLivro")]
		public IActionResult GetById(long id){
			var item = _context.Livros.FirstOrDefault(l => l.Id == id);
			if(item == null){
				return NotFound();
			}
			return new ObjectResult(item);
		}

		[HttpPost]
		public IActionResult Create([FromBody] Livros item) {
			if(item == null) {
				return BadRequest();
			}

			_context.Livros.Add(item);
			_context.SaveChanges();

			var retorno = new { Mensagem = "Livro cadastrado com sucesso!" };
            return new OkObjectResult(retorno);
		}

		[HttpPut("{id}")]
		public IActionResult Update(long id, [FromBody] Livros item) {

			if (item == null || item.Id != id) {
				return BadRequest();
			}

			var std = _context.Livros.FirstOrDefault(l => l.Id == id);
			if (std == null) {
				return NotFound();
			}

			std.Titulo = item.Titulo;
			std.Autor = item.Autor;
			std.Ano = item.Ano;
			std.Edicao = item.Edicao;
			_context.Livros.Update(std);
			_context.SaveChanges();
			var retorno = new { Mensagem = "Livro atualizado com sucesso!" };
            return new OkObjectResult(retorno);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id) {
			var todo = _context.Livros.FirstOrDefault(l => l.Id == id);
			if (todo == null) {
				return NotFound();
			}
			_context.Livros.Remove(todo);
			_context.SaveChanges();
			var retorno = new { Mensagem = "Livro deletado com sucesso!" };
            return new OkObjectResult(retorno);
		}




    }
}
