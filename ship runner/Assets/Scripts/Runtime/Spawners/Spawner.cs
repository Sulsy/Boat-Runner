using UnityEngine;

namespace Spawners
{
    public class Spawner : MonoBehaviour
    {
        protected static GameObject Spawn(GameObject prefab, Vector3 position, Vector3 rotation)
        {
            var spawnedGameObject = Instantiate(prefab, position, Quaternion.identity);

            spawnedGameObject.transform.rotation = Quaternion.Euler(rotation);

            return spawnedGameObject;
        }
    }
}