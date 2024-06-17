using UnityEngine;

[CreateAssetMenu(fileName = "FSM_Player", menuName = "Characters/FSM_Player")]
public class ChasingSO : ScriptableObject
{
    [field: SerializeField] public float ChasingRange { get; private set; } = 10f;
    [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public int Damage;
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
}