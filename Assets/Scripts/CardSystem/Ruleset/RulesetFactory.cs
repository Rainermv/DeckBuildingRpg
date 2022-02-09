using Assets.Scripts.CardSystem;

namespace Assets.Scripts
{
    internal class RulesetFactory
    {
        public static ICardRuleset Build()
        {
            return new CardRuleset(
                new RandomCollectionShuffler());
        }
    }
}