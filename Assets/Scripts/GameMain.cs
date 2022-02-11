using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Assets.Scripts.CardSystem;
using Assets.Scripts.CardSystem.Constants;
using Assets.Scripts.CardSystem.Models;
using Assets.Scripts.CardSystem.Models.Attributes;
using Assets.Scripts.CardSystem.Views;
using Assets.Scripts.Ruleset;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameMain : MonoBehaviour
    {
        public TextMeshProUGUI HealthText;

        public CardSystemViewController CardSystemViewController;
        private GameContext _gameContext;
        private IGameRuleset _gameRuleset;

        // Start is called before the first frame update
        void Start()
        {
            _gameContext = new GameContext()
            {
                CardSystemModel = CardSystemModelFactory.Build(),
                GlobalAttributeSet = new AttributeSet()
            };

            _gameRuleset = RulesetFactory.Build();
            _gameRuleset.Setup(_gameContext);


            CardSystemViewController.Initialize(_gameContext.CardSystemModel,
                cardView => _gameRuleset.OnCardClicked(cardView.Card),
                cardCollectionView => _gameRuleset.OnCardCollectionClicked(cardCollectionView.CardCollection));
            
            CardSystemViewController.DisplayPlayer(_gameContext.CardSystemModel.CardPlayers.Values.First());

            _gameContext.GlobalAttributeSet.OnAttributeValueChange = (s, i) =>
            {
                Debug.Log($"GLOBAL: {s} is now {i}");
                switch (s)
                {
                    case GlobalAttributeNames.ENEMY_HEALTH:
                        HealthText.text = $"{i}";
                        return;

                }
            };
            HealthText.text = $"{_gameContext.GlobalAttributeSet.GetValue(GlobalAttributeNames.ENEMY_HEALTH)}";


        }

    }
}