﻿
using UnityEngine;
using UnityEngine.UI;

namespace MuonhoryoLibrary.Unity.Debug
{
    public sealed class DebugConsoleInputField_ : MonoBehaviour
    {
        [SerializeField] private Text TextComponent;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void ConsoleInputChangedAction(string newInput)
        {
            TextComponent.text = newInput;
        }
        private void ConsoleActivationAction()
        {
            gameObject.SetActive(true);
            DebugConsole.InputChangedEvent += ConsoleInputChangedAction;
            DebugConsole.ConsoleCommandExecutedEvent += ConsoleInputChangedAction;
            ConsoleInputChangedAction(DebugConsole.ConsoleInput);
        }
        private void ConsoleDeactivationAction()
        {
            gameObject.SetActive(false);
            DebugConsole.InputChangedEvent -= ConsoleInputChangedAction;
            DebugConsole.ConsoleCommandExecutedEvent -= ConsoleInputChangedAction;
        }
        private void Start()
        {
            DebugConsoleController_.Instance_.ConsoleControllerEnabledEvent += ConsoleActivationAction;
            DebugConsoleController_.Instance_.ConsoleControllerDisabledEvent += ConsoleDeactivationAction;
            ConsoleDeactivationAction();
        }
    }
}
