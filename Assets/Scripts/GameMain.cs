using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Controller;
using Assets.Scripts.Controller.CardShuffler;
using Assets.Scripts.Controller.MovementResolver;
using Assets.Scripts.GridMapSerializer;
using Assets.Scripts.Model;
using Assets.Scripts.View;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

namespace Assets.Scripts
{
    public class GameMain : SerializedMonoBehaviour
    {
        [SerializeField, SceneObjectsOnly] private LevelViewController _levelViewController;

        [SerializeField, AssetsOnly] private GridMapModelScriptableObject _gridMapModelScriptableObject;

        private LevelController _levelController;

        // Start is called before the first frame update
        void Start()
        {
            var levelModel = LevelModelFactory.Build(_gridMapModelScriptableObject.GridMapModel);

            _levelController = new LevelController(
                new RandomCardShuffler(),
                new ManhattanMovementResolver());
                //new SingleMovementResolver());

            levelModel = _levelController.Setup(levelModel);

            _levelViewController.Initialize(levelModel,
                cardView => _levelController.OnCardClicked(cardView.CardModel),
                cardCollectionView => _levelController.OnCardCollectionClicked(cardCollectionView.CardCollectionModel),
                _levelController.OnFindPathToTarget,
                _levelController.OnExecuteMovement);



        }


    }
}