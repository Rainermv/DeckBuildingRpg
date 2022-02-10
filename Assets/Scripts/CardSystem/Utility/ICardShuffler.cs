using System.Collections.Generic;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.Collection;

namespace Assets.Scripts.CardSystem
{
    public interface ICardShuffler
    {
        List<Card> Run(List<Card> cards);
    }
}