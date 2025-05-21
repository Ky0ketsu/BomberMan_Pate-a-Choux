using UnityEngine;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Range(0,3)][SerializeField] public int playerID = 0; // Rewired plugin
    public bool CanRun{get{return canRun;}set{canRun=value;}}
    bool canRun=false;
    public bool CanFall{get{return canFall;}set{canFall=value;}}
    bool canFall=false;
    [Range(0,100f)][SerializeField] float maxSpeed = 5f;
    Vector2 inputs;
    Vector3 movement, movementRelativeToCam;
    CharacterController character => GetComponent<CharacterController>();
    PlayerLook lookScript => GetComponent<PlayerLook>();
    Player player; // Rewired plugin
    [HideInInspector] public int speedUp = 0;

    [SerializeField] private GameObject[] animationSprite = new GameObject[4];
    [HideInInspector] public bool chouxBouleToutActive;
    [SerializeField] private GameObject colliderInternal;

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
        player = ReInput.players.GetPlayer(playerID);
        if (GAME.MANAGER.CurrentState==State.gameplay) EnableMoveSet();

        for (int i = 0; i < animationSprite.Length; i++)
        {
            if(i != playerID) transform.Find("GRAPHICS").Find("PlayerAnimation").GetChild(i).gameObject.SetActive(false);
            else transform.Find("GRAPHICS").Find("PlayerAnimation").GetChild(i).gameObject.SetActive(true);

            animationSprite[i] = transform.Find("GRAPHICS").Find("PlayerAnimation").GetChild(playerID).GetChild(i).gameObject;
        }

        gameObject.layer = 24 + playerID;
        colliderInternal.layer = 24 + playerID;

       
    }

    void EnableMoveSet()
    {
        CanRun = CanFall = true;
    }

    void DisableMoveSet()
    {
        CanRun = CanFall = false;
    }



    void Update()
    {
        if (!canRun) return;
        HorizontalMovement();
        VerticalMovement();
        ApplyMovement();
        CheckAnimation();
    }

    void GetInputs()
    {
        inputs.x = player.GetAxis("MoveHorizontal");
        inputs.y = player.GetAxis("MoveVertical");
        if (inputs.sqrMagnitude>1f) inputs.Normalize(); // avoir diagonals bigger than 1 (pythagoras)
    }

    private GameObject currentAnim;

    void CheckAnimation()
    {
        float currentDirX = inputs.x;
        float currentDirY = inputs.y;
        

        if(currentDirX < 0) currentDirX = -currentDirX;  
        if(currentDirY < 0) currentDirY = -currentDirY;

        if (currentDirX < currentDirY)
        {
            if (inputs.y < 0) ApplyAnimation(3);
            else ApplyAnimation(1);
        }
        else if (currentDirY < currentDirX)
        {
            if (inputs.x < 0) ApplyAnimation(2);
            else ApplyAnimation(0);
        }
        else ApplyAnimation(3);


    }

    [HideInInspector] public Vector3[] dir = new Vector3[4];

    private void ApplyAnimation(int animationID)
    {
        for (int i = 0; i < animationSprite.Length; i++)
        {
            if(i != animationID)
            {
                animationSprite[i].SetActive(false);
            }
            else
            {
                animationSprite[i].SetActive(true);
            }
        }
    }

    private  void SetCurrentDir(int currentDir)
    {

    }

    private float slowValue = 1;
    public void SetSpeed(bool slow)
    {
        if (slow == true) slowValue = 0.6f;
        else slowValue = 1;
    }

    void VerticalMovement()
    {
        //movement.y= CanFall ? -60f : 0; // very simple fall with constant speed
    }

    [SerializeField] private GameObject trail;

    void HorizontalMovement()
    {
        GetInputs();
        movement.x = CanRun ? inputs.x * ((maxSpeed + (1 * speedUp)) * slowValue) : 0;
        movement.z = CanRun ? inputs.y * ((maxSpeed + (1 * speedUp)) * slowValue) : 0;

        if (movement.x != 0 || movement.z != 0)
        {
            trail.GetComponent<ParticleSystem>().enableEmission = true;
            trail.transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = true;
        }
        else
        {
            trail.GetComponent<ParticleSystem>().enableEmission = false;
            trail.transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = false;
        }
    }

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
