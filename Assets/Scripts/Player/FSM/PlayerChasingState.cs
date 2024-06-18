using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerChasingState : PlayerBaseState
{
    public PlayerChasingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("PlayerChasingState::Enter()");
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    public override void Exit()
    {
        Debug.Log("PlayerChasingState::Exit()");
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!IsInChasingRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackingState);
            return;
        }

        UpdateMove();
        UpdateRotataion();
    }

    private void UpdateMove()
    {
        stateMachine.MovementDirection = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).normalized;
        stateMachine.Player.transform.position += stateMachine.MovementDirection * stateMachine.Player.Data.BaseSpeed * Time.deltaTime;
    }

    private void UpdateRotataion()
    {
        stateMachine.Player.AimRotation.RotateSprite(stateMachine.MovementDirection);
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Player.Data.AttackRange * stateMachine.Player.Data.AttackRange;
    }
}
