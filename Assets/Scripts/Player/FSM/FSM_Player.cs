using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Player : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    [field: SerializeField] public PlayerData Data { get; private set; }

    public Animator Animator { get; private set; }
    public BoxCollider2D BoxCollider2D { get; private set; }
    public PlayerAimRotation AimRotation { get; private set; }

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        AnimationData = new PlayerAnimationData();
        AnimationData.Initialize();
        Data = new PlayerData();

        Animator = GetComponentInChildren<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        AimRotation = GetComponent<PlayerAimRotation>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}

[SerializeField]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string baseAttackParameterName = "BaseAttack";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int BaseAttackParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        BaseAttackParameterHash = Animator.StringToHash(baseAttackParameterName);
    }
}

[SerializeField]
public class PlayerData
{
    [SerializeField] public float PlayerChasingRange { get; private set; } = 10.0f;
    [SerializeField] public float BaseSpeed { get; private set; } = 5.0f;
    [SerializeField] public float AttackRange { get; private set; } = 3.0f;
}