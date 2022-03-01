using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.EntityModel;

namespace Assets.TestsEditor
{
    public class CardScriptCommand
    {
        public Func<Entity, CardScriptCommandPlayData, CombatModel, bool> OnValidatePlay { get; set; }
        public Func<Entity, CardScriptCommandPlayData, CombatModel, CardScriptCommandPlayData> OnPlay { get; set; }
        public string ScriptCommandText { get; set; }
    }
}