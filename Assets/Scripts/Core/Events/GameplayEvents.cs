using System;
using Assets.Scripts.Core.Model.Cards;
using Assets.Scripts.Core.Model.GridMap;

namespace Assets.Scripts.View.Cards
{
    public static class GameplayEvents
    {
        public static Action<Card, int> OnCardEvent;

    }
}