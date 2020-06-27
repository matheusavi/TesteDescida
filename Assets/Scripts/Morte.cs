using UnityEngine;

public class Morte : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameManager>().EndGame();
    }
}
