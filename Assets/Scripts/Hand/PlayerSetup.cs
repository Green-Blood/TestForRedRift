using Hand;
using UnityEngine;

public sealed class PlayerSetup : MonoBehaviour
{
    [SerializeField] private PlayerHand[] playerHands;

    public PlayerHand[] PlayerHands => playerHands;
}
