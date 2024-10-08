using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Player/Data", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Player")]
    public float initialJumpForce;
    public LayerMask groundLayerMask;
    public KeyCode jumpKey = KeyCode.Space;
}
