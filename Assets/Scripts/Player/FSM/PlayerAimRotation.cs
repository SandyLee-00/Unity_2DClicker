using UnityEngine;

public class PlayerAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;

    [SerializeField] private SpriteRenderer characterRenderer;

    public void RotateSprite(Vector2 direction)
    {
        // Atan2는 직각삼각형이 있다고 할 때 세로가 y, 가로가 x일 때 그 각도를 라디안 [-Pi,Pi]로 나타내는 함수임
        // 라디안의 -Pi는 -180도, Pi는 180도 이므로 Mathf.Rad2Deg는 약 57.29임 (180 / 3.14)
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // [1. 캐릭터 뒤집기]
        // 이때 각도는 오른쪽(1,0 방향)이 0도이므로,
        // -90~90도에서는 오른쪽을 바라보는 게 맞지만, -90도 미만 90도 초과라면 왼쪽을 바라보는 것임.
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        // 플레이어 무기는 상하대칭이라 괜찮았지만 샤먼 무기는 상하대칭이 아니라 뒤집어줘야함.
        armRenderer.flipY = characterRenderer.flipX;

        // [2. 팔 돌리기]
        // 팔을 돌릴 때는 나온 각도를 그대로 적용하는데, 이때 유니티 내부에서 사용하는 쿼터니언으로 변환한다.
        // 쿼터니언으로 변형하는 방법 두 가지
        // 1) Vector3를 Quaternion으로 변환해서 넣는 방법
        //    Quaternion.Euler(x 회전, y 회전, z 회전) : 오일러 각 기준으로 값을 넣으면 쿼터니언으로 변환됨
        // 2) 프로퍼티를 통해 자동으로 변환되게 하는 방법
        //    Transform.eulerAngles을 변경
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        // (2번 방법으로 하면) armPivot.eulerAngles = new Vector3(0, 0, rotZ);
    }
}