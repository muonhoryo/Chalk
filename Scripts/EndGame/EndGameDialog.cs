


using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chalk
{
    public sealed class EndGameDialog : MonoBehaviour
    {
        [SerializeField] private GameObject MainMenuSkipperPrefab;

        private void Awake()
        {
            if (MainMenuSkipperPrefab == null)
                throw new System.NullReferenceException("Missing MainMenuSkipperPrefab.");

            gameObject.SetActive(false);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
        public void RestartSceneWithMainMenuSkip()
        {
            Instantiate(MainMenuSkipperPrefab);
            RestartScene();
        }
    }
}