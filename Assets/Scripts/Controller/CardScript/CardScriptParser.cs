using System;
using System.Collections.Generic;
using System.Globalization;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Command;
using Assets.Scripts.Core.Model.Entity;

namespace Assets.TestsEditor
{
    public class CardScriptParser
    {
        private Func<Entity, CombatModel, FindTargetData, List<ITargetable>> _onFindTarget;
        private Dictionary<string, int> _attributeMap;
        private IFormatProvider _provider;

        public CardScriptParser(Dictionary<string, int> attributeMap, 
            Func<Entity, CombatModel, FindTargetData, List<ITargetable>> onFindTarget)
        {
            _attributeMap = attributeMap;
            _onFindTarget = onFindTarget;
            _provider = CultureInfo.CurrentCulture;
        }

        public CardScriptParseResult ParseScript(string script)
        {
            var cardScriptCommands = new List<CardScriptCommand>();

            foreach (var scriptCommand in CardScriptParserUtilities.SplitOnSeparator(script, ParserCharacters.COMMAND_SEPARATOR))
            {
                var commandParseResult = new CardScriptCommandParseResult();

                switch (scriptCommand[0])
                {
                    case ParserCharacters.ATTRIBUTE_IDENTIFIER:
                    {
                        commandParseResult = ModifyAttributeCommand.Parse(scriptCommand[1..], _attributeMap);
                        break;
                    }

                    case ParserCharacters.TARGET_IDENTIFIER:
                        commandParseResult = FindTargetCommand.Parse(scriptCommand[1..], _onFindTarget);
                        break;
                }

                if (!commandParseResult.Success)
                {
                    return new CardScriptParseResult()
                    {
                        Success = false,
                        ErrorReason = $"Error parsing Command. {commandParseResult.ErrorReason}",
                        CardScriptCommands = cardScriptCommands,
                        LastCommandParseResult = commandParseResult
                    };
                }

                cardScriptCommands.Add(commandParseResult.CardScriptCommand);

            }


            return new CardScriptParseResult()
            {
                Success = true,
                CardScriptCommands = cardScriptCommands
            };
        }

        // #Enemies,Entities,Quantity:1,Range:1; 


        // @#health:-1


        //public delegate CardScriptCommandResult CardScriptCommand();
    }
}