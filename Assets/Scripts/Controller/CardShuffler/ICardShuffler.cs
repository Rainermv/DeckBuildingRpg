using System.Collections.Generic;
using Assets.Scripts.Core.Model.Card;

namespace Assets.Scripts.Controller.CardShuffler
{
    public interface ICardShuffler
    {
        List<CardModel> Run(List<CardModel> cards);
    }
}