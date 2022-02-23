using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorableZoneComponent : MonoBehaviour
{

    public List<int> TriggerTeamID = new List<int>();

    // Score changes...
    // Update our score
    public float TickPoints;
    // How often do we receive points?
    public float TickRate;
    // Changes...
    private float timer = 0;
    public int zoneID=0;
    
    private void Start()
    {
        var zones = TeamPointSystem.instantce.zones;  
        zones.Add(this);
        zoneID = zones.IndexOf(this);
        //Debug.Log(zoneID);
    }

    private void Score()
    {
        timer += Time.deltaTime;
        bool isSame = true;
        if (timer < TickRate) return;
        if (TriggerTeamID.Count != 0)
        {

            var setTeamID = TriggerTeamID[0];
            foreach (var triggerTeamID in TriggerTeamID)
            {
                if (triggerTeamID != setTeamID)
                {
                    isSame = false;
                }
            }
            var teams = TeamPointSystem.instantce.teams;
            if (isSame&&TriggerTeamID.Count>TeamPointSystem.instantce.minNumOfTeam)//if every ID in the list is the same,get the ID and make it score
            {
                for (int i = 0; i < teams.Count; i++)
                {
                    if (teams[i].ID == setTeamID)
                    {
                        teams[i].teamScore += TickPoints;
                        Debug.Log($"Team[{i}]'s CurrentScore is :" + teams[i].teamScore);
                    }
                }
            }

            timer = 0;
        }
    }
    private void Update()
    {
        Score();

    }
    // Start is called before the first frame update


    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<ScorerComponent>())
        {
            TriggerTeamID.Add(other.GetComponent<ScorerComponent>().TeamID);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ScorerComponent>())
        {
            TriggerTeamID.Remove(other.GetComponent<ScorerComponent>().TeamID);
        }
    }

}



