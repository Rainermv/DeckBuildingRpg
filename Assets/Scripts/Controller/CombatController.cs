using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Controller.CardShuffler;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.Cards.Collections;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.View.Cards;
using Assets.TestsEditor;

namespace Assets.Scripts.Controller
{
    public class CombatController
    {

        private readonly ICardShuffler _cardShuffler;


        private readonly CardPlayController _cardPlayController;


        private Entity _controlledEntity;

        private List<Entity> _battleEntities;
        private CombatModel _combatModel;

        private CardPlayData _cardPlayData;


        public CombatController(ICardShuffler cardShuffler,
            CardScriptParser cardScriptParser)
        {
            _cardShuffler = cardShuffler;

            _cardPlayController = new CardPlayController(cardScriptParser, FindTargetsResolver.OnCardScriptFindTarget);

            GameplayEvents.OnCardEvent += (card, cardEvent) =>
            {
                if (cardEvent == CardEventIdentifiers.Activate)
                    _cardPlayController.OnCardActivate(card, _controlledEntity, _combatModel);
            };
        }


        public CombatModel Setup(CombatModel combatModel)
        {
            _combatModel = combatModel;

            _cardPlayController.SetupCardData(combatModel.CardDataList);

            foreach (var player in combatModel.Players)
            {
                var deck = player.CardCollections[CardCollectionIdentifier.Deck];
                deck.Cards = _cardShuffler.Run(deck.Cards);
            }

            _battleEntities = combatModel.Entities;

            _controlledEntity = combatModel.Entities[0];

            return combatModel;
        }

        public void OnCombatStart()
        {
            foreach (var battleEntity in _battleEntities)
            {
                battleEntity.Attributes.SetValue(0, 0);
                battleEntity.Attributes.SetValue(1, 0);
            }
        }
    }

    public static class AttributeKey
    {
        public const int Power = 0;
        public const int Health = 1;
        public const int CardType = 2;
        public const int PowerCost = 3;
    }
}