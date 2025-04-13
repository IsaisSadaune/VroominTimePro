using UnityEngine;

public class LevelBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckForCarOnLevel()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.lossyScale);
        int numbeOfPlayer = 0;
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Car"))
            {
                numbeOfPlayer++;
            }
        }
        Debug.Log(numbeOfPlayer);
        if (numbeOfPlayer == MultiplayerManager.Instance.players.Count)
        {
            GameManager.Instance.ChangeMenu();
            GetComponent<Collider>().enabled = false;
        }
    }
}
