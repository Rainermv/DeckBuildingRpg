using System;

namespace Assets.Scripts.Core.Events
{
    public static class DebugEvents
    {
        public static Action<object, string> OnLog;

        public static Action<object, string> OnLogError;
    }
}