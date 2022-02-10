using Assets.Scripts.CardSystem;

namespace Assets.Scripts
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