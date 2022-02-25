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
        public string Text;
        public Sprite Image;

        public string EffectScriptText;

        private void OnValidate()
        {
                string thisFileNewName = "CardData" + "_" + Name;
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this.GetInstanceID());
                UnityEditor.AssetDatabase.RenameAsset(assetPath, thisFileNewName);
        }

        //public Dictionary<string, int> Attributes = new();
        public CardDataModel ToCardDataModel(int index)
        {
            return new CardDataModel()
            {
                Name = this.Name,
                Text = this.Text,
                Index = index
            };
        }
    }

}