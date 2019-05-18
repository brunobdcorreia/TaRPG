using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Campanhas
{
    class Campanha
    {
        private string Nome;
        private int NumeroJogadores;

        public Campanha(string Nome, int NumeroJogadores)
        {
            this.Nome = Nome;
            this.NumeroJogadores = NumeroJogadores;
        }
    }
}
