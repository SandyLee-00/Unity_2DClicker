using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform Player { get; private set; }
    public ObjectPool ObjectPool { get; private set; }
    [SerializeField] private string playerTag = "Player";

    [Header("CoinController")]
    [SerializeField] private CoinController coin;
    public CoinController Coin => coin;

    [Header("Stage")]
    [SerializeField] private Stage stage;
    public Stage Stage => stage;

    private void Awake()
    {
        Instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;

        ObjectPool = GetComponent<ObjectPool>();

        coin = GetComponent<CoinController>();
        stage = GetComponent<Stage>();
    }
}