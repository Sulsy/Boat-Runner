using UnityEngine;

namespace Spawners
{
    public class BoundSpawner : Spawner
    {
        [SerializeField] private GameObject Bound;

        private void Start()
        {
            BoundSpawn();
        }

        private void BoundSpawn()
        {
            Spawn(Bound, new Vector3(10, 1.6F, 0), Vector3.zero, Vector3.zero, 0, false, true, true,
                RigidbodyConstraints.FreezePosition);
            Spawn(Bound, new Vector3(-10, 1.6F, 0), Vector3.zero, new Vector3(0, 180, 0), 0, false, true, true,
                RigidbodyConstraints.FreezePosition);
            const int offset = 10;
            float oldZPosition = 0;
            float newZPosition = 0;
            for (int i = 0; i < 30; i++)
            {
                newZPosition = oldZPosition + offset;
                Spawn(Bound, new Vector3(10, 1.6F, newZPosition), Vector3.zero, Vector3.zero, 0, false, true, true,
                    RigidbodyConstraints.FreezePosition);
                Spawn(Bound, new Vector3(-10, 1.6F, newZPosition), Vector3.zero, new Vector3(0, 180, 0), 0, false, true, true,
                    RigidbodyConstraints.FreezePosition);
                oldZPosition = newZPosition;
            }
        }
    }
}