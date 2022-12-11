using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.Cards.Collections;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.Core.Utility;
using Assets.Scripts.View.Cards;
using Assets.TestsEditor;
using Sirenix.OdinInspector.Editor.Modules;

namespace Assets.Scripts.Controller
{
    public class CardPlayController
    {


        private readonly CardScriptParser _cardScriptParser;
        private readonly Func<Entity, CombatModel, FindTargetData, List<ITargetable>> _onCardScriptFindTarget;

        private Dictionary<int, CardScript> _parsedCardScripts = new();

        private CardPlayData _activatedCardPlayData;

        public CardPlayController(CardScriptParser cardScriptParser,
            Func<Entity, CombatModel, FindTargetData, List<ITargetable>> onCardScriptFindTarget)
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
                    DebugEvents.LogError(this, $"Error parsing CardScript on CardData {cardData.Index}: {cardData.Name}\n " + cardScriptParseResult.ErrorReason);
                    continue;
                }

                _parsedCardScripts.Add(cardData.Index, cardScriptParseResult.ParsedCardScript);
            }
        }

    }
}