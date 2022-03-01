using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Assets.TestsEditor
{
    public class ModifyAttributeCommand
    {
        public static CardScriptCommandParseResult Parse(string commandText, Dictionary<string, int> attributeMap)
        {
            var commandParseResult = new CardScriptCommandParseResult();
            try
            {
                commandParseResult.CommandText = commandText;

                var symbol= CardScriptParserUtilities.ParseKeyValue(commandText);

                commandParseResult.AttributeName = symbol.key;
                if (!attributeMap.TryGetValue(commandParseResult.AttributeName, out var attributeKey))
                {
                    return new CardScriptCommandParseResult()
                    {
                        Success = false,
                        CommandText = commandText,
                        ErrorReason = $"Coud not find attribute {commandParseResult.AttributeName} in Attribute Map"
                    };
                }

                commandParseResult.AttributeKey = attributeKey;


                commandParseResult.CardScriptCommand = new CardScriptCommand()
                {
                    OnValidatePlay = (source, commandPlayData, combatModel) =>
                    {
                        return commandPlayData.targetables.All(target => target.Attributes.Contains(attributeKey));
                    }
                };


                if (symbol.value[0] == ParserCharacters.EQUAL_SIGN)
                {
                    var setIntValue = int.Parse(symbol.value[1..]);

                    commandParseResult.CardScriptCommand.OnPlay = (source, commandPlayData, combatModel) =>
                    {
                        foreach (var targetable in commandPlayData.targetables)
                        {
                            targetable.Attributes.SetValue(attributeKey, setIntValue);
                        };

                        return commandPlayData;
                    };

                    commandParseResult.Success = true;
                    return commandParseResult;
                }

                var modifyIntValue = int.Parse(symbol.value, NumberStyles.AllowLeadingSign);

                commandParseResult.CardScriptCommand.OnPlay = (source, commandPlayData, combatModel) =>
                {
                    foreach (var targetable in commandPlayData.targetables)
                    {
                        targetable.Attributes.ModifyValue(attributeKey, modifyIntValue);
                    };

                    return commandPlayData;
                };

                commandParseResult.Success = true;
                return commandParseResult;
            }
            catch (Exception e)
            {
                commandParseResult.Success = false;
                commandParseResult.Exception = e;
                commandParseResult.ErrorReason = $"Exception found {e.Message}";
                return commandParseResult;

            }
        }
    }
}