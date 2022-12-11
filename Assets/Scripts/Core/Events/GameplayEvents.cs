using System;
using Assets.Scripts.Core.Model.Cards;

namespace Assets.Scripts.Core.Events
{
    public static class GameplayEvents
    {
        public static Action<Card, int> OnCardEvent;

    }
}