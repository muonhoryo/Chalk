


using Chalk.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chalk
{
    public sealed class MainMenuSkipper : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene i,LoadSceneMode j)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            GameObject.FindAnyObjectByType<MainMenuUI>().StartGame();
            Destroy(gameObject);
        }
    }
}