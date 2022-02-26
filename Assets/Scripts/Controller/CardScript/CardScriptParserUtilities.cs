using System.Linq;

namespace Assets.TestsEditor
{
    public class CardScriptParserUtilities
    {
        public static (string key, string value ) ParseKeyValue(string symbol)
        {
            var split = symbol.Split(ParserCharacters.KEYVALUE_SEPARATOR);

            return (split[0], split[1]);
        }

        public static string[] SplitOnSeparator(string text, string separator)
        {
            return text.Split(separator)
                .Select(l => l.Trim().ToLower())
                .Where(l => !string.IsNullOrEmpty(l))
                .ToArray();
        }
    }
}