using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_Cricket
{
   public  class TeamDetails
    {
        private string BatsmanOne { get; set; }
        public  void UserTeamDetails(int team)
        {
            switch (team)
            {
                case 1:
                    this.BatsmanOne = "Adam Gilchrist";

                    break;
            }
        }
    }
}
