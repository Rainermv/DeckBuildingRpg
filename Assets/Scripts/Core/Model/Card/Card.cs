using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Commands;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Card.Collections;
using Assets.Scripts.Core.Model.Command;

namespace Assets.Scripts.Core.Model.Card
{
    public class Card 
    {
        public string Name { get; set; }
        public int CardDataIndex { get; set; }
        public string Text { get; set; }
        
        public List<ICardCommand> Commands { get; set; } = new();
        public CardCollectionModel CardCollectionModelParent { get; set; }
        public AttributeSet AttributeSet { get; set; }
        
        public Action OnUpdate { get; set; }
        public Action<Card, CardPlayReport> OnStartPlay { get; set; }
        public Action<Card, CardPlayReport, CardCommandReport> OnCommandRun { get; set; }
        public Action<Card, CardPlayReport> OnFinishPlay { get; set; }


        public static Card Make(string name = "")
        {
            return new Card()
            {
                AttributeSet = new AttributeSet(),
                Name = name,
            };

        }

        public static Card MakeFromCardData(CardData cardData)
        {
            return new Card()
            {
                Name = cardData.Name,
                Text = cardData.Text,
                CardDataIndex = cardData.Index,
                AttributeSet = new AttributeSet()
            };
        }

        public CardPlayReport Play(CombatModel combatModel)
        {
            // Initialize reports
            var cardPlayReport = new CardPlayReport()
            {
                CardCommandReports = Commands.Select(command => new CardCommandReport(CardCommandStatus.Start)).ToList()
            };

            OnStartPlay?.Invoke(this, cardPlayReport);

            // Run commands in sequence
            for (var index = 0; index < Commands.Count; index++)
            {
                var commandReport = Commands[index].Run(this, combatModel);

                cardPlayReport.CardCommandReports[index] = commandReport;
                OnCommandRun?.Invoke(this, cardPlayReport, commandReport);
            }

            // Finish play card
            OnFinishPlay?.Invoke(this, cardPlayReport);
            OnUpdate?.Invoke();

            return cardPlayReport;
        }


        
    }
}