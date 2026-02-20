using UnityEngine;

public class LoadPrototypeScene : MonoBehaviour
{
    public void LoadScene1v1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("1v1CombatTest");
    }

    public void LoadScene3v3()
    {
               UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("3v3CombatTest");
    }
}
