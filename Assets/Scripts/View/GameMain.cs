using Assets.Scripts.Controller;
using Assets.Scripts.Controller.CardShuffler;
using Assets.Scripts.Controller.Factories;
using Assets.Scripts.Controller.MovementResolver;
using Assets.Scripts.Core.Utility;
using Assets.Scripts.View.CardTemplate;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.View
{
    public class GameMain : SerializedMonoBehaviour
    {
        [SerializeField, AssetsOnly] private CardDataLibraryScriptableObject _cardDataLibrary;
        [SerializeField, SceneObjectsOnly] private BattleViewController _battleViewController;

        public Tilemap GridTilemap; 

        private BattleController _battleController;

        // Start is called before the first frame update
        void Start()
        {
            var gridMapModel = TilemapUtilities.GridMapModelFrom(GridTilemap);
            var cardDataModel = _cardDataLibrary.ToCardDataModelList();
            var battleModel = BattleModelFactory.Build(gridMapModel, cardDataModel);

            _battleController = new BattleController(
                new RandomCardShuffler(),
                new AStarPathFindResolver());

            battleModel = _battleController.Setup(battleModel);

            _battleViewController.Initialize(battleModel,
                _battleController.OnCardActivate,
                _battleController.OnGridFindPathToTarget,
                _battleController.OnGridMovePath, 
                _cardDataLibrary.ToSpriteLibrary());

        }


    }
}