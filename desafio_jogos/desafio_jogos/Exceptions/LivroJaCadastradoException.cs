using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_jogos.Exceptions
{
    public class LivroJaCadastradoException : Exception
    {
        public LivroJaCadastradoException()
            : base("Este livro já está cadastrado")
        { }
    }
}
