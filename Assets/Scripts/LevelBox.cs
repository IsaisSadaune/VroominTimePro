using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string sceneName;
    private Vector3 scaleBox;
    void Start()
    {
        MultiplayerManager.Instance.PlayerLeaveEvent += CheckForCarOnLevel;
        scaleBox = GetComponent<BoxCollider>().size;
        scaleBox = new Vector3(scaleBox.x * transform.lossyScale.x, scaleBox.y * transform.lossyScale.y, scaleBox.z * transform.lossyScale.z);
    }

    // Update is called once per frame
  
    private void OnTriggerEnter(Collider other)
    {
        CheckForCarOnLevel();
    }

    private void CheckForCarOnLevel()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, scaleBox);
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
            SceneManager.LoadScene(sceneName);
        }
    }

    private void CheckForCarOnLevel(GameObject leavingPlayer)
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, scaleBox);
        int numbeOfPlayer = 0;
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Car") && collider.gameObject != leavingPlayer)
            {
                numbeOfPlayer++;
            }
        }
        if (numbeOfPlayer > 0 && numbeOfPlayer == MultiplayerManager.Instance.players.Count)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
