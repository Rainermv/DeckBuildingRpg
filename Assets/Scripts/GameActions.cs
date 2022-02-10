using System;
using System.Threading.Tasks;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts
{
    internal class GameActions
    {
        public GameActions(Func<CardCollection, CardCollection, int, Task> onDrawCards)
        {
            OnDrawCards = onDrawCards;
        }

        public Func<CardCollection, CardCollection, int, Task> OnDrawCards { get; }
    }
}