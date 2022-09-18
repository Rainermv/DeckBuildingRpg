using System;
using Assets.Scripts.Core.Model.Cards;

namespace Assets.Scripts.View.Cards
{
    public static class GameplayEvents
    {
        public static Action<Card, int> OnCardEvent;

    }
}