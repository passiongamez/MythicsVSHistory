using UnityEngine;

public class TeamManager : SingletonMaster<TeamManager>
{
    int _nextTeamId = 1;
    string _teamTag;

    public override void Initialize()
    {
        if(Instance != null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string AssignTeam(GameObject character, int teamId)
    {
        if(teamId == -1) teamId = _nextTeamId++;
        
        _teamTag = "Team" + teamId;
        character.tag = _teamTag;

        Debug.Log(character.name + " is assignted to " + _teamTag);

        return _teamTag;
    }

    public bool IsFriendly(GameObject a, GameObject b)
    {
        return a.tag == b.tag && a.tag.StartsWith("Team");
    }

    public bool IsEnemy(GameObject a, GameObject b)
    {
        return a.tag != b.tag && a.tag.StartsWith("Team") && b.tag.StartsWith("Team");
    }

    public void ResetTeams()
    {
        _nextTeamId = 1;
    }
}
