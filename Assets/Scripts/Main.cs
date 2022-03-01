using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public enum LevelType
    {
        Creation,
        Playing
    }
    [SerializeField] private LevelType _levelType;
    public static LevelType CurrentLevelType;

    private void Awake()
    {
        CurrentLevelType = _levelType;
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }
}
