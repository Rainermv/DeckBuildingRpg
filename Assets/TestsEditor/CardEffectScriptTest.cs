using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Card.Collections;
using Assets.Scripts.Core.Model.Entity;
using Assets.Scripts.Core.Model.GridMap;
using NUnit.Framework;
using UnityEngine;

namespace Assets.TestsEditor
{
    [TestFixture]
    public class CardEffectScriptTest
    {

        [TestCase("@damage +1", 0,1)]
        [TestCase("@damage -1", 0,-1)]
        [TestCase("@damage =5", 4,5)]

        [TestCase("@damage +1; @damage +2", 0, 3)]
        [TestCase("@damage -1; @damage -2", 0, -3)]
        [TestCase("@damage =5; @damage +5", 4, 10)]

        [TestCase(" @damage  +1 ", 0, 1)]
        [TestCase(" @damage  -1 ", 0, -1)]
        [TestCase(" @damage  =5 ", 4, 5)]

        [TestCase("   @damage +1  ;   @damage    +2  ", 0, 3)]
        [TestCase(" @damage   -1 ;   @damage   -2", 0, -3)]
        [TestCase("  @damage  =5     ;  @damage +5", 4, 10)]
        public void SimpleEffectScript(string script, int startingValue, int expectedResult)
        {
            var attributeMap = new Dictionary<string, int>()
            {
                {"damage", 0}
            };

            var cardEffector = new CardScriptEffector(attributeMap);
            
            // Make base model and add test entity
            var testModel = TestBattleModel();
            var entity = BattleEntity.Make("foo", new GridPosition(0, 0), testModel.Players[0]);
            entity.AttributeSet.Set(0, startingValue);
            testModel.Entities.Add(entity);
            
            // run script
            testModel = cardEffector.RunScript(script, entity, entity, testModel);

            // Assert
            Assert.AreEqual(expectedResult, entity.AttributeSet.GetValue(0));

        }

        private BattleModel TestBattleModel()
        {
            var cardModel = CardModel.Make("TEST");
            var player = Player.Make("0");
            player.AddNewCardCollection(CardCollectionIdentifier.Deck).InsertCards(new List<CardModel> { cardModel });
            
            return new BattleModel()
            {
                Players = new List<Player>() { player }
            };
        }
    }

    public class CardScriptEffector
    {
        const char ATTRIBUTE_SYMBOL = '@';

        const string COMMAND_SEPARATOR = ";";
        const string SYMBOL_SEPARATOR = " ";


        private Dictionary<string, int> _attributeMap;
        private IFormatProvider provider;

        public CardScriptEffector(Dictionary<string, int> attributeMap)
        {
            _attributeMap = attributeMap;
            provider = CultureInfo.CurrentCulture;
        }

        public BattleModel RunScript(string script, BattleEntity sourceEntity, BattleEntity targetEntity, BattleModel battleModel)
        {
            
            var commands = SplitOnSeparator(script, COMMAND_SEPARATOR);

            foreach (var command in commands)
            {
                switch (command[0])
                {
                    case ATTRIBUTE_SYMBOL:
                    {
                        var symbols = SplitOnSeparator(command, SYMBOL_SEPARATOR);

                        var attributeKey = _attributeMap[symbols[0][1..]];
                        var valueSymbol = symbols[1];

                        if (valueSymbol[0] == '=')
                        {
                            targetEntity.AttributeSet.Set(attributeKey, int.Parse(valueSymbol[1..]));
                            continue;
                        }

                        targetEntity.AttributeSet.Modify(attributeKey, int.Parse(valueSymbol, NumberStyles.AllowLeadingSign));
                        continue;
                    }
                }
            }


            return battleModel;
        }


        private static string[] SplitOnSeparator(string text, string separator)
        {
            return text.Split(separator)
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrEmpty(l))
                .ToArray();
        }
    }
}