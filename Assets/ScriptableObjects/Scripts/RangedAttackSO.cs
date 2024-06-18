using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "TopDownController/Attacks/Ranged", order = 1)]
public class RangedAttackSO : AttackSO
{
    // 원거리 공격에만 있는 옵션 정의
    [Header("Ranged Attack Data")]
    public string bulletNameTag;
    public float duration;
    public float spread;
    public int numberofProjectilesPerShot;
    public float multipleProjectilesAngel;
    public Color projectileColor;
}

[SerializeField]
public class RangedData
{
    [Header("Ranged Attack Data")]
    public string bulletNameTag = "Arrow";
    public float duration = 5;
    public float spread = 4;
    public int numberofProjectilesPerShot = 1;
    public float multipleProjectilesAngel = 7;
    public Color projectileColor = Color.white;

    [Header("Attack Info")]
    public float size =1;
    public float delay = 0.15f;
    public float power = 2;
    public float speed = 10;
    public LayerMask target = LayerMask.NameToLayer("Enemy");

    [Header("Knock Back Info")]
    public bool isOnKnockback;
    public float knockbackPower;
    public float knockbackTime;
}
