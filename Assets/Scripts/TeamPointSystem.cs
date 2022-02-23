using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class TeamPointSystem : Singleton<TeamPointSystem>
{

    public int minNumOfTeam;
    // Need a way to get all the team members when this thing is added to the scene
    // Need a way for team members to register DYNAMICALLY as THEY are added to the scene
    public List<Team> teams = new List<Team>();  //The class "Team " comes from the Script"Team.cs"
    public List<ScorableZoneComponent> zones = new List<ScorableZoneComponent>();
    
    public override void Awake()
    {
        base.Awake();
        var scorerComponents = FindObjectsOfType<ScorerComponent>();//Is "scorerComponent" an array or something?

        foreach (var scorerComponent in scorerComponents)
        {
            // Without LINQ...
            //var matchingTeamIndex = -1;

            //each GameObject with scorerComponent have an int "TeamID", check if it matches with any of that in the List "Team"
            //for (var i = 0; i < teams.Count; i++)
            //{
            //    if (teams[i].ID.Equals(scorerComponent.TeamID))
            //    {
            //        matchingTeamIndex = i;
            //    }
            //}
            //// if this GameObject's TeamID matches with None...Make a new instance of Team for it
            //if (matchingTeamIndex < 0)
            //{
            //    var team = new Team()
            //    {
            //        ID = scorerComponent.TeamID,
            //        Members = new List<ScorerComponent> { scorerComponent }
            //    };
            //    //  and Add the new instance of Team to the List
            //    teams.Add(team);
            //}
            //else
            //{//if the team that has already matches doesn't contain component<scorerComponent>,add one to it
            //    if (teams[matchingTeamIndex].Members.Contains(scorerComponent)) return;

            //    teams[matchingTeamIndex].Members.Add(scorerComponent);
            //}

            //With LINQ...
            if (!teams.Any(team => team.ID.Equals(scorerComponent.TeamID)))
            {
                var team = new Team()
                {
                    ID = scorerComponent.TeamID,
                    Members = new List<ScorerComponent> { scorerComponent }
                };

                teams.Add(team);
            }
            else
            {
                var team = teams.FirstOrDefault(team => team.ID.Equals(scorerComponent.TeamID));

                if (team.Members.Contains(scorerComponent)) return;

                team.Members.Add(scorerComponent);
            }
        }

    }

    private void Update()
    {

    }


}