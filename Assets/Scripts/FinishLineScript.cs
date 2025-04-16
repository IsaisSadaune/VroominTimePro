using UnityEngine;
using UnityEngine.InputSystem;

public class FinishLineScript : MonoBehaviour
{
    private Collider coliderLine;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Car"))
        {
            playerFinishLine(other.gameObject);
        }
        if(gameManager.playersTimer.Count == MultiplayerManager.Instance.players.Count)
        {
            GameManager.Instance.EndRound();  
        }
    }

    private void playerFinishLine(GameObject player)
    {
        gameManager.AddPlayerToTimer(player);
    }

}
