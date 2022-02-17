using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem.Models.Attributes;
using Assets.Scripts.Controller;
using Assets.Scripts.Controller.Factories;
using Assets.Scripts.Model;
using Assets.Scripts.Model.CardModel;
using Assets.Scripts.Model.CardModel.Collections;
using Assets.Scripts.Model.CardModel.Commands;
using Assets.Scripts.Model.CharacterModel;
using Assets.Scripts.Model.GridModel;
using Assets.Scripts.Systems.CardSystem.Constants;
using Assets.Scripts.Systems.CardSystem.Utility;

namespace Assets.Scripts
{
    internal class LevelModelFactory
    {
        public static LevelModel Build(GridMapModel gridMapModel)
        {
            var levelModel = new LevelModel()
            {
                GridMapModel = gridMapModel,
                GlobalAttributeSet = new AttributeSet(),
                Players = CardSystemModelFactory.BuildPlayers(),
                Characters = new List<Character>()
            };

            levelModel.GlobalAttributeSet.Set(AttributeKey.Health, 100);

            int i = 0;
            foreach (var player in levelModel.Players.Values)
            {
                SetupPlayer(player);
                AddCharacterTo(player, levelModel.GridMapModel.GridTiles[i++], levelModel);
                
                // Add cards to Deck
                foreach (var card in player.CardCollections[CardCollectionIdentifier.Deck].Cards)
                {
                    SetupCard(card, player);
                }

                // Draw initial hand
                CardService.DrawCards(player.CardCollections[CardCollectionIdentifier.Deck],
                    player.CardCollections[CardCollectionIdentifier.Hand],
                    5);
            }

            return levelModel;
        }
        
        

        private static void AddCharacterTo(Player player, GridTile gridTile, LevelModel levelModel)
        {
            var character = Character.Make(player.Name, gridTile.GridPosition, player);

            gridTile.PositionedEntity = character;

            levelModel.Characters.Add(character);
        }


        private static void SetupPlayer(Player player)
        {
            player.AttributeSet.Set(AttributeKey.Power, 5);


        }

        private static void SetupCard(Card card, Player player)
        {
            var random = new Random();

            var possibleCardTypes = new[] { CardTypes.DRAW, CardTypes.ATTACK, CardTypes.POWER };

            var cardType = random.Next(0, possibleCardTypes.Length);
            var powerCost = 0;
            card.ImageIndex = cardType;

            card.AttributeSet.Set(AttributeKey.CardType, cardType);

            switch (cardType)
            {
                case CardTypes.DRAW:
                    var cardsToDraw = random.Next(1, 4); //1 to 3
                    powerCost = cardsToDraw * 2;

                    card.Name = $"Insight";


                    card.Commands.Add(
                        new DrawCardsCommand(
                            player.CardCollections[CardCollectionIdentifier.Deck],
                            player.CardCollections[CardCollectionIdentifier.Hand],
                            cardsToDraw));
                    break;

                case CardTypes.ATTACK: // Attack
                    powerCost = random.Next(1, 6);

                    card.Name = $"Sword Attack";

                    card.Commands.Add(
                        new SumGlobalAttributeCommand(AttributeKey.Health, -powerCost * 2));
                    break;

                case CardTypes.POWER: // Attack
                    var powerGenerated = 3;
                    card.Name = $"Empower";

                    card.Commands.Add(
                        new SumAttributeCommand(player, AttributeKey.Power, powerGenerated));
                    break;

            }

            card.AttributeSet.Set(AttributeKey.PowerCost, powerCost);
        }

    }
}