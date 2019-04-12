using UnityEngine;
using UnityEngine.UI;

namespace Cubes
{
    /// <summary>
    /// Text Component that displays the score property of the GameManager.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class UIScoreDisplay: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The game manager used to get the score value.")]
        private GameManager _gameManager;
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
            if (_gameManager == null)
                _gameManager = GameManager.Instance;
        }

        private void Update()
        {
            _text.text = string.Format("{0} {1}", _message, _gameManager.Score);
        }
    }
}
