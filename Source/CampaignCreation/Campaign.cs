using RPGproject.Source.CharacterCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.CampaignCreation
{
    class Campaign
    {
        private string campaignName;
        public string CampaignName { get { return campaignName; } set { campaignName = value; } }
        private int numberOfPlayers;
        public int NumberOfPlayers { get { return numberOfPlayers; } }
        private List<Character> characters;
        public List<Character> Characters { get { return characters; } set { characters = value; } }

        public Campaign()
        {
            characters = new List<Character>();
        }
    }
}
