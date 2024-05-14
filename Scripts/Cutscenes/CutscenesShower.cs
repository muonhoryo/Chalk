

using System;
using System.Collections;
using Chalk.Player;
using MuonhoryoLibrary.Unity.COM;
using UnityEngine;
using UnityEngine.UI;

namespace Chalk.Cutscenes 
{
    public sealed class CutscenesShower : MonoBehaviour 
    {
        public static CutscenesShower inst_ { get; private set; }

        public event Action<CutsceneManager.Cutscene> StartShowingSceneEvent = delegate { };
        public event Action<Sprite> ChangeCutsceneImageEvent = delegate { };
        public event Action ShowingSceneDoneEvent = delegate { };

        [SerializeField] private Image ImageShower;

        [SerializeField] private MonoBehaviour ShowingImageTimeProvider;

        private IConstProvider<float> ParsedShowingImageTimeProvider;

        private void Awake()
        {
            ParsedShowingImageTimeProvider = ShowingImageTimeProvider as IConstProvider<float>;
            if (ParsedShowingImageTimeProvider == null)
                throw new NullReferenceException("Missing ShowingImageTimeProvider.");

            inst_ = this;
        }
        private void Start()
        {
            CutsceneManager.inst_.UpdateCutscenesListEvent += ShowScene;
            gameObject.SetActive(false);
        }
        public void ShowScene(int index)
        {
            ShowScene(CutsceneManager.inst_.GetSceneByIndex(index));
        }
        public void ShowScene(CutsceneManager.Cutscene showedScene)
        {
            PlayerController.instance_.IsActive_ = false;
            gameObject.SetActive(true);
            StartCoroutine(ShowImages(showedScene));
            StartShowingSceneEvent(showedScene);
        }

        private IEnumerator ShowImages(CutsceneManager.Cutscene showedScene)
        {
            for(int i = 0; i < showedScene.SceneImages.Length; i++)
            {
                ImageShower.sprite = showedScene.SceneImages[i];
                ChangeCutsceneImageEvent(showedScene.SceneImages[i]);
                yield return new WaitForSeconds(ParsedShowingImageTimeProvider.GetValue());
            }
            PlayerController.instance_.IsActive_ = true;
            gameObject.SetActive(false);
            ShowingSceneDoneEvent.Invoke();
        }
    }
}