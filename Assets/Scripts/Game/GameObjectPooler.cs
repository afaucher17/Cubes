using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cubes
{
    /// <summary>
    /// Creates a pool of GameObject that can be reused to prevent too many expensive calls to the Garbage Collector.
    /// </summary>
    public class GameObjectPooler : MonoBehaviour
    {
        [Header("Pooler Settings")]
        [SerializeField]
        [Tooltip("Prefab of the GameObject used by this instance of GameObjectPooler.")]
        private GameObject _prefab = null;
        [SerializeField]
        [Tooltip("The size of the pool of GameObjects that will be used and re-used by this instance of GameObjectPooler.")]
        private int _poolSize = 20;
        [SerializeField]
        [Tooltip("If true, when an instance of the stored GameObject is requested but no instance if available, a new instance of the stored GameObject will be created instead.")]
        private bool _expandable = false;
        [SerializeField]
        [Tooltip("If this field is set, the pooled GameObject will be instantiated as children of the given transform.")]
        private Transform _poolTransform = null;

        private List<GameObject> _pooledObjects;
        /// <summary>
        /// Prefab of the GameObject used by this instance of GameObjectPooler.
        /// This GameObject will be cloned an amount of times defined in the PoolSize property.
        /// </summary>
        public GameObject Prefab
        {
            get
            {
                return _prefab;
            }
        }
        /// <summary>
        /// The size of the pool of GameObjects that will be used and re-used by this instance of GameObjectPooler.
        /// </summary>
        public int PoolSize
        {
            get
            {
                return _poolSize;
            }
        }
        /// <summary>
        /// If true, when an instance of the stored GameObject is requested but no instance if available,
        /// a new instance of the stored GameObject will be created instead.
        /// </summary>
        public bool Expandable
        {
            get
            {
                return _expandable;
            }
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _pooledObjects = new List<GameObject>();
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject go = Instantiate(_prefab, _poolTransform);
                go.gameObject.name += " " + i;
                go.SetActive(false);
                _pooledObjects.Add(go);
            }
        }

        /// <summary>
        /// Gets the first inactive GameObject in the pool.
        /// </summary>
        /// <returns>The first inactive GameObject in the pool, null if there's no inactive GameObject and the Expandable property is set to false.</returns>
        public GameObject GetPooledObject()
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                GameObject go = _pooledObjects[i];
                if (!go.activeInHierarchy)
                {
                    go.SetActive(true);
                    return go;
                }
            }
            if (_expandable)
            {
                GameObject go = Instantiate(_prefab, _poolTransform);
                go.name += " " + _pooledObjects.Count;
                go.SetActive(false);
                _pooledObjects.Add(go);
            }
            return null;
        }
    }
}