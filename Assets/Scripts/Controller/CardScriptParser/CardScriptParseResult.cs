using System.Collections.Generic;

namespace Assets.TestsEditor
{
    public class CardScriptParseResult
    {
        public bool Success { get; set; }
        public string ErrorReason { get; set; }

        public CardScriptCommandParseResult LastCommandParseResult { get; set; }
        public CardScript ParsedCardScript { get; set; }
    }
}