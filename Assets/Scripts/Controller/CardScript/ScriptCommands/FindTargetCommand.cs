using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Command;
using Assets.Scripts.Core.Model.Entity;

namespace Assets.TestsEditor
{
    public static class FindTargetCommand
    {
        private const string PARAMETER_QUANTITY = "quantity";
        private const string PARAMETER_RANGE = "range";
        private const int PARAMETER_QTY_DEFAULT_VALUE = 1;
        private const int PARAMETER_RANGE_DEFAULT_VALUE = 1;

        public static CardScriptCommandParseResult Parse(string scriptCommand,
            Func<Entity, CombatModel, FindTargetData, List<ITargetable>> onFindTarget)
        {
            var commandParseResult = new CardScriptCommandParseResult();

            try
            {
                var targetParameters = new Dictionary<string, int>();
                var tags = new List<string>();

                foreach (var targetCommandSymbol in CardScriptParserUtilities.SplitOnSeparator(scriptCommand, ParserCharacters.COMMA_SEPARATOR))
                {
                    if (targetCommandSymbol.Contains(ParserCharacters.KEYVALUE_SEPARATOR))
                    {
                        // add as parameter
                        var keyValueSymbol = CardScriptParserUtilities.ParseKeyValue(targetCommandSymbol);
                        targetParameters.Add(keyValueSymbol.key, int.Parse(keyValueSymbol.value));
                        continue;
                    }

                    // add as a tag
                    tags.Add(targetCommandSymbol);
                }

                var FindTargetData = new FindTargetData()
                {
                    Quantity = targetParameters.TryGetValue(PARAMETER_QUANTITY, out var quantity)? quantity : PARAMETER_QTY_DEFAULT_VALUE,
                    Range = targetParameters.TryGetValue(PARAMETER_RANGE, out var range) ? range : PARAMETER_RANGE_DEFAULT_VALUE,
                    Tags = tags
                };

                commandParseResult.CardScriptCommand = new CardScriptCommand()
                {
                    OnPlay = (source, commandPlayData, combatModel) =>
                    {
                        commandPlayData.targetables = onFindTarget(source, combatModel, FindTargetData);
                        return commandPlayData;

                    }
                };

                commandParseResult.Success = true;
                return commandParseResult;

            }
            catch (Exception e)
            {
                commandParseResult.Success = false;
                commandParseResult.Exception = e;
                commandParseResult.ErrorReason = $"[FindTargetCommand][{scriptCommand}]Exception found {e.Message}";
                return commandParseResult;
            }
        }
    }
}