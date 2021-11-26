using desafio_jogos.Entities;
using desafio_jogos.Exceptions;
using desafio_jogos.InputModel;
using desafio_jogos.Repositories;
using desafio_jogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_jogos.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<List<LivrosViewModel>> Obter(int pagina, int quantidade)
        {
            var livros = await _livroRepository.Obter(pagina, quantidade);

            return livros.Select(livro => new LivrosViewModel
            {
                Id = livro.Id,
                Nome = livro.Nome,
                Autor = livro.Autor,
                Preco = livro.Preco
            })
                        .ToList();
        }

        public async Task<LivrosViewModel> Obter(Guid id)
        {
            var livro = await _livroRepository.Obter(id);

            if (livro == null)
                return null;

            return new LivrosViewModel
            {
                Id = livro.Id,
                Nome = livro.Nome,
                Autor = livro.Autor,
                Preco = livro.Preco
            };

        }

        public async Task<LivrosViewModel> Inserir(LivroInputModel livro)
        {
            var entidadeLivro = await _livroRepository.Obter(livro.Nome, livro.Autor);

            if (entidadeLivro.Count > 0)
                throw new LivroJaCadastradoException();

            var LivroInsert = new Livro
            {
                Id = Guid.NewGuid(),
                Nome = livro.Nome,
                Autor = livro.Autor,
                Preco = livro.Preco
            };

            await _livroRepository.Inserir(LivroInsert);

            return new LivrosViewModel
            {
                Id = LivroInsert.Id,
                Nome = livro.Nome,
                Autor = livro.Autor,
                Preco = livro.Preco
            };
        }

        public async Task Atualizar(Guid id, LivroInputModel livro)
        {
            var entidadeLivro = await _livroRepository.Obter(id);

            if (entidadeLivro == null)
                throw new LivroNaoCadastradoException();

            entidadeLivro.Nome = livro.Nome;
            entidadeLivro.Autor = livro.Autor;
            entidadeLivro.Preco = livro.Preco;

            await _livroRepository.Atualizar(entidadeLivro);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeLivro = await _livroRepository.Obter(id);

            if (entidadeLivro == null)
                throw new LivroNaoCadastradoException();

            entidadeLivro.Preco = preco;

            await _livroRepository.Atualizar(entidadeLivro);
        }

        public async Task Remover(Guid id)
        {
            var livro = await _livroRepository.Obter(id);

            if (livro == null)
                throw new LivroNaoCadastradoException();

            await _livroRepository.Remover(id);
        }

        public void Dispose()
        {
            _livroRepository?.Dispose();
        }

    }
}

