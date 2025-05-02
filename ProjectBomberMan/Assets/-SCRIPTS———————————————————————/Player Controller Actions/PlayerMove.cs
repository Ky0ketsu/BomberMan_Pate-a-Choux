using UnityEngine;
using Rewired;
//using UnityEditor.Animations;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Range(0,3)]public int playerID = 0; // Rewired plugin
    public bool CanRun{get{return canRun;}set{canRun=value;}}
    bool canRun=false;
    public bool CanFall{get{return canFall;}set{canFall=value;}}
    bool canFall=false;
    [Range(0,100f)]public float maxSpeed = 5f;
    public float startMaxSpeed;
    Vector2 inputs;
    Vector3 movement, movementRelativeToCam;
    CharacterController character => GetComponent<CharacterController>();
    PlayerLook lookScript => GetComponent<PlayerLook>();
    Player player; // Rewired plugin

    public RuntimeAnimatorController[] animatorsChoux = new RuntimeAnimatorController[4];
    public RuntimeAnimatorController[] animatorsEclair = new RuntimeAnimatorController[4];

    public bool pushActive;

    void Awake()
    {
        EVENTS.OnGameplay += EnableMoveSet;
        EVENTS.OnGameplayExit += DisableMoveSet;
    }

    void OnDestroy()
    {
        EVENTS.OnGameplay -= EnableMoveSet;
        EVENTS.OnGameplayExit -= DisableMoveSet;
    }

    void Start()
    {
        startMaxSpeed = maxSpeed;
        player = ReInput.players.GetPlayer(playerID);
        if (GAME.MANAGER.CurrentState==State.gameplay) EnableMoveSet();
    }

    void EnableMoveSet()
    {
        CanRun = CanFall = true;
        /*if (playerID == 1 || playerID == 3) ApplyMove(1, 2);
        if (playerID == 0 || playerID == 2) ApplyMove(0, 2);*/
    }

    void DisableMoveSet()
    {
        CanRun = CanFall = false;
    }

    /*public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<PlayerMove>())
        {
            if(inputs.x < 0)
        }
    }*/



    void Update()
    {
        HorizontalMovement();
        VerticalMovement();
        ApplyMovement();
    }

    void GetInputs()
    {
        inputs.x = player.GetAxis("MoveHorizontal");
        inputs.y = player.GetAxis("MoveVertical");
        if (inputs.sqrMagnitude>1f) inputs.Normalize(); // avoir diagonals bigger than 1 (pythagoras)
    }

    void VerticalMovement()
    {
        movement.y= CanFall ? -10f : 0; // very simple fall with constant speed
    }

    void HorizontalMovement()
    {
        GetInputs();
        movement.x = CanRun ? inputs.x * maxSpeed : 0;
        movement.z = CanRun ? inputs.y * maxSpeed : 0;

        /*if(playerID == 1 || playerID == 3)
        {
            if (inputs.x <= -1) ApplyMove(1, 0);
            if (inputs.x >= 1) ApplyMove(1, 1);
            if (inputs.y <= -1) ApplyMove(1, 2);
            if (inputs.y >= 1) ApplyMove(1, 3);
        }
        if(playerID == 0 || playerID == 2)
        {
            if (inputs.x <= -1) ApplyMove(0, 0);
            if (inputs.x >= 1) ApplyMove(0, 1);
            if (inputs.y <= -1) ApplyMove(0, 2);
            if (inputs.y >= 1) ApplyMove(0, 3);
        }*/
        
    }

    /*void ApplyMove(int type, int dir)
    {
        if(type == 0)
        {
            transform.GetComponent<Animator>().runtimeAnimatorController = animatorsEclair[dir];
        }
        if(type == 1)
        {
            transform.GetComponent<Animator>().runtimeAnimatorController = animatorsChoux[dir];
        }
        
    }*/

    void ApplyMovement()
    {
        if (lookScript!=null) MovementRelativeToCamera();
        character.Move(movement * Time.deltaTime);
    }

    void MovementRelativeToCamera()
    {
        movementRelativeToCam = lookScript.HorizontalPivot.right * movement.x; // Horizontal (left-right) movement relative to camera
        movementRelativeToCam += lookScript.HorizontalPivot.forward * movement.z; // Longitudinal (forward-backward) movement relative to camera
        movementRelativeToCam += transform.up * movement.y; // Vertical movement relative to character
        movement = movementRelativeToCam;
    }


} // SCRIPT END
