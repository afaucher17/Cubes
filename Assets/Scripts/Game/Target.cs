using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cubes
{
    /// <summary>
    /// The Target of the game. The target falls to the bottom on the screen. When clicked, it disable itself.
    /// </summary>
    public class Target : PoolableObject
    {
        [Header("Target settings.")]
        [SerializeField]
        [Tooltip("The fall speed of the target.")]
        private float _fallSpeed = 10.0f;
        [SerializeField]
        [Tooltip("Duration of the hit animation (in seconds).")]
        private float _hitAnimationDuration = 0.25f;

        private Vector3 _startingScale;

        private void Awake()
        {
            _startingScale = transform.localScale;
        }

        private void OnBecameInvisible()
        {
            Available = false;
        }

        protected override void OnAvailable()
        {
            base.OnAvailable();
            transform.localScale = _startingScale;
        }

        /// <summary>
        /// Starts the hit animation of the target then disable hit when the animation is done.
        /// </summary>
        public void Hit()
        {
            StartCoroutine(HitAnimation());
        }

        private IEnumerator HitAnimation()
        {
            float start = Time.time;
            while ((Time.time - start) < _hitAnimationDuration)
            {
                transform.localScale = Vector3.Lerp(_startingScale, Vector3.zero, (Time.time - start) / _hitAnimationDuration);
                yield return null;
            }
            Available = false;
        }

        private void Update()
        {
            transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime);
        }
    }
}
