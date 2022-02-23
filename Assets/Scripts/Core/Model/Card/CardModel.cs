using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Commands;
using Assets.Scripts.Core.Model.AttributeModel;
using Assets.Scripts.Core.Model.Card.Collections;
using Assets.Scripts.Core.Model.Command;

namespace Assets.Scripts.Core.Model.Card
{
    public class CardModel 
    {
        public string Name { get; set; }
        public int CardDataIndex { get; set; }
        public string Text { get; set; }
        
        public List<ICardCommand> Commands { get; set; } = new();
        public CardCollectionModel CardCollectionModelParent { get; set; }
        public AttributeSet AttributeSet { get; set; }
        
        public Action OnUpdate { get; set; }
        public Action<CardModel, CardPlayReport> OnStartPlay { get; set; }
        public Action<CardModel, CardPlayReport, CardCommandReport> OnCommandRun { get; set; }
        public Action<CardModel, CardPlayReport> OnFinishPlay { get; set; }


        public static CardModel Make(string name = "")
        {
            return new CardModel()
            {
                AttributeSet = new AttributeSet(),
                Name = name,
            };
        }

        public static CardModel MakeFromCardData(CardDataModel cardDataModel)
        {
            return new CardModel()
            {
                Name = cardDataModel.Name,
                Text = cardDataModel.Text,
                CardDataIndex = cardDataModel.Index,
                AttributeSet = new AttributeSet()
            };
        }

        public CardPlayReport Play(BattleModel battleModel)
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
                var commandReport = Commands[index].Run(this, battleModel);

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