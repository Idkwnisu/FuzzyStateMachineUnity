using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuzzyficator : MonoBehaviour {

    private void Start()
    {
        

    }

    public Dictionary<string,FuzzyVariable> getUpdatedFuzzyVariables(FuzzyMachine fm)
    {
        Dictionary<string, FuzzyVariable> dictionary = new Dictionary<string, FuzzyVariable>();

        float NearPlayer = Mathf.Clamp(1/(Vector3.Distance(fm.player.transform.position, gameObject.transform.position)+1), 0, 1);

        PlayerController movement = fm.player.GetComponent<PlayerController>();
        float Nuggets = movement.getCollectibles() / movement.maxCollectibles;

        EnemyHealth HealthScript = gameObject.GetComponent<EnemyHealth>();
        float health = HealthScript.currentHealth / HealthScript.health;

        float pHealth = movement.getHealth() / movement.maxHealth;

        float NearFriends = Mathf.Clamp(1/(Vector3.Distance(fm.getAveragePosition(), gameObject.transform.position))+1, 0, 1);
        float Crowded = Mathf.Clamp(1 / ((float)fm.getAverageDistance() + 1), 0, 1);

        dictionary.Add("NearPlayer", new FuzzyVariable(NearPlayer));
        dictionary.Add("Nuggets", new FuzzyVariable(Nuggets));
        dictionary.Add("NearFriends", new FuzzyVariable(1-NearFriends));
        dictionary.Add("LowHealth", new FuzzyVariable(1-health));
        dictionary.Add("PlayerLowHealth", new FuzzyVariable(1-pHealth));
        dictionary.Add("FriendsAttacking", new FuzzyVariable(fm.getAverageAttack()));
        dictionary.Add("Crowded", new FuzzyVariable(Crowded));

        
        return dictionary;
    }
}
