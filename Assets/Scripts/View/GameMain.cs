using Assets.Scripts.Controller.CardShuffler;
using Assets.Scripts.Controller.Factories;
using Assets.Scripts.Controller.MovementResolver;
using Assets.Scripts.Core.Utility;
using Assets.Scripts.View;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Controller
{
    public class GameMain : SerializedMonoBehaviour
    {
        [SerializeField, SceneObjectsOnly] private BattleViewController _battleViewController;

        //[SerializeField, AssetsOnly] private GridMapModelScriptableObject _gridMapModelScriptableObject;

        public Tilemap GridTilemap; 

        private BattleController _battleController;

        // Start is called before the first frame update
        void Start()
        {
            var gridMapModel = GridUtilities.GridMapModelFrom(GridTilemap);

            var battleModel = BattleModelFactory.Build(gridMapModel);

            _battleController = new BattleController(
                new RandomCardShuffler(),
                new AStarPathFindResolver());

            battleModel = _battleController.Setup(battleModel);

            _battleViewController.Initialize(battleModel,
                _battleController.OnCardActivate,
                _battleController.OnGridFindPathToTarget,
                _battleController.OnGridMovePath);



        }


    }
}