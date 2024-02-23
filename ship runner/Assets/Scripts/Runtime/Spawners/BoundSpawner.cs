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
            Spawn(Bound, new Vector3(12, 0, 0), Vector3.zero);
            Spawn(Bound, new Vector3(-12, 0, 0), new Vector3(0, 180, 0));
            const int offset = 20;
            float oldZPosition = 0;
            float newZPosition = 0;
            for (int i = 0; i < 65; i++)
            {
                newZPosition = oldZPosition + offset;
                Spawn(Bound, new Vector3(12, 0, newZPosition), Vector3.zero);
                Spawn(Bound, new Vector3(-12, 0, newZPosition), new Vector3(0, 180, 0));
                oldZPosition = newZPosition;
            }
        }
    }
}