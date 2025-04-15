using UnityEngine;

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
            gameManager.AddPlayerToTimer(other.gameObject);
        }
        if(gameManager.playersTimer.Count == MultiplayerManager.Instance.players.Count)
        {
            LevelManager.Instance.EndRound();  
        }
    }
    
}
