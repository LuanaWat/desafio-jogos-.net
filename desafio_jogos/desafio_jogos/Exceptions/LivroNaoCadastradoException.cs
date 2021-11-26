﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_jogos.Exceptions
{
    public class LivroNaoCadastradoException : Exception
    {
        public LivroNaoCadastradoException()
            : base("Este livro não está cadastrado")
        { }
    }
}
