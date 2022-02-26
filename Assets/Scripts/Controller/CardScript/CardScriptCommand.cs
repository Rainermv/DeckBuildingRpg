using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Command;
using Assets.Scripts.Core.Model.Entity;

namespace Assets.TestsEditor
{
    public class CardScriptCommand
    {
        public Func<Entity, CommandPlayData, CombatModel, bool> OnValidatePlay { get; set; }
        public Func<Entity, CommandPlayData, CombatModel, CommandPlayData> OnPlay { get; set; }
    }
}