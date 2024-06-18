using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void FixedUpdate();
}

public class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}

public class PlayerStateMachine : StateMachine
{
    public FSM_Player Player { get; private set; }
    public GameObject Target { get; private set; }
    public Vector3 MovementDirection { get; set; }

    public PlayerIdleState IdleState { get; }
    public PlayerChasingState ChasingState { get; }
    public PlayerAttackingState AttackingState { get; }

    public PlayerStateMachine(FSM_Player fsmPlayer)
    {
        this.Player = fsmPlayer;

        IdleState = new PlayerIdleState(this);
        ChasingState = new PlayerChasingState(this);
        AttackingState = new PlayerAttackingState(this);

        Target = FindTarget();
    }

    public GameObject FindTarget()
    {
        Target = GameObject.FindGameObjectWithTag(Define.ENEMY_TAG);
        if(Target == null)
        {
            Debug.Log($"PlayerStateMachine::FindTarget() Target is null");
            return null;
        }

        Debug.Log($"Target : {Target.name}");
        return Target;
    }
}