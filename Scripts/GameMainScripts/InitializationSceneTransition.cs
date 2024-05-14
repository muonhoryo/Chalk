
using Chalk.Exceptions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chalk.Initialization
{
    public sealed class InitializationSceneTransition : MonoBehaviour
    {
        [SerializeField] private string NextSceneName;
        private void Start()
        {
            if (string.IsNullOrEmpty(NextSceneName))
                throw new ChalkMissingValueException("NextSceneName");

            SceneManager.LoadScene(NextSceneName);
        }
    }
}