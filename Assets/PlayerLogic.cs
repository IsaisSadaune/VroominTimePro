using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    
    private MultiplayerManager multiplayer;
    public void Leave(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            multiplayer.players.Remove(gameObject);
            multiplayer.PlayerLeaveEvent(gameObject);
            Destroy(gameObject);
            
        }
    }

    

    void Awake()
    {
        multiplayer = MultiplayerManager.Instance;
        multiplayer.players.Add(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        multiplayer.SpawnSpecificPlayer(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
