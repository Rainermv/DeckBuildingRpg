using System.Collections.Generic;
using Assets.Scripts.CardSystem.Models;

namespace Assets.Scripts.CardSystem.Utility
{
    public interface ICardShuffler
    {
        List<Card> Run(List<Card> cards);
    }
}