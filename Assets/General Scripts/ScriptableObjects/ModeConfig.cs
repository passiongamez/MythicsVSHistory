using UnityEngine;

[CreateAssetMenu(fileName = "ModeConfig", menuName = "Game/Mode Config")]
public class ModeConfig : ScriptableObject
{
    public GameModeType gameMode;
    public string sceneName;
    public int teamSize;
}
