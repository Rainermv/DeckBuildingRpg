using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.Cards.Collections;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.Core.Utility;
using Assets.TestsEditor;

namespace Assets.Scripts.Controller
{
    public class CardPlayController
    {


        private readonly CardScriptParser _cardScriptParser;
        private readonly Func<Entity, CombatModel, FindTargetData, List<ITargetable>> _onCardScriptFindTarget;

        private Dictionary<int, CardScript> _parsedCardScripts = new();

        private CardPlayData _activatedCardPlayData;


        public CardPlayController(CardScriptParser cardScriptParser, Func<Entity, CombatModel, FindTargetData, List<ITargetable>> onCardScriptFindTarget)
        {
            _cardScriptParser = cardScriptParser;
            _onCardScriptFindTarget = onCardScriptFindTarget;
        }


        public void SetupCardData(List<CardData> cardDataList)
        {
            foreach (var cardData in cardDataList)
            {
                var cardScriptParseResult = _cardScriptParser.ParseScript(cardData.CardScript, _onCardScriptFindTarget);

                if (!cardScriptParseResult.Success)
                {
                    DebugEvents.OnLogError(this, $"Error parsing CardScript on CardData {cardData.Index}: {cardData.Name}\n " + cardScriptParseResult.ErrorReason);
                    continue;
                }

                _parsedCardScripts.Add(cardData.Index, cardScriptParseResult.ParsedCardScript);
            }
        }

        public CardPlayData OnCardActivate(Card card, Entity sourceEntity, CombatModel combatModel)
        {
            var player = card.CardCollectionModelParent.PlayerParent;

            switch (card.CardCollectionModelParent.CollectionIdentifier)
            {
                case CardCollectionIdentifier.Hand:
                    //todo: decide if the card play is valid
                    ActivateCard(card, sourceEntity, combatModel);

                    return new CardPlayData()
                    {
                        SourceEntity = sourceEntity,
                        Player = sourceEntity.Owner,
                        Card = card,
                        IsPlayValid = true
                    };

            }

            return new CardPlayData()
            {
                Player = player,
                Card = card,
                IsPlayValid = false
            };
        }

        private void ActivateCard(Card card, Entity sourceEntity, CombatModel combatModel)
        {
            var cardScript = _parsedCardScripts[card.CardData.Index];
            var commandPlayData = new CardScriptCommandPlayData();

            foreach (var cardScriptCommand in cardScript.CardScriptCommands)
            {
                if (cardScriptCommand.OnValidatePlay != null &&
                    !cardScriptCommand.OnValidatePlay(sourceEntity, commandPlayData, combatModel))
                {
                    DebugEvents.OnLog(this,$"Failed to Validate: {cardScriptCommand.ScriptCommandText}\n" +
                                    $"on Targets: {string.Join(", ", commandPlayData.targetables.Select(t => t.Name))}");
                    return;
                }

                DebugEvents.OnLog(this, $"Activating: {cardScriptCommand.ScriptCommandText}\n" +
                                        $"on Targets: {string.Join(", ", commandPlayData.targetables.Select(t => t.Name))}");
                commandPlayData = cardScriptCommand.OnPlay(sourceEntity, commandPlayData, combatModel);

            }
        }
    }
}