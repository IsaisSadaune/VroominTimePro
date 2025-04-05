using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menu1;
    [SerializeField] private GameObject menu2;

    private static GameManager instance = null;
    public static GameManager Instance => instance;
    private MultiplayerManager multiplayerManager;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        multiplayerManager = MultiplayerManager.Instance;
    }
    public void ChangeMenu()
    {
        StartCoroutine(multiplayerManager.DisablePlayer(2));
        menu1.transform.DOMove(new Vector3(0, -48, 0), 1.3f).SetEase(Ease.InBack);
        menu2.transform.DOMove(new Vector3(0, 0, 0), 2).SetEase(Ease.OutQuad);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
