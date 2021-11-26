using desafio_jogos.Exceptions;
using desafio_jogos.InputModel;
using desafio_jogos.Services;
using desafio_jogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_jogos.Controller.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroservice;
        public LivrosController(ILivroService livroservice)
        {
            _livroservice = livroservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivrosViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var livros = await _livroservice.Obter(pagina, quantidade);

            if (livros.Count() == 0)
                return NoContent();

            return Ok(livros);
        }

        [HttpGet("{idLivro:guid}")]
        public async Task<ActionResult<LivrosViewModel>> Obter([FromRoute] Guid idLivro)
        {
            var livro = await _livroservice.Obter(idLivro);

            if (livro == null)
                return NoContent();

            return Ok(livro);
        }

        [HttpPost]
        public async Task<ActionResult<LivrosViewModel>> InserirLivro([FromBody] LivroInputModel livroInputModel)
        {
            try
            {
                var livro = await _livroservice.Inserir(livroInputModel);

                return Ok(livro);
            }
            catch (LivroJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um livro com este nome para este autor");
            }
        }

        [HttpPut("{idLivro:guid}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] Guid idLivro, [FromBody] LivroInputModel livroInputModel)
        {
            try
            {
                await _livroservice.Atualizar(idLivro, livroInputModel);
                return Ok();
            }
            catch (LivroNaoCadastradoException ex)
            {
                return NotFound("Não existe este livro");
            }
        }

        [HttpPatch("{idLivro:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] Guid idLivro, [FromRoute] double preco)
        {
            try
            {
                await _livroservice.Atualizar(idLivro, preco);
                return Ok();
            }
            catch (LivroNaoCadastradoException ex)
            {
                return NotFound("Não existe esse livro");
            }
        }

        [HttpDelete("{idLivro:guid}")]
        public async Task<ActionResult> ApagarLivro([FromRoute] Guid idLivro)
        {
            try
            {
                await _livroservice.Remover(idLivro);
                
                return Ok();
            }
            catch (LivroNaoCadastradoException ex)
            {
                return NotFound("Não existe esse livro");
            }
        }

    }
}
