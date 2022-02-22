using System;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Card.Collections;

namespace Assets.Scripts.Core.Model.Card
{
    public class CardModel 
    {
        public int ImageIndex { get; set; }

        public string Name { get; set; }
        //public List<ICardCommand> Commands { get; set; } = new();
        public CardCollectionModel CardCollectionModelParent { get; set; }
        public AttributeSet AttributeSet { get; set; }


        public Action<CardModel, CardPlayReport> OnStartPlay { get; set; }
        //public Action<CardModel, CardPlayReport, CardCommandReport> OnCommandRun { get; set; }
        public Action<CardModel, CardPlayReport> OnFinishPlay { get; set; }
        public Action OnUpdate { get; set; }
        public string TextBlock => "Foo";


        public static CardModel Make(string name = "")
        {
            return new CardModel()
            {
                AttributeSet = new AttributeSet(),
                Name = name,
            };
        }

        public CardPlayReport Play(BattleModel battleModel)
        {/*
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
                var commandReport = Commands[index].Run(this, battleModel);

                cardPlayReport.CardCommandReports[index] = commandReport;
                OnCommandRun?.Invoke(this, cardPlayReport, commandReport);
            }

            // Finish play card
            OnFinishPlay?.Invoke(this, cardPlayReport);
            OnUpdate?.Invoke();

            return cardPlayReport;
            */
            return new CardPlayReport();
        }


        
    }
}