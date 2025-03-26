using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MultiplayerManager : MonoBehaviour
{
    public List<GameObject> players;
    public List<Transform> spawnPoint;
    public bool settingUpPlayer = true;
    public float timer;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void StartRaceM()
    {
        foreach(GameObject car in players) 
        {
                car.GetComponent<PlayerInput>().SwitchCurrentActionMap("Car");
        }
        settingUpPlayer = false;

    }


    public void Victory(GameObject winner)
    {
        settingUpPlayer = true;
        foreach (GameObject car in players)
        {

            car.GetComponent<PlayerInput>().SwitchCurrentActionMap("Vide");
        }

    

        Invoke("StartPick", 2f);
    }

    void StartPick()
    {
        TPBack();

    }
    public void TPBack()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawnPoint[i].position;
            players[i].transform.rotation = spawnPoint[i].rotation;
        }

        foreach (GameObject car in players)
        {

            car.GetComponent<PlayerInput>().SwitchCurrentActionMap("BeforeRace");
        }
    }

}
