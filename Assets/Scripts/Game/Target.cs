using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cubes
{
    /// <summary>
    /// The Target of the game. The target falls to the bottom on the screen. When clicked, it disable itself.
    /// </summary>
    public class Target : MonoBehaviour
    {
        [Tooltip("The fall speed of the target.")]
        private float _fallSpeed = 10.0f;

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime);
        }
    }
}
