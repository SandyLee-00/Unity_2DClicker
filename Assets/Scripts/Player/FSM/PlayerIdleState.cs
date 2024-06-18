using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("PlayerIdleState::Enter()");

        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        Debug.Log("PlayerIdleState::Exit()");

        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsInChasingRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
    }
}

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void Update()
    {
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    /// <summary>
    /// 애니메이터의 상태 정보를 가져와서, 주어진 태그에 해당하는 상태의 진행 시간을 반환합니다.
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        // 현재 애니메이터의 상태 정보를 가져옵니다.
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        // 애니메이터가 전환 중인지 확인하고, 전환 중이라면 다음 상태의 정보를 확인합니다.
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        // 애니메이터가 전환 중이 아니고, 현재 상태가 주어진 태그와 일치하는지 확인합니다.
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        // 위의 조건에 모두 해당되지 않으면, 진행 시간을 0으로 반환합니다.
        else
        {
            return 0f;
        }
    }

    protected bool IsInChasingRange()
    {
        if(stateMachine.Target == null)
        {
            Debug.Log($"PlayerBaseState::IsInChasingRange() : Target is null");

            stateMachine.FindTarget();
            if(stateMachine.Target == null)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }

            return false;
        }

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Player.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Player.Data.PlayerChasingRange * stateMachine.Player.Data.PlayerChasingRange;
    }
}