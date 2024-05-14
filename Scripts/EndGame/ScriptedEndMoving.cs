using System;
using System.Collections;
using System.Collections.Generic;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk.Player
{
    public sealed class ScriptedEndMoving : MonoBehaviour,IActiveModule
    {
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };

        [SerializeField] private MonoBehaviour[] LockedScripts;
        [SerializeField] private MonoBehaviour MovingModule;
        [SerializeField] private MonoBehaviour MovingDirCalculatorChanger;

        private IActiveModule[] ParsedLockedScripts;
        private IMovingModule ParsedMovingModule;
        private ModuleSelector<IMovingDirectionCalculator> ParsedMovingDirCalculatorChanger;

        [SerializeField] private Vector3 MovingDirection;
        [SerializeField] private EndGameArea EndGameArea;
        [SerializeField] private EndGame EndGame;
        [SerializeField] private int SelectorIndex;

        private bool IsActive = false;

        public bool IsActive_
        {
            get => IsActive;
            set
            {
                if (IsActive_ != value)
                {
                    IsActive = value;
                    enabled = value;
                    if (IsActive)
                        ActivateModuleEvent();
                    else
                        DeactivateModuleEvent();
                }
            }
        }

        private void Awake()
        {
            ParsedLockedScripts = new IActiveModule[LockedScripts.Length];
            for(int i=0; i < LockedScripts.Length; i++)
            {
                ParsedLockedScripts[i] = LockedScripts[i] as IActiveModule;
                if (ParsedLockedScripts[i] == null)
                    throw new System.NullReferenceException("Missing LockedScripts at index=" + i + ".");
            }

            if (EndGameArea == null)
                throw new System.NullReferenceException("Missing EndGameArea.");
            ParsedMovingModule = MovingModule as IMovingModule;
            if (ParsedMovingModule == null)
                throw new System.NullReferenceException("Missing MovingModule.");
            ParsedMovingDirCalculatorChanger = MovingDirCalculatorChanger as ModuleSelector<IMovingDirectionCalculator>;
            if (ParsedMovingDirCalculatorChanger == null)
                throw new NullReferenceException("Missing MovingDirCalculatorChanger.");

            EndGameArea.PlayerEnterEvent += OnEnterArea;
            EndGame.EndGameEvent += OnEndGame;
            enabled = false;
        }
        private void OnEnterArea()
        {
            foreach (var scr in ParsedLockedScripts)
                scr.IsActive = false;
            IsActive_ = true;
            EndGameArea.PlayerEnterEvent -= OnEnterArea;
            ParsedMovingDirCalculatorChanger.SelectModule(SelectorIndex);
            ParsedMovingModule.SetMovingDirection(MovingDirection);
        }
        private void OnEndGame()
        {
            IsActive_ = false;
            ParsedMovingModule.StopMoving();
        }

        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
    }
}
