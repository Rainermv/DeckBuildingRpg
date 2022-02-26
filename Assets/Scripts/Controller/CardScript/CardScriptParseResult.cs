using System.Collections.Generic;

namespace Assets.TestsEditor
{
    public class CardScriptParseResult
    {
        public bool Success { get; set; }
        public string ErrorReason { get; set; }

        public List<CardScriptCommand> CardScriptCommands { get; set; }
        public CardScriptCommandParseResult LastCommandParseResult { get; set; }
    }
}