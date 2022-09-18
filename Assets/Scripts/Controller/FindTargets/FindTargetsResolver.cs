using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.EntityModel;
using Assets.Scripts.Core.Utility;
using Assets.TestsEditor;

namespace Assets.Scripts.Controller
{
    public class FindTargetsResolver
    {
        public static List<ITargetable> OnCardScriptFindTarget(Entity source, CombatModel combatModel, FindTargetData findTargetData)
        {
            var parameters = findTargetData.Parameters;

            switch (findTargetData.Mode)
            {
                case FindTargetModes.SELF:
                    return new List<ITargetable>() { source };
                case FindTargetModes.RADIUS: //todo probably not requried anymore
                    return new List<ITargetable>(
                        EntitiesOnRadius(source,
                            combatModel.Entities,
                            findTargetData,
                            GetValueInt(parameters,
                                FindTargetsParameters.MAGNITUDE)));


            }

            return new List<ITargetable>();
        }


        private static string GetValue(Dictionary<string, string> parameters, string key)
        {
            if (parameters.TryGetValue(key, out var value))
                return value;

            DebugEvents.OnLogError(parameters, $"(GetValue) Could not get value of parameter {key}. Returning empty");
            return string.Empty;

        }

        private static int GetValueInt(Dictionary<string, string> parameters, string key)
        {
            if (!parameters.TryGetValue(key, out var value))
            {
                DebugEvents.OnLogError(parameters, $"(GetValueInt) Could not get value of parameter {key}. Returning 0");
                return 0;
            }

            if (int.TryParse(value, out var result)) 
                return result;

            DebugEvents.OnLogError(parameters, $"Could not parse {value} of parameter {key} of key  to int. Returning 0");
            return 0;

        }

        private static List<Entity> EntitiesOnRadius(Entity source, List<Entity> entities,
            FindTargetData findTargetData, int magnitude)
        {
            throw new NotImplementedException();
            //return entities.Where(entity => GridUtilities.DistancePrecise(source.GridPosition, entity.GridPosition) <= magnitude).ToList();
        }
    }
}