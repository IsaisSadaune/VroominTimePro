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

    public delegate void LeaveHandler(GameObject leavingPlayer);
    public LeaveHandler PlayerLeaveEvent;

    public List<GameObject> players;
    public List<Transform> spawnPoint;
    public bool settingUpPlayer = true;
    public float timer;
    public PlayerInputManager playerInputManager;

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
        
    }

    private void Start()
    {
        
    }

    public void SpawnAllPlayers()
    {
        foreach (GameObject car in players)
        {
            car.GetComponent<PlayerLogic>().SpawnPlayer(spawnPoint[players.IndexOf(car)]);
        }
    }

    public void ChangeMenu(GameObject oldMenu, GameObject newMenu)
    {
        StartCoroutine(DisablePlayer(2));
        oldMenu.transform.DOMove(new Vector3(0, -48, 0), 1.3f).SetEase(Ease.InBack);
        newMenu.transform.DOMove(new Vector3(0, 0, 0), 2).SetEase(Ease.OutQuad);
    }


    public IEnumerator DisablePlayer(float waitTime)
    {
        foreach (GameObject car in players)
        {
            car.GetComponent<PlayerInput>().DeactivateInput();
        }
        yield return new WaitForSeconds(waitTime);
        foreach (GameObject car in players)
        {
            car.GetComponent<PlayerInput>().ActivateInput();
        }

    }
    public IEnumerator DisableJoining(float waitTime)
    {

        playerInputManager.DisableJoining();
        yield return new WaitForSeconds(waitTime);
        playerInputManager.EnableJoining();
         
    }
    public void SwitchPlayersActionMap(string actionMapName)
    {
        foreach (GameObject car in players)
        {

            car.GetComponent<PlayerInput>().SwitchCurrentActionMap(actionMapName);
        }
    }

}
