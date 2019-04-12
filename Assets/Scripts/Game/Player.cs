using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cubes
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The main camera used to convert the position of the click / touch to world values.")]
        private Camera _camera = null;
        [SerializeField]
        [Tooltip("The target spawner used to check if the spawn limit has been hit.")]
        private TargetSpawner _targetSpawner = null;
        [SerializeField]
        [Tooltip("Level Manager used to load the next scene.")]
        private LevelManager _levelManager = null;

        private void Start()
        {
            if (_targetSpawner != null)
                _targetSpawner.TargetLimitHit += TargetLimitHit;
        }

        private void TargetLimitHit(object sender)
        {
            if (_levelManager != null)
                _levelManager.LoadNext();
        }

        private void CheckHit()
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "Target")
                {
                    hit.collider.gameObject.SetActive(false);
                    GameManager.Instance.ScorePoints(hit.distance);
                }
            }
        }

        private void Update()
        {
            if (_camera != null && Input.GetMouseButtonDown(0))
                CheckHit();
        }
    }
}