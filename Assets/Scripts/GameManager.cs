using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void EndGame()
    {
        FindObjectOfType<Husky>().Reset();
        Debug.Log("Game over");
    }
}
