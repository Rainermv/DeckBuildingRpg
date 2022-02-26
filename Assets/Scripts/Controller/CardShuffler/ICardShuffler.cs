using System.Collections.Generic;
using Assets.Scripts.Core.Model.Card;

namespace Assets.Scripts.Controller.CardShuffler
{
    public interface ICardShuffler
    {
        List<Card> Run(List<Card> cards);
    }
}