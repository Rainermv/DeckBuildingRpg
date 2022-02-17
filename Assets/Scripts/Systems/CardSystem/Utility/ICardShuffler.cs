using System.Collections.Generic;
using Assets.Scripts.Model.CardModel;

namespace Assets.Scripts.Systems.CardSystem.Utility
{
    public interface ICardShuffler
    {
        List<Card> Run(List<Card> cards);
    }
}