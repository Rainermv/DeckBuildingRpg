using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Controller;
using Assets.Scripts.Model;
using Assets.Scripts.Model.CharacterModel;
using Assets.Scripts.Systems.CardSystem.Utility;
using Assets.Scripts.Systems.CardSystem.Views;
using Assets.Scripts.Systems.GridSystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

namespace Assets.Scripts
{
    public class GameMain : SerializedMonoBehaviour
    {
        [SerializeField, SceneObjectsOnly] private LevelView _levelView;
        [SerializeField] private GridMapModelScriptableObject _gridMapModelScriptableObject;

        private LevelController _levelController;

        // Start is called before the first frame update
        void Start()
        {
            var levelModel = LevelModelFactory.Build(_gridMapModelScriptableObject.GridMapModel);

            _levelController = new LevelController(
                new RandomCardShuffler());

            levelModel = _levelController.Setup(levelModel);
            
            _levelView.Initialize(levelModel,
                cardView => _levelController.OnCardClicked(cardView.Card),
                cardCollectionView => _levelController.OnCardCollectionClicked(cardCollectionView.CardCollection),
                _levelController.OnGridPositionInput);


        }


    }
}