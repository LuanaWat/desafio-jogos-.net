using desafio_jogos.InputModel;
using desafio_jogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_jogos.Services
{
    public interface ILivroService : IDisposable
    {
        Task<List<LivrosViewModel>> Obter(int pagina, int quantidade);

        Task<LivrosViewModel> Obter(Guid Id);

        Task<LivrosViewModel> Inserir(LivroInputModel livro);

        Task Atualizar(Guid Id, LivroInputModel livro);

        Task Atualizar(Guid Id, double preco);

        Task Remover(Guid Id);
    }
}
