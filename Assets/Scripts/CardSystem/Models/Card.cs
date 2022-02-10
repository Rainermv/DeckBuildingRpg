using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem.Model.Collection;
using Assets.Scripts.CardSystem.Model.Command;
using UnityEngine;

namespace Assets.Scripts.CardSystem.Model
{
    public class Card 
    {
        public string Name { get; set; }
        public List<ICardCommand> Commands { get; set; } = new();
        public CardCollection CardCollectionParent { get; set; }
        public AttributeSet AttributeSet { get; set; }


        public Action<Card, CardPlayReport> OnStartPlay { get; set; }
        public Action<Card, CardPlayReport, CardCommandReport> OnCommandRun { get; set; }
        public Action<Card, CardPlayReport> OnFinishPlay { get; set; }
        public Action OnUpdate { get; set; }


        public static Card Make(string name)
        {
            return new Card()
            {
                AttributeSet = new AttributeSet(),
                Name = name,
            };
        }

        public CardPlayReport Play(GameContext gameContext)
        {
            // Initialize reports
            var cardPlayReport = new CardPlayReport()
            {
                CardCommandReports = Commands.Select(command => new CardCommandReport(CardCommandStatus.Start)).ToList()
            };

            OnStartPlay?.Invoke(this, cardPlayReport);

            Debug.Log($"{Name} was played with {Commands.Count} commands");

            // Run commands in sequence
            for (var index = 0; index < Commands.Count; index++)
            {
                var commandReport = Commands[index].Run(this, gameContext);

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