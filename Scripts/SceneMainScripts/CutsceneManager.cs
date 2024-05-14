


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chalk.Cutscenes
{
    public sealed class CutsceneManager:MonoBehaviour
    {
        public event Action<Cutscene> UpdateCutscenesListEvent = delegate { };

        public static CutsceneManager inst_ { get; private set; }

        public readonly struct Cutscene
        {
            public Cutscene(int Index,Sprite Icon , Sprite SelectedIcon, Sprite[] SceneImages)
            {
                if (Index < 0)
                    throw new ArgumentException("Index must be more than or equal 0.");
                if (SceneImages == null || SceneImages.Length == 0)
                    throw new ArgumentNullException("Missing SceneImages.");
                if (Icon == null)
                    throw new ArgumentNullException("Missing Icon.");
                if (SelectedIcon == null)
                    throw new ArgumentNullException("Missing SelectedIcon.");

                this.Index = Index;
                this.SceneImages = SceneImages;
                this.Icon = Icon;
                this.SelectedIcon = SelectedIcon;
            }

            public readonly int Index;
            public readonly Sprite[] SceneImages;
            public readonly Sprite Icon;
            public readonly Sprite SelectedIcon;
        }

        private SortedList<int, Cutscene> CutscenesList = new SortedList<int, Cutscene>();

        public Cutscene[] GetShowedScenes() =>
            CutscenesList.Select((element) => element.Value).ToArray();

        public void AddCutscene(Cutscene cutscene)
        {
            if (!CutscenesList.ContainsKey(cutscene.Index))
            {
                CutscenesList.Add(cutscene.Index, cutscene);
                UpdateCutscenesListEvent.Invoke(cutscene);
            }
        }
        public Cutscene GetSceneByIndex(int index) =>
            CutscenesList[index];
        public int CutscenesCount() => CutscenesList.Count;

        private void Awake()
        {
            inst_ = this;
        }
    }
}