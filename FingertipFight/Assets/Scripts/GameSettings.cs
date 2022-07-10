using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings/GameSetting")]
public class GameSettings : ScriptableObject
{
    [Header("Game Settings")]
    [SerializeField]
    private float _gameSuccessPoint = 100;

    [Space]
    [Header("Collectible Settings")]
    [SerializeField]
    private int _collectableSize = 10;
    [SerializeField]
    private float _collectableSpawnRate = 1f;


    [Space]
    [Header("Player Settings")]
    [SerializeField]
    private float _playerMovementSpeed = 1f;
    [SerializeField]
    private float _playerRotationSpeed = 1f;

    public float GameSuccessPoint { get => _gameSuccessPoint; }
    public float CollectableSpawnRate { get => _collectableSpawnRate; }
    public float PlayerMovementSpeed { get => _playerMovementSpeed; }
    public float PlayerRotationSpeed { get => _playerRotationSpeed; }
    public int CollectableSize { get => _collectableSize; }
}
