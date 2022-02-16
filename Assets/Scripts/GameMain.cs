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
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameMain : MonoBehaviour
    {

        public GameObject CharacterPrefab;

        public Camera MainCamera;

        public TextMeshProUGUI HealthText;

        [PreviouslySerializedAs("CardSystemViewController")]
        public CardSystemView CardSystemView;

        public GridSystemView GridSystemView;

        private IGameController _gameController;

        // Start is called before the first frame update
        void Start()
        {
            var gameControllerSettings = new GameControllerSettings()
            {
                MapWidth = 20,
                MapHeight = 20
            };
            
            _gameController = GameControllerFactory.Build();
            var gameModel = _gameController.SetupWithSettings(gameControllerSettings);

            GridSystemView.Initialize(gameModel.GridSystemModel, OnGetCursorWorldPosition);


            GridSystemView.SetThingTo(Instantiate(CharacterPrefab).transform
                , new Vector3Int(0, 0));

            CardSystemView.Initialize(gameModel.CardSystemModel,
                cardView => _gameController.OnCardClicked(cardView.Card),
                cardCollectionView => _gameController.OnCardCollectionClicked(cardCollectionView.CardCollection));

            CardSystemView.DisplayPlayer(gameModel.CardSystemModel.CardPlayers.Values.First());

            gameModel.GlobalAttributeSet.OnAttributeValueChange = (s, i) =>
            {
                Debug.Log($"GLOBAL: {s} is now {i}");
                switch (s)
                {
                    case AttributeKey.Health:
                        HealthText.text = $"{i}";
                        return;

                }
            };
            HealthText.text = $"{gameModel.GlobalAttributeSet.GetValue(AttributeKey.Health)}";

            MainCamera.transform.position += GridSystemView.GridCenter;



        }


        private Vector3 OnGetCursorWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private float minSize = 5;
        private float maxSize = 10;
        float sensitivity = 10f;
        private float speed = 10f;

        void Update()
        {
            Mathf.Clamp(MainCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * sensitivity, minSize, maxSize);

            MainCamera.transform.position += (Vector3.up * speed * Input.GetAxis("Vertical") * Time.deltaTime );
            MainCamera.transform.position += (Vector3.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime);

        }

    }
}