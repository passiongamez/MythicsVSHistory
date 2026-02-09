using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeLoadManager : SingletonMaster<GameModeLoadManager>
{
    static int _gameModeIndex;

    public void LoadGameMode(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }
}
