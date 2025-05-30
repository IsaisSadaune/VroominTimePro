using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menu1;
    [SerializeField] private GameObject menu2;

    private static GameManager instance = null;
    public static GameManager Instance => instance;
    private MultiplayerManager multiplayerManager;
    public Dictionary<GameObject, float> playersTimer = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, int> playersScore;

    private bool timerRunning;
    private float raceTimer;

    private int numberOfPlayerReady;
    
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
        StartCoroutine(multiplayerManager.DisableJoining(2));
        menu1.transform.DOMove(new Vector3(0, -48, 0), 1.3f).SetEase(Ease.InBack);
        menu2.transform.DOMove(new Vector3(0, 0, 0), 2).SetEase(Ease.OutQuad);

    }
    // Update is called once per frame
    void Update()
    {
        if(timerRunning)
        {
            raceTimer += Time.deltaTime;

        }
    }

 
    public void StartDecompte()
    {
        StartCoroutine(Decompte());
        StartCoroutine(multiplayerManager.DisablePlayer(3));

    }

    private IEnumerator Decompte()
    {
        Debug.Log(3);
        yield return new WaitForSeconds(1);
        Debug.Log(2);
        yield return new WaitForSeconds(1);
        Debug.Log(1);
        yield return new WaitForSeconds(1);
        Debug.Log("GO !");
        raceTimer = 0;
        timerRunning = true;
    }

    public void AddPlayerToTimer(GameObject player)
    {
        playersTimer.Add(player, raceTimer);
        player.GetComponent<PlayerInput>().DeactivateInput();
        if (playersTimer.Count == multiplayerManager.players.Count)
        {
            EndRound();
        }
    }


    public void StartRound()
    {
        multiplayerManager.SwitchPlayersActionMap("VroominTimeAM");
        StartDecompte();

    }

    public void EndRound()
    {
        foreach (var line in playersTimer)
        {
            Debug.Log($"Player{multiplayerManager.players.IndexOf(line.Key) + 1} : {line.Value}");
        }
        playersTimer.Clear();
        StartTileinTime();
    }


    //________________________________________________________________________________TEMPORAIRE________________________________

    public void StartTileinTime() 
    {
        multiplayerManager.SpawnAllPlayers();
        multiplayerManager.SwitchPlayersActionMap("TileinTime");
        Debug.Log("Mettez Vous Pr�t");
        numberOfPlayerReady = 0;
        
    }

    public void AddReadyPlayer()
    {
        numberOfPlayerReady++;
        if(numberOfPlayerReady == multiplayerManager.players.Count)
        {
            StartRound();
        }
    }



}
