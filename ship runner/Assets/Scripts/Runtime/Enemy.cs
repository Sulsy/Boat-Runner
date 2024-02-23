using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        GameController.instance.player.currentBoat.Hp -= 10;
        if (GameController.instance.player.currentBoat.Hp <= 0)
        {
            GameController.instance.Lose();
        }

        gameObject.AddComponent<MoveObject>().direction = Vector3.down;
    }
}