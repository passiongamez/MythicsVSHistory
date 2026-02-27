using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> _team1Characters;
    [SerializeField] List<GameObject> _team2Characters;
    [SerializeField] List<GameObject> _team3Characters;
    [SerializeField] List<GameObject> _team4Characters;
    [SerializeField] List<GameObject> _team5Characters;
    [SerializeField] List<GameObject> _team6Characters;
    [SerializeField] List<GameObject> _team7Characters;
    [SerializeField] List<GameObject> _team8Characters;
    [SerializeField] List<GameObject> _team9Characters;
    [SerializeField] List<GameObject> _team10Characters;


    [SerializeField] List<Transform> _spawnPoints;

    int _teamSize;
    int _teamID = 1;


    public void AddCharactersToTeams(GameObject character)
    {
        _team1Characters.Add(character);

        if(_team1Characters.Count == GameModeManager.Instance.gameMode.teamSize)
        {

        }
        TeamManager.Instance.AssignTeam(character);
    }
}
