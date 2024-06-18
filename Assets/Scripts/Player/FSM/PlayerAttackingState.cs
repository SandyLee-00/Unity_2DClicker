using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttackingState : PlayerBaseState
{
    public PlayerAttackingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("PlayerAttackingState::Enter()");
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Player.AnimationData.BaseAttackParameterHash);

        stateMachine.Player.StartCoroutine(ShootArrowRoutine());
    }

    public override void Exit()
    {
        Debug.Log("PlayerAttackingState::Exit()");
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.BaseAttackParameterHash);

        stateMachine.Player.StopCoroutine(ShootArrowRoutine());
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        Debug.Log($"normalizedTime : {normalizedTime}");

        // 공격 애니메이션 중
        if (normalizedTime < 1f)
        {
        }

        // 공격 애니메이션 끝
        else
        {
            // 추적 가능한 범위에 있으면 -> 추적 상태로 전환
            if (IsInChasingRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            // 추적 범위 밖에 있으면 -> 대기 상태로 전환
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
    }

    private void CreateProjectile()
    {
        // 오브젝트 풀을 활용한 생성으로 변경
        GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool("Arrow");

        // 발사체 기본 세팅
        obj.transform.position = stateMachine.Player.transform.position + Vector3.up * 0.5f;
        ProjectileController attackController = obj.GetComponent<ProjectileController>();
        attackController.InitializeAttack(RotateVector2(stateMachine.MovementDirection, 0));

        obj.SetActive(true);
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }

    // 1초에 한번 씩 코루틴으로 화살 생성
    private IEnumerator ShootArrowRoutine()
    {
        while (true)
        {
            CreateProjectile();
            yield return new WaitForSeconds(1f);
        }
    }

}