
using UnityEngine;

namespace Cubes
{
    /// <summary>
    /// A poolable object
    /// </summary>
    public class PoolableObject : MonoBehaviour
    {
        protected bool _available;
        /// <summary>
        /// If true, the object is considered poolable.
        /// </summary>
        public bool Available
        {
            get
            {
                return _available;
            }
            set
            {
                if (value)
                    OnAvailable();
                else
                    OnUnavailable();
                _available = value;
            }
        }

        protected virtual void OnAvailable()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnUnavailable()
        {
            gameObject.SetActive(false);
        }
    }
}