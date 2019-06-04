using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.CampaignCreation
{
    class CampaignModel
    {
        private Campaign campaignModel;
        public Campaign GetCampaignModel { get { return campaignModel; } }

        public CampaignModel()
        {
            campaignModel = new Campaign();
        }
    }
}
