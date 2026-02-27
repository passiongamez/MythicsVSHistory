using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeManager : SingletonMaster<GameModeManager>
{
    [SerializeField] ModeConfig[] _allGameModes;
    public ModeConfig gameMode;

    public void LoadGame(GameModeType selectedMode)
    {
        gameMode = _allGameModes.FirstOrDefault(g => g.gameMode == selectedMode);

        if(gameMode == null)
        {
            Debug.LogError($"No config found for mode {selectedMode}");
            return;
        }

        SceneManager.LoadSceneAsync(gameMode.sceneName);
    }
}
