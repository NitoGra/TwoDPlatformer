using System.Collections;
using UnityEngine;

namespace Scripts
{
    internal class Spawner : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _objectPrefab;
        [SerializeField] private float _spawnInterval = 5f;
        [SerializeField] private float _spawnCount = 5f;
        [SerializeField] private float _forceMagnitude = 5f;

        private Coroutine _spawnCoroutine;

        private void Start()
        {
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            int spawnCounter = 0;

            while (spawnCounter < _spawnCount)
            {
                yield return new WaitForSeconds(_spawnInterval);
                spawnCounter++;
                SpawnObject();
            }

            StopSpawning();
        }

        private void SpawnObject()
        {
            Rigidbody2D spawnedObject = Instantiate(_objectPrefab, transform.position, Quaternion.identity);
            ApplyRandomForce(spawnedObject);
            spawnedObject.transform.SetParent(transform);
        }

        private void ApplyRandomForce(Rigidbody2D rb)
        {
            float force = Random.Range(_forceMagnitude/ 2, _forceMagnitude);
            rb.AddForce(Random.insideUnitCircle.normalized *  force, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-1f, 1f) * _forceMagnitude * 0.1f, ForceMode2D.Impulse);
        }

        private void StopSpawning()
        {
            if (_spawnCoroutine == null) 
                return;
            
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }
}