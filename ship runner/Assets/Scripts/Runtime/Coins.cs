using UnityEngine;

public class Coins : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        GameController.instance.player.AddCoin(5);
        Destroy(gameObject);
    }
}
