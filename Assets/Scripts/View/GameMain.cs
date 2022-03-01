using System.Collections.Generic;
using Assets.Scripts.Controller;
using Assets.Scripts.Controller.CardShuffler;
using Assets.Scripts.Controller.Factories;
using Assets.Scripts.Controller.MovementResolver;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Utility;
using Assets.Scripts.View.Cards.CardDataView;
using Assets.TestsEditor;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.View
{
    public class GameMain : SerializedMonoBehaviour
    {
        [SerializeField, AssetsOnly] private CardDataLibraryScriptableObject _cardDataLibrary;
        [SerializeField, SceneObjectsOnly] private CombatViewController _combatViewController;

        public Tilemap GridTilemap; 

        private CombatController _combatController;

        // Start is called before the first frame update
        void Start()
        {
            InitializeDebugEvents();
            
            // todo: move this to a scriptable object or something
            var attributeMap = new Dictionary<string, int>()
            {
                { "power", 0 },
                { "health", 1 },
                
            };


            var gridMapModel = TilemapUtilities.GridMapModelFrom(GridTilemap);
            var cardDataModel = _cardDataLibrary.ToCardDataModelList();
            var combatModel = BattleModelFactory.Build(gridMapModel, cardDataModel, attributeMap);
            
            _combatController = new CombatController(
                new RandomCardShuffler(),
                new AStarPathFindResolver(),
                new CardScriptParser(attributeMap));
            
            combatModel = _combatController.Setup(combatModel);

            _combatViewController.Initialize(combatModel,
                _combatController.OnGridFindPathToPosition,
                _combatController.OnGridMovePath, 
                _cardDataLibrary.ToSpriteLibrary());

            _combatController.OnCombatStart();

        }

        private void InitializeDebugEvents()
        {
            DebugEvents.OnLog += OnLog;
            DebugEvents.OnLogError+= OnLogError;
        }

        private void OnLogError(object sourceObject, string error)
        {
            Debug.LogError($"[{sourceObject.GetType().Name}] {error}");
        }
        private void OnLog(object sourceObject, string log)
        {
            Debug.Log($"[{sourceObject.GetType().Name}] {log}");
        }
    }
}