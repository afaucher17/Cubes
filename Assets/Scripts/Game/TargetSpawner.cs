using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cubes
{
    public delegate void TargetSpawnerEventHandler(object sender);

    /// <summary>
    /// Spawns targets at a position and rate defined by its settings.
    /// </summary>
    public class TargetSpawner : MonoBehaviour
    {
        public event TargetSpawnerEventHandler TargetLimitHit;
        [Header("Target Spawner Settings")]
        [SerializeField]
        [Tooltip("The GameObjectPooler used to generate the targets.")]
        private GameObjectPooler _targetPooler = null;
        [SerializeField]
        [Tooltip("The Camera. The camera viewport is used to allow the targets to always spawn in a space the Camera can see them.")]
        private Camera _camera = null;
        [SerializeField]
        [Tooltip("The rect of the camera viewport where a target can spawn.")]
        private Rect _rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        [SerializeField]
        [Tooltip("Min z value of spawned target.")]
        private float _minZ = 10.0f;
        [SerializeField]
        [Tooltip("Max z value of spawned target.")]
        private float _maxZ = 50.0f;
        [SerializeField]
        [Tooltip("The number of target spawned each seconds.")]
        private float _spawnRate = 10.0f;
        [SerializeField]
        [Tooltip("When the number of target spawned hit the target limit, no further spawning are made.")]
        private int _targetLimit = 1000;

        private float _previousSpawnTime;

        public int SpawnCount { get; private set; }

        private void Start()
        {
            if (_camera == null)
                _camera = Camera.main;
            _previousSpawnTime = Time.time;
            _targetPooler.Init();
        }

        /// <summary>
        /// Gets a target from the target pool then places it at a randomized position defined by the bounds.
        /// </summary>
        /// <returns>True if a target has been successfully reused from the pool.</returns>
        public bool SpawnTarget()
        {
            if (_targetPooler != null)
            {
                PoolableObject go = _targetPooler.GetPooledObject();
                if (go != null)
                {
                    float x = Random.Range(_rect.min.x, _rect.max.x);
                    float y = Random.Range(_rect.min.y, _rect.max.y);
                    float z = Random.Range(_minZ, _maxZ);

                    SpawnCount++;
                    go.transform.position = _camera.ViewportToWorldPoint(new Vector3(x, y, z));
                    if (SpawnCount >= _targetLimit && TargetLimitHit != null)
                        TargetLimitHit(this);
                    return true;
                }
            }
            return false;
        }

        private void Update()
        {
            if ((SpawnCount < _targetLimit) && _spawnRate > 0.0f && ((Time.time - _previousSpawnTime) > (1.0f / _spawnRate)))
            {
                SpawnTarget();
                _previousSpawnTime = Time.time;
            }
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(TargetSpawner), true)]
        public class AnimationElementEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                var gm = (TargetSpawner)target;
                // Used for easier testing.
                if (GUILayout.Button("Create Target"))
                    gm.SpawnTarget();
            }
        }
#endif
    }


}
