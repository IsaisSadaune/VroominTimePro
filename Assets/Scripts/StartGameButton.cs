using System.Globalization;
using System.Threading;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    private MultiplayerManager multiplayerManager;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        multiplayerManager = MultiplayerManager.Instance;
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    

    private void OnTriggerEnter(Collider other)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position,2.2f);
        int numbeOfPlayer = 0;
        foreach (Collider collider in hitColliders)
        {
            if(collider.gameObject.CompareTag("Car"))
            {
                numbeOfPlayer++;
            }
        }
        Debug.Log(numbeOfPlayer);
        if(numbeOfPlayer == multiplayerManager.players.Count)
        {
            gameManager.ChangeMenu();
            GetComponent<Collider>().enabled = false;
        }
        
    }
}
