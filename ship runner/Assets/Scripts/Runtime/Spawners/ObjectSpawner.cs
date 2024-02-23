using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] 
        private List<GameObject> Objects;

        private void Start()
        {
            foreach (var eGameObject in Objects)
            {
                var position = eGameObject.transform.position;
                position = new Vector3(Random.Range(-8, 8), position.y,
                    position.z);
                eGameObject.transform.position = position;
            }
        }
    }
}