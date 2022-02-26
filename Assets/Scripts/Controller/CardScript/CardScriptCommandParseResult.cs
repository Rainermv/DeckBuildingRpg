using System;
using System.Collections.Generic;

namespace Assets.TestsEditor
{
    public class CardScriptCommandParseResult
    {
        public CardScriptCommand CardScriptCommand { get; set; }
        public bool Success { get; set; }
        public string ErrorReason { get; set; }
        public string CommandText { get; set; }
        public string AttributeName { get; set; }
        public int AttributeKey { get; set; }
        public Exception Exception { get; set; }
    }
}