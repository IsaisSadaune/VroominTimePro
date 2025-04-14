using UnityEngine;
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
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position,scaleBox);
    }
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
        Debug.Log(numbeOfPlayer);
        if (numbeOfPlayer == MultiplayerManager.Instance.players.Count)
        {
            GameManager.Instance.GoToVroominScene(sceneName);
           
        }
    }
}
