using UnityEngine;
using UnityEngine.UI;

namespace Cubes
{
    /// <summary>
    /// Text component that displays the SpawnCount property of a TargetSpawner.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class UITargetSpawnDisplay : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The Target Spawner used to get the target spawn value.")]
        private TargetSpawner _targetSpawner;
        [SerializeField]
        [Tooltip("The text component on which the spawn is to be displayed.")]
        private Text _text;
        [SerializeField]
        [Tooltip("Message to be displayed before the spawn value.")]
        private string _message = "";

        private void Reset()
        {
            if (_text == null)
                _text = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            if (_targetSpawner == null)
                _targetSpawner = FindObjectOfType<TargetSpawner>();
        }

        private void Update()
        {
            _text.text = string.Format("{0} {1}", _message, _targetSpawner.SpawnCount);
        }
    }
}
