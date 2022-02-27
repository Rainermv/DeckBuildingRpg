using System;

namespace Assets.Scripts.Core.Model
{
    public static class MDebug
    {
        public static Action<object, string> OnLogFromController;
        
        internal static void Log(object sourceObject, string log)
        {
            OnLogFromController?.Invoke(sourceObject, log);
        }

        public static event Action<object, string> OnLogErrorFromController;

        internal static void LogError(object sourceObject, string log)
        {
            OnLogErrorFromController?.Invoke(sourceObject, log);
        }

    }
}