using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem.CardCommand;
using UnityEngine;

namespace Assets.Scripts.CardSystem
{
    public class Card 
    {

        public string Name { get; set; }
        public List<ICardCommand> Commands { get; set; } = new();
        public int CardType { get; set; }

        public Action<Card, CardPlayReport> OnStartPlay { get; set; }
        public Action<Card, CardPlayReport, CardCommandReport> OnComandRun { get; set; }
        public Action<Card, CardPlayReport> OnFinishPlay { get; set; }
        public Action OnUpdate { get; set; }


        public static Card Make(string name)
        {
            return new Card()
            {
                Name = name,
            };
        }


        public CardPlayReport Play()
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
                var commandReport = Commands[index].Run();

                cardPlayReport.CardCommandReports[index] = commandReport;
                OnComandRun?.Invoke(this, cardPlayReport, commandReport);
            }

            // Finish play card
            OnFinishPlay?.Invoke(this, cardPlayReport);
            OnUpdate?.Invoke();

            return cardPlayReport;
        }

        
    }
}