using System.Collections.Generic;
using Assets.Scripts.Controller;
using Assets.Scripts.Controller.CardShuffler;
using Assets.Scripts.Controller.Factories;
using Assets.Scripts.Core.Events;
using Assets.Scripts.View.Cards.CardDataView;
using Assets.TestsEditor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class GameMain : SerializedMonoBehaviour
    {
        [SerializeField, AssetsOnly] private CardDataLibraryScriptableObject _cardDataLibrary;
        [SerializeField, SceneObjectsOnly] private CombatViewController _combatViewController;

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

            var cardDataModel = _cardDataLibrary.ToCardDataModelList();
            var combatModel = BattleModelFactory.Build(cardDataModel, attributeMap);
            
            _combatController = new CombatController(
                new RandomCardShuffler(),
                new CardScriptParser(attributeMap));
            
            combatModel = _combatController.Setup(combatModel);

            _combatViewController.Initialize(combatModel, 
                _cardDataLibrary.ToSpriteLibrary());

            _combatController.OnCombatStart();

        }

        private void InitializeDebugEvents()
        {
            DebugEvents.Log += OnLog;
            DebugEvents.LogError+= OnLogError;
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