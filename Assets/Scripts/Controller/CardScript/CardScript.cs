using System.Collections.Generic;

namespace Assets.TestsEditor
{
    public class CardScript
    {
        public CardScript(List<CardScriptCommand> cardScriptCommands)
        {
            CardScriptCommands = cardScriptCommands;
        }

        public List<CardScriptCommand> CardScriptCommands { get; private set; }
    }
}