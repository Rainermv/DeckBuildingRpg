using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Model.Card;
using UnityEngine;

namespace Assets.Scripts.View.CardTemplate
{
    [CreateAssetMenu(menuName = "Card/Card Data", fileName = "CardData")]
    public class CardDataScriptableObject : ScriptableObject
    {
        public string Name;
        public Sprite Image;

        [TextArea(15, 20)]
        public string CardScript;

        [TextArea(15, 20)]
        public string Text;


        private void OnValidate()
        {
                string thisFileNewName = "CardData" + "_" + Name;
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this.GetInstanceID());
                UnityEditor.AssetDatabase.RenameAsset(assetPath, thisFileNewName);
        }

        //public Dictionary<string, int> Attributes = new();
        public CardData ToCardDataModel(int index)
        {
            return new CardData()
            {
                Name = this.Name,
                Text = this.Text,
                Index = index,
                CardScript = CardScript
            };
        }
    }

}