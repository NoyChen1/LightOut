using System.Collections.Generic;
using UnityEngine;

    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _availableObjects = new();
        private readonly T _prefab;
        private readonly Transform _parentTransform;

        public ObjectPool(T prefab, int initialSize, Transform parentTransform = null)
        {
            _prefab = prefab;
            _parentTransform = parentTransform;

            for (var i = 0; i < initialSize; i++)
            {
                ExpandPool();
            }
        }

        // Retrieve an object from the pool
        public T Get()
        {
            if (_availableObjects.Count == 0)
            {
                ExpandPool();
            }

            T obj = _availableObjects.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        // Return an object to the pool
        public void Release(T obj)
        {
            if (obj == null)
            {
                return;
            }

            obj.gameObject.SetActive(false);
            _availableObjects.Enqueue(obj);
        }

        // Expand the pool by creating additional instances
        private void ExpandPool()
        {
            T newObject = Object.Instantiate(_prefab, _parentTransform);
            newObject.gameObject.SetActive(false);
            _availableObjects.Enqueue(newObject);
        }
    }

