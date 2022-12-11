using System;

namespace Assets.Scripts.Core.Events
{
    public static class DebugEvents
    {
        public static Action<object, string> Log;

        public static Action<object, string> LogError;
    }
}