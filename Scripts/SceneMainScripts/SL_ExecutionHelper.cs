using MuonhoryoLibrary.Unity.Debug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chalk.SL
{
    public sealed class SL_ExecutionHelper : MonoBehaviour
    {
        private void Awake()
        {
            SL_Executor.ExecutorComponent = this;
            DebugConsole.ConsoleCommandExecutedEvent += (comm) =>
            {
                SL_Executor.ExecuteSingleCommand(comm);
            };
        }
    }
}
