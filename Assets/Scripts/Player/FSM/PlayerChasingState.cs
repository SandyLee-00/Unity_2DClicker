using System;
using UnityEngine;

public class PlayerChasingState : PlayerBaseState
{
    public PlayerChasingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    public override void Exit()
    {
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
    }

    private void UpdateMove()
    {
        Vector3 movementDirection = GetMovementDirection();

        stateMachine.Player.transform.position += movementDirection * stateMachine.Player.Data.BaseSpeed * Time.deltaTime;
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Player.Data.AttackRange * stateMachine.Player.Data.AttackRange;
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).normalized;
        return dir;
    }
}