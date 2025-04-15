using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;
    public static LevelManager Instance => instance;

    private MultiplayerManager multi;
    private GameManager gameManager;
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
        multi = MultiplayerManager.Instance;
        gameManager = GameManager.Instance;
        SceneManager.sceneLoaded += StartSceneVroomin;

    }
    // Update is called once per frame
    public void GoToRace()
    {
        multi.SwitchPlayersToVroominTime();
        multi.playerInputManager.DisableJoining();
        gameManager.StartDecompte();
    }

    private void StartSceneVroomin(Scene scene, LoadSceneMode mode)
    {
        multi.SpawnAllPlayers();

        if (scene.name != "TestControles")
        {
            GoToRace();
        }
        else
        {
            GoToMenu();
        }
    }

    public void GoToMenu()
    {
        
    }

}
