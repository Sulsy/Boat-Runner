using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EnemyShipSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> enemyPrefab;

        private EnemyShip _enemyShip;
        private bool _isStop;

        public void DeSpawn()
        {
            _enemyShip.AddComponent<MoveObject>().direction = Vector3.down;
        }

        public void Stop()
        {
            _enemyShip = null;
            _isStop = true;
        }

        private void Start()
        {
            _enemyShip = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], transform)
                .GetComponent<EnemyShip>();
            _enemyShip.AddComponent<MoveObject>().direction = Vector3.up;
            _isStop = false;
        }

        private void Update()
        {
            if (!_isStop)
            {
                if (_enemyShip.IsDestroyed())
                {
                    _enemyShip = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], transform)
                        .GetComponent<EnemyShip>();
                    _enemyShip.AddComponent<MoveObject>().direction = Vector3.up;
                }
            }
        }
    }
}