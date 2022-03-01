using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.Cards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.View.Cards.CardDataView
{
    [CreateAssetMenu(menuName = "Card/Card Data Library", fileName = "CardDataLibrary")]
    public class CardDataLibraryScriptableObject : ScriptableObject
    {
        [AssetsOnly] public List<CardDataScriptableObject> CardDataList;

        public List<CardData> ToCardDataModelList()
        {
            var index = 0;
            return CardDataList.Select(dataScriptable =>
            {
                return dataScriptable.ToCardDataModel(index++);
            }).ToList();
        }

        public CardSpriteLibrary ToSpriteLibrary()
        {
            return new CardSpriteLibrary(CardDataList.Select(dataScriptable => dataScriptable.Image).ToList());
        }
    }
}