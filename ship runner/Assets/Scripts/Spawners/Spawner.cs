using UnityEngine;

namespace Spawners
{
    public class Spawner : MonoBehaviour
    {
        public static GameObject Spawn(GameObject prefab, Vector3 position, Vector3 direction, Vector3 rotation,
            float speed, bool isUseGravity, bool isKinematic, bool freezePosition, RigidbodyConstraints freezeRotation)
        {
            var spawnedGameObject = Instantiate(prefab, position, Quaternion.identity);

            if (!spawnedGameObject.TryGetComponent<Rigidbody>(out var rb))
            {
                spawnedGameObject.AddComponent<Rigidbody>();
            }
            
            spawnedGameObject.transform.rotation = Quaternion.Euler(rotation);
          
            return spawnedGameObject;
        }
    }
}