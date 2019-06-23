using RPGproject.Source.CampaignCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.UserData
{
    class CreatedCampaigns
    {
        private static List<Campaign> userCampaigns = new List<Campaign>();
        public static List<Campaign> UserCampaigns { get { return userCampaigns; } }

        public static void AddCampaign(Campaign campaign)
        {
            foreach(Campaign c in userCampaigns)
            {
                if (campaign.CampaignName.Equals(c.CampaignName))
                    return;
            }

            userCampaigns.Add(campaign);
        }

        public static void DeleteCampaign(Campaign campaign)
        {
            userCampaigns.Remove(campaign);
        }
    }
}
