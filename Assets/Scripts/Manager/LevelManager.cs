using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cubes {
    /// <summary>
    /// Has methods to load the next scene or go back to the start.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        /// <summary>
        /// Returns to the first scene and resets the GameManager if there's one.
        /// </summary>
        public void LoadHome()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ResetScore();
            }
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// Go to the next scene.
        /// </summary>
        public void LoadNext()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }
}
