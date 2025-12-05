using UnityEngine;

public class PlayerStatusService : PersistantSingleton<PlayerStatusService>
{
    public PlayerStatus PlayerStatus { get; private set; }
}
