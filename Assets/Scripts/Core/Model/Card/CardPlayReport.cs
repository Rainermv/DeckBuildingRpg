using System.Collections.Generic;
using Assets.Scripts.Core.Model.Command;

namespace Assets.Scripts.Core.Model.Card
{
    public class CardPlayReport
    {
        public List<CardCommandReport> CardCommandReports { get; set; }
    }
}