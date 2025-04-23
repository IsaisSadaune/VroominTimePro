using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    
    private MultiplayerManager multiplayer;
    private PlayerInput carInput;
    private CarMovementPhysics physicsScript;

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
        carInput = GetComponent<PlayerInput>();
        physicsScript = GetComponent<CarMovementPhysics>();


        DontDestroyOnLoad(this.gameObject);
        DisableAllMaps();
    }

    private void Start()
    {
        SpawnPlayer(multiplayer.spawnPoint[multiplayer.players.IndexOf(gameObject)]);
        carInput.SwitchCurrentActionMap("MenuinTimeAM");

    }

  

    public void SpawnPlayer(Transform transformToCopy)
    {

        transform.position = transformToCopy.position + Vector3.up / 5;
        transform.rotation = transformToCopy.rotation;
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        physicsScript.isAccelerating = 0;
        physicsScript.speedActu = 0;
    }
    public void DisableAllMaps()
    {
        foreach (var map in carInput.actions.actionMaps)
        {

            map.Disable();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerReady(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            Debug.Log("ready");
            //j'ai desactivé ça temporairement
            //MapManager.InstanceMapManager.PlaceTile(0);
            GameManager.Instance.AddReadyPlayer();

        }
    }

    public void RotateRight(InputAction.CallbackContext ctx)
    {
        if ((ctx.started))
        {
        MapManager.InstanceMapManager.RotateRight(1);

        }
    }
    public void RotateLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            MapManager.InstanceMapManager.RotateLeft(1);
        }
    }
    public void Left(InputAction.CallbackContext ctx)
    {
        if ((ctx.started))
        {
            MapManager.InstanceMapManager.Left(1);
        }

    }
    public void Right(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            MapManager.InstanceMapManager.Right(1);
        }
    }
    public void Up(InputAction.CallbackContext ctx)
    {
        if ((ctx.started))
        {
            MapManager.InstanceMapManager.Up(1);
        }

    }
    public void Down(InputAction.CallbackContext ctx)
    {
        if ((ctx.started))
        {
            MapManager.InstanceMapManager.Down(1);
        }
    }
}
