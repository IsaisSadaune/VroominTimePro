using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

public class MultiplayerManager : MonoBehaviour
{
    private static MultiplayerManager instance = null;
    public static MultiplayerManager Instance => instance;

    public delegate void LeaveHandler();
    public LeaveHandler PlayerLeaveEvent;

    public List<GameObject> players;
    public List<Transform> spawnPoint;
    public bool settingUpPlayer = true;
    public float timer;
    private PlayerInputManager playerInputManager;

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
        playerInputManager = GetComponent<PlayerInputManager>();
        SceneManager.sceneLoaded += StartSceneVroomin;
    }

  
    private void StartSceneVroomin(Scene scene, LoadSceneMode mode)
    {
        SpawnAllPlayers();

    }

    private void SpawnAllPlayers()
    {
        foreach (GameObject car in players)
        {
            car.GetComponent<CarMovementPhysics>().SpawnPlayer();
        }
        StartCoroutine(DisablePlayer(2));
    }
    
    

    public void ChangeMenu(GameObject oldMenu, GameObject newMenu)
    {
        StartCoroutine(DisablePlayer(2));
        oldMenu.transform.DOMove(new Vector3(0, -48, 0), 1.3f).SetEase(Ease.InBack);
        newMenu.transform.DOMove(new Vector3(0, 0, 0), 2).SetEase(Ease.OutQuad);

    }

    public IEnumerator DisablePlayer(float waitTime)
    {
        playerInputManager.DisableJoining();
        foreach (GameObject car in players)
        {
            car.GetComponent<PlayerInput>().DeactivateInput();
        }
        yield return new WaitForSeconds(2);
        foreach (GameObject car in players)
        {
            car.GetComponent<PlayerInput>().ActivateInput();
        }
        playerInputManager.EnableJoining();

    }
}
