using System;
using System.Collections.Generic;
using Assets.Scripts.Controller;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.EntityModel;

namespace Assets.TestsEditor
{
    public static class FindTargetCommand
    {
        

        public static CardScriptCommandParseResult Parse(string scriptCommand,
            Func<Entity, CombatModel, FindTargetData, List<ITargetable>> onFindTarget)
        {
            var commandParseResult = new CardScriptCommandParseResult();

            try
            {
                var findTargetsData = new FindTargetData()
                {
                    Parameters = new Dictionary<string, string>(),
                    Tags = new List<string>()
                };

                foreach (var targetCommandSymbol in CardScriptParserUtilities.SplitOnSeparator(scriptCommand, ParserCharacters.COMMA_SEPARATOR))
                {
                    
                    if (targetCommandSymbol.Contains(ParserCharacters.KEYVALUE_SEPARATOR))
                    {

                        var keyValueSymbol = CardScriptParserUtilities.ParseKeyValue(targetCommandSymbol);

                        // add as mode
                        if (keyValueSymbol.key == FindTargetsParameters.MODE)
                        {
                            findTargetsData.Mode = keyValueSymbol.value;
                            continue;
                        }

                        // add as parameter
                        findTargetsData.Parameters.Add(keyValueSymbol.key, keyValueSymbol.value);
                        continue;
                    }

                    // add as a tag
                    findTargetsData.Tags.Add(targetCommandSymbol);
                }

                if (string.IsNullOrEmpty(findTargetsData.Mode))
                {
                    DebugEvents.LogError(findTargetsData, $"No {FindTargetsParameters.MODE} found in FindTargetCommand");
                    return commandParseResult;
                }

                commandParseResult.CardScriptCommand = new CardScriptCommand()
                {
                    OnPlay = (source, commandPlayData, combatModel) =>
                    {
                        commandPlayData.targetables = onFindTarget(source, combatModel, findTargetsData);
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