using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Model.Card;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.View.CardTemplate
{
    [CreateAssetMenu(menuName = "Card/Card Data Library", fileName = "CardDataLibrary")]
    public class CardDataLibraryScriptableObject : ScriptableObject
    {
        [AssetsOnly] public List<CardDataScriptableObject> CardDataList;

        public List<CardDataModel> ToCardDataModelList()
        {
            var index = 0;
            return CardDataList.Select(dataScriptable =>
            {
                return dataScriptable.ToCardDataModel(index++);
            }).ToList();
        }

        public List<Sprite> ToSpriteLibraryList()
        {
            return CardDataList.Select(dataScriptable => dataScriptable.Image).ToList();
        }

        public CardSpriteLibrary ToSpriteLibrary()
        {
            return new CardSpriteLibrary(CardDataList.Select(dataScriptable => dataScriptable.Image).ToList());
        }
    }
}