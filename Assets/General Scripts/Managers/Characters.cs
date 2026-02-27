using UnityEngine.UI;
using UnityEngine;
public class Characters : SingletonMaster<Characters>
{
    public bool[] unlockedCharacters;
    public GameObject[] characterPrefabs;
    public Button[] characterButtons;

    public void UnlockCharacter(int index)
    {
        if (unlockedCharacters[index] == false)
        {
            unlockedCharacters[index] = true;
            if (characterButtons[index].interactable == false)
            {
                characterButtons[index].interactable = true;
            }
        }
    }
}
