using System;
using System.Collections;
using System.Collections.Generic;
using Chalk.Player;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;

namespace Chalk
{
    public sealed class EndGame : MonoBehaviour
    {
        public event Action EndGameEvent = delegate { };

        [SerializeField] private MonoBehaviour EndGameDistanceProvider;
        [SerializeField] private Transform TrackedObject;
        [SerializeField] private GameObject EndGameDialog;
        [SerializeField] private GameObject GUI;

        private IConstProvider<float> ParsedEndGameDistanceProvider;

        private void Awake()
        {
            ParsedEndGameDistanceProvider = EndGameDistanceProvider as IConstProvider<float>;
            if (ParsedEndGameDistanceProvider == null)
                throw new System.NullReferenceException("Missing EndGameDistanceProvider.");
        }

        private void Update()
        {
            if (TrackedObject.transform.position.magnitude >= ParsedEndGameDistanceProvider.GetValue())
                End();
        }
        private void End()
        {
            PlayerController.instance_.IsActive_ = false;
            Cursor.visible = true;
            EndGameDialog.SetActive(true);
            GUI.SetActive(false);
            EndGameEvent();
        }
    }
}
