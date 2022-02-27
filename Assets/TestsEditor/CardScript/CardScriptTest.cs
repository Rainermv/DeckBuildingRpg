using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using Assets.Scripts.Controller;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Card;
using Assets.Scripts.Core.Model.Card.Collections;
using Assets.Scripts.Core.Model.Command;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.Core.Model.GridMap;
using Assets.Scripts.Core.Utility;
using NUnit.Framework;
using UnityEngine;

namespace Assets.TestsEditor
{
    [TestFixture]
    public class CardScriptTest
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
        public void SimpleCardScript(string script, int startingValue, int expectedResult, object source)
        {
            var testModel = TestBattleModel(1);
            // Test target with initial value
            var entity = testModel.Entities[0];
            entity.AttributeSet.Set(0, 0);

            var scriptParser = new CardScriptParser(testModel.AttributeMap);
            
            // run script
            var runScriptParseResult = scriptParser.ParseScript(script, FindTargetsResolver.OnCardScriptFindTarget);

            // Assert Parse
            Debug.Log(runScriptParseResult.ErrorReason);
            Assert.IsTrue(runScriptParseResult.Success);

            var runResult = RunScriptCommands(runScriptParseResult.ParsedCardScript.CardScriptCommands, testModel, entity);
            
            Assert.IsTrue(runResult);
            Assert.AreEqual(expectedResult, entity.AttributeSet.GetValue(0));

        }

        private bool RunScriptCommands(List<CardScriptCommand> cardScriptCommands, CombatModel combatModel, Entity source)
        {
            var commandPlayData = new CardScriptCommandPlayData();

            foreach (var cardScriptCommand in cardScriptCommands)
            {
                if (cardScriptCommand.OnValidatePlay != null && 
                    !cardScriptCommand.OnValidatePlay(source, commandPlayData, combatModel))
                {
                    return false;
                }

                commandPlayData = cardScriptCommand.OnPlay(source, commandPlayData, combatModel);

            }

            return true;
        }


        // @Attribute
        // #Target
        
        // Target 1 enemy at range 1, -1 health to targets
        [TestCase("#quantity:1,Range:1; @health:+1", 1, 1)]
        [TestCase("#quantity:1,Range:1; @health:+1", 2, 1)]
        [TestCase("#quantity:1,Range:1; @health:+1", 3, 1)]
        [TestCase("#quantity:1,Range:1; @health:+1", 3, 0)]


        [TestCase("#quantity:2,Range:1; @health:+1", 1, 1)]
        [TestCase("#quantity:2,Range:1; @health:+1", 2, 2)]
        [TestCase("#quantity:2,Range:1; @health:+1", 3, 2)]
        [TestCase("#quantity:2,Range:1; @health:+1", 3, 0)]


        [TestCase("#quantity:1,Range:2; @health:+1", 1, 1)]
        [TestCase("#quantity:1,Range:2; @health:+1", 2, 1)]
        [TestCase("#quantity:1,Range:2; @health:+1", 3, 1)]
        [TestCase("#quantity:1,Range:2; @health:+1", 3, 0)]

        [TestCase("#quantity:2,Range:2; @health:+1", 1, 1)]
        [TestCase("#quantity:2,Range:2; @health:+1", 2, 2)]
        [TestCase("#quantity:2,Range:2; @health:+1", 3, 2)]
        [TestCase("#quantity:2,Range:2; @health:+1", 3, 0)]

        [TestCase("#quantity:3,Range:3; @health:+1", 1, 1)]
        [TestCase("#quantity:3,Range:3; @health:+1", 2, 2)]
        [TestCase("#quantity:3,Range:3; @health:+1", 3, 3)]
        [TestCase("#quantity:3,Range:3; @health:+1", 3, 0)]


        public void CardScriptWithTarget(string script, int totalEntities, int expectedEntities)
        {
            var testModel = TestBattleModel(totalEntities);
            var scriptParser = new CardScriptParser(testModel.AttributeMap);

            for (int i = 0; i < totalEntities; i++)
            {
                var entity1 = testModel.Entities[0];
                entity1.AttributeSet.Set(1, 0);

            }
            // Test source

            var runScriptParseResult = scriptParser.ParseScript(script, FindTargetsResolver.OnCardScriptFindTarget);

            // Assert Parse
            Debug.Log(runScriptParseResult.ErrorReason);
            Debug.Log(runScriptParseResult.LastCommandParseResult?.Exception);
            Assert.IsTrue(runScriptParseResult.Success);

            var runResult = RunScriptCommands(runScriptParseResult.ParsedCardScript.CardScriptCommands, testModel, testModel.Entities[0]);

            Assert.IsTrue(runResult);

            var modifiedEntities = testModel.Entities.Count(entity => entity.AttributeSet.GetValue(1) == 1);
            Assert.AreEqual(expectedEntities, modifiedEntities);

        }
        
        private CombatModel TestBattleModel(int players)
        {
            var combatModel = new CombatModel()
            {
                AttributeMap = new Dictionary<string, int>()
                {
                    { "damage", 0 },
                    { "health", 1 }
                },
                Entities = new(),
                GlobalAttributeSet = new(),
                Players = new()
            };
            

            for (int i = 0; i < players; i++)
            {
                var player = Player.Make($"Player{i}");
                player.AddNewCardCollection(CardCollectionIdentifier.Deck).InsertCards(new List<Card> { Card.Make(new CardData()) });
                var entity = Entity.Make($"Entity{i}", new GridPosition(i, 0), player);

                foreach (var attributeKey in combatModel.AttributeMap.Values)
                {
                    entity.AttributeSet.Set(attributeKey, 0);
                }

                combatModel.Players.Add(player);
                combatModel.Entities.Add(entity);
            }

            return combatModel;
        }
    }
}