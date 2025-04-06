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
            multiplayer.PlayerLeaveEvent();
            Destroy(gameObject);
            
        }
    }

    

    void Start()
    {
        multiplayer = MultiplayerManager.Instance;
        GetComponent<PlayerLogic>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
