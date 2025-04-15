using System.Globalization;
using System.Threading;
using NUnit.Framework.Constraints;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MultiplayerManager.Instance.PlayerLeaveEvent += CheckForStartGame;

    }

    // Update is called once per frame
    

    private void OnTriggerEnter(Collider other)
    {
       CheckForStartGame(null);
        
    }

    private void CheckForStartGame(GameObject leavingPlayer)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.2f);
        int numbeOfPlayer = 0;
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Car"))
            {
                numbeOfPlayer++;
            }
        }
        if (numbeOfPlayer > 0 && numbeOfPlayer == MultiplayerManager.Instance.players.Count)
        {
            GameManager.Instance.ChangeMenu();
            enabled = false;
        }

    }
}
