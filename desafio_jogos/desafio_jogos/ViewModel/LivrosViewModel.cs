using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_jogos.ViewModel
{
    public class LivrosViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public double Preco { get; set; }
    }
}
