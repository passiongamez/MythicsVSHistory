using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeManager : SingletonMaster<GameModeManager>
{
    [SerializeField] ModeConfig[] _allGameModes;
    ModeConfig _gameMode;

    public void LoadGame(GameModeType selectedMode)
    {
        _gameMode = _allGameModes.FirstOrDefault(g => g.gameMode == selectedMode);

        if(_gameMode == null)
        {
            Debug.LogError($"No config found for mode {selectedMode}");
            return;
        }

        SceneManager.LoadSceneAsync(_gameMode.sceneName);
    }
}
