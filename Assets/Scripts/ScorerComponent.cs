using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScorerComponent : MonoBehaviour
{
    public int TeamID;
    public int ZoneID;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<ScorableZoneComponent>())
        {
            ZoneID = other.GetComponent<ScorableZoneComponent>().ZoneID;
        }
    }
    private void OnDestroy()
    {
        var teams = TeamPointSystem.Instantce.Teams;
        for (int i = 0; i < teams.Count; i++)
        {
            if (teams[i].ID == TeamID)
            {
                teams[i].Members.Remove(this);
            }
        }
        var allzones = FindObjectsOfType<ScorableZoneComponent>();
        foreach (var zone in allzones)
        {
            if (zone.ZoneID == this.ZoneID)
            {
                var triggerTeamID = zone.TriggerTeamID;
                for (int i = 0; i < triggerTeamID.Count; i++)
                {
                    if (triggerTeamID[i] == this.TeamID)
                    {
                        triggerTeamID.Remove(triggerTeamID[i]);
                        break;
                    }
                }
            }
        }
    }
}