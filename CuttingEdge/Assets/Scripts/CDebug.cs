using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace CustomDebug
{
    public static class CDebug
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(object o)
        {
            Debug.Log(o);
           // Debug.LogError(o);
        }

        [Conditional("UNITY_EDITOR")]
        public static void LogError(object o)
        {
            Debug.LogError(o);
        }


        [Conditional("UNITY_EDITOR")]
        public static void ColorLog(object o, string color = "red")
        {
            Debug.Log("<color="+ color + ">"+ o +"</color>");
        }

    }

}


