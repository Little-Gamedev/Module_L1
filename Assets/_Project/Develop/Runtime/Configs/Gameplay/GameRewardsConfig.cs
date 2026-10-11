using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
    [CreateAssetMenu(fileName = "GameRewardsConfig", menuName = "Configs/Gameplay/GameRewardsConfig")]
    public class GameRewardsConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public int WinReward { get; private set; } = 20;
        [field: SerializeField, Min(0)] public int LossPenalty { get; private set; } = 10;
    }
}