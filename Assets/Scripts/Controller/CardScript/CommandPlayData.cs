using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.Model.Command;

namespace Assets.TestsEditor
{
    public class CommandPlayData
    {
        public List<ITargetable> targetables { get; set; } = new();
    }
}