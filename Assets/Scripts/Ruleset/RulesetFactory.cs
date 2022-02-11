using Assets.Scripts.CardSystem.Utility;

namespace Assets.Scripts.Ruleset
{
    internal class RulesetFactory
    {
        public static IGameRuleset Build()
        {
            return new StandardGameRuleset(
                new RandomCardShuffler());
        }
    }
}