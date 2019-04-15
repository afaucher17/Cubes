using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cubes
{
    /// <summary>
    /// Creates a pool of PoolableObject that can be reused to prevent too many expensive calls to the Garbage Collector.
    /// </summary>
    [Serializable]
    public class GameObjectPooler
    {
        [Header("Pooler Settings")]
        [SerializeField]
        [Tooltip("Prefab of the PoolableObject used by this instance of PoolableObjectPooler.")]
        private PoolableObject _prefab = null;
        [SerializeField]
        [Tooltip("The size of the pool of PoolableObjects that will be used and re-used by this instance of PoolableObjectPooler.")]
        private int _poolSize = 20;
        [SerializeField]
        [Tooltip("If true, when an instance of the stored PoolableObject is requested but no instance if available, a new instance of the stored PoolableObject will be created instead.")]
        private bool _expandable = false;
        [SerializeField]
        [Tooltip("If this field is set, the pooled PoolableObject will be instantiated as children of the given transform.")]
        private Transform _poolTransform = null;

        private List<PoolableObject> _pooledObjects;
        /// <summary>
        /// Prefab of the PoolableObject used by this instance of PoolableObjectPooler.
        /// This PoolableObject will be cloned an amount of times defined in the PoolSize property.
        /// </summary>
        public PoolableObject Prefab
        {
            get
            {
                return _prefab;
            }
        }
        /// <summary>
        /// The size of the pool of PoolableObjects that will be used and re-used by this instance of PoolableObjectPooler.
        /// </summary>
        public int PoolSize
        {
            get
            {
                return _poolSize;
            }
        }
        /// <summary>
        /// If true, when an instance of the stored PoolableObject is requested but no instance if available,
        /// a new instance of the stored PoolableObject will be created instead.
        /// </summary>
        public bool Expandable
        {
            get
            {
                return _expandable;
            }
        }

        public void Init()
        {
            _pooledObjects = new List<PoolableObject>();
            for (int i = 0; i < _poolSize; i++)
            {
                PoolableObject go = Instantiate(_prefab, _poolTransform);
                go.name += " " + i;
                go.Available = false;
                _pooledObjects.Add(go);
            }
        }

        private PoolableObject Instantiate(PoolableObject prefab, Transform transform = null)
        {
            return UnityEngine.Object.Instantiate(prefab, transform);
        }

        /// <summary>
        /// Gets the first inactive PoolableObject in the pool.
        /// </summary>
        /// <returns>The first inactive PoolableObject in the pool, null if there's no inactive PoolableObject and the Expandable property is set to false.</returns>
        public PoolableObject GetPooledObject()
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                PoolableObject go = _pooledObjects[i];
                if (!go.Available)
                {
                    go.Available = true;
                    return go;
                }
            }
            if (_expandable)
            {
                PoolableObject go = Instantiate(_prefab, _poolTransform);
                go.name += " " + _pooledObjects.Count;
                go.Available = false;
                _pooledObjects.Add(go);
            }
            return null;
        }
    }
}