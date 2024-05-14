
using UnityEngine;
using UnityEditor;

namespace Chalk.SL
{
    public static partial class SL_Executor
    {
        public static void Command_Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
