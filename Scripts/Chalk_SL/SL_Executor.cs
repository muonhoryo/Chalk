
using System.Linq;
using System.Collections;
using Chalk.Exceptions;
using UnityEngine;

namespace Chalk.SL
{
    public static partial class SL_Executor
    {
        public static SL_ExecutionHelper ExecutorComponent;

        public static void ExecuteCommandsList(string[] commands)
        {
            int delayCommIndex;
            for (delayCommIndex = 0; delayCommIndex < commands.Length; delayCommIndex++)
            {
                int spaceIndex = commands[delayCommIndex].IndexOf(' ');
                if (spaceIndex == -1)
                    continue;
                string commandIdentifier = commands[delayCommIndex].Substring(0, spaceIndex);
                if (commandIdentifier == "Delay")
                {
                    float time;
                    {
                        //Parse time value
                        int lengthOfSerializedTime = commands[delayCommIndex].IndexOf(' ', spaceIndex + 1);
                        if (lengthOfSerializedTime == -1)
                            lengthOfSerializedTime = commands[delayCommIndex].Length - spaceIndex - 1;
                        else
                            lengthOfSerializedTime = lengthOfSerializedTime - spaceIndex - 1;

                        string serializedTimeValue = commands[delayCommIndex].Substring(spaceIndex + 1, lengthOfSerializedTime);

                        if (!float.TryParse(serializedTimeValue, out time))
                            throw new ChalkParsingException("Time");
                    }
                    string[] delayedCommands = commands.Skip(delayCommIndex + 1).Take(commands.Length - delayCommIndex - 1).ToArray();
                    ExecutorComponent.StartCoroutine(DelayedCommandsExecution(delayedCommands, time));
                    break;
                }
            }
            for (int i = 0; i < delayCommIndex; i++)
            {
                ExecuteSingleCommand(commands[i]);
            }
        }
        private static IEnumerator DelayedCommandsExecution(string[] commands, float time)
        {
            yield return new WaitForSeconds(time);
            ExecuteCommandsList(commands);
        }

        /// <summary>
        /// If true command dont end script execution.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="AIADException"></exception>
        public static void ExecuteSingleCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                throw new ChalkMissingValueException("Command");
            }
            ExecuteCommandBySyntax(command.Split(' '));
        }


        private static void ExecuteCommandBySyntax(string[] syntax)
        {
            switch (syntax[0])
            {
                case "Quit": Command_Quit(); return;

                default:
                    throw new ChalkException($"Unknown command {syntax[0]}.");
            }
        }
    }
}
