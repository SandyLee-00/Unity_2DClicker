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
