using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Campanhas
{
    class Campanha
    {
        private string campaignName;
        private int numberOfPlayers;

        public Campanha(string Nome, int NumeroJogadores)
        {
            this.campaignName = Nome;
            this.numberOfPlayers = NumeroJogadores;
        }
    }
}
