using Assets.Scripts.CardSystem.Utility;

namespace Assets.Scripts.Ruleset
{
    internal class GameControllerFactory
    {
        public static IGameController Build()
        {
            return new StandardGameController(new RandomCardShuffler());
        }
    }
}