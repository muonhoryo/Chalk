using System.Collections;
using System.Collections.Generic;
using Chalk.Cutscenes;
using UnityEngine;

namespace Chalk
{
    public sealed class Door : MonoBehaviour
    {
        [SerializeField] private int ItemsCount;
        [SerializeField] private Animator animator;
        [SerializeField] private string OpenDoorTriggerAnim;

        private void Awake()
        {
            if (ItemsCount <= 0)
                throw new System.Exception("ItemsCount must be more than 0.");

        }
        private void Start()
        {
            CutsceneManager.inst_.UpdateCutscenesListEvent += UpdateCutsceneList;
        }
        private void UpdateCutsceneList(CutsceneManager.Cutscene i)
        {
            if (CutsceneManager.inst_.CutscenesCount() >= ItemsCount)
                OpenDoor();
        }
        private void OpenDoor()
        {
            animator.SetTrigger(OpenDoorTriggerAnim);
        }
    }
}
