using System.Collections.Generic;
using Assets.Scripts.CardSystem.Models.Commands;

namespace Assets.Scripts.CardSystem.Models
{
    public class CardPlayReport
    {
        public List<CardCommandReport> CardCommandReports { get; set; }
    }
}