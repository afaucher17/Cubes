using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cubes
{
    /// <summary>
    /// Manages the score of the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [Header("Game Settings")]
        [SerializeField]
        [Tooltip("If the target hit is at a distance lower than this value, the player will get the minimal amount of points.")]
        private float _minDistance = 5.0f;
        [SerializeField]
        [Tooltip("If the target hit is at a distance greater than this value, the player will get the maximal amount of points.")]
        private float _maxDistance = 20.0f;
        [SerializeField]
        [Tooltip("The minimal amount of point you can get on hit.")]
        private int _minPoints = 1;
        [SerializeField]
        [Tooltip("The maximal amount of point you can get on hit.")]
        private int _maxPoints = 10;

        /// <summary>
        /// The score of the player.
        /// </summary>
        public int Score { get; private set; }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void ScorePoints(float hitDistance)
        {
            hitDistance = Mathf.Clamp(hitDistance, _minDistance, _maxDistance);
            int points = (int)(_minPoints + ((_maxPoints - _minPoints) * ((hitDistance - _minDistance) / (_maxDistance - _minDistance))));
            Score += points;
        }

        public void ResetScore()
        {
            Score = 0;
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}