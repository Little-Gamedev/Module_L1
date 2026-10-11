using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Progress
{
    [CreateAssetMenu(fileName = "ProgressResetConfig", menuName = "Configs/Meta/Progress/ProgressResetConfig")]
    public class ProgressResetConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public int ResetPrice { get; private set; } = 50;
    }
}