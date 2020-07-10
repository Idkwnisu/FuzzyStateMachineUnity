using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyMachine : MonoBehaviour {


    public float secondsUntilUpdate;

    public GameObject player;
    public GameObject friends;
    EnemyMovement script;
    Rigidbody rb;

    Fuzzyficator fuzzyficator;
    Defuzzyficator defuzzyficator;
    FuzzyVariable attack;

    private IEnumerator corMoving;
    WaitForSeconds waitingTime;

    Vector3 averagePosition;
    double averageAttack;
    double averageDistance;


    // Use this for initialization
    void Start () {
        script = GetComponent<EnemyMovement>();
        rb = GetComponent<Rigidbody>();
        waitingTime = new WaitForSeconds(secondsUntilUpdate);

        corMoving = updatePosition();

        fuzzyficator = GetComponent<Fuzzyficator>();

        defuzzyficator = GetComponent<Defuzzyficator>();

        attack = new FuzzyVariable(0.0);


        StartCoroutine(corMoving);

    }

    public Vector3 getAveragePosition()
    {
        return averagePosition;
    }

    public double getAverageAttack()
    {
        return averageAttack;
    }

    public FuzzyVariable attackingValue()
    {
        return attack;
    }

    public double getAverageDistance()
    {
        return averageDistance;
    }
    // Update is called once per frame
    void Update () {
       
        
    }

    IEnumerator updatePosition()
    {
        while(true)
        {
            yield return waitingTime;

            Vector3 pDest = player.transform.position - rb.transform.position;
            pDest.Normalize();
            averagePosition = new Vector3(0, 0, 0);
            int count = 0;
            double attackCount = 0.0f;
            bool existFriends = false;
            int children = transform.childCount;
            for (int i = 0; i < children; ++i)
            {
                if (!GameObject.ReferenceEquals(friends.transform.GetChild(i).gameObject, gameObject))
                {
                    existFriends = true;
                    averagePosition = averagePosition + friends.transform.GetChild(i).position;
                    FuzzyVariable attackingValue = friends.transform.GetChild(i).gameObject.GetComponent<FuzzyMachine>().attackingValue();
                    attackCount += attackingValue.value;
                    count += 1;
                }                
            }


            averagePosition = averagePosition / count;
            averageAttack = attackCount / count;

            double distanceCount = 0.0f;
            for (int i = 0; i < children; ++i)
            {
                float distance = Vector3.Distance(friends.transform.GetChild(i).position, averagePosition);
                distanceCount += distance;
            }
            averageDistance = distanceCount / count;
          //  Debug.Log(distanceCount);


            Vector3 fDest = averagePosition - rb.transform.position;
            fDest.Normalize();
            if (!existFriends)
                fDest = new Vector3(0, 0, 0);
            Dictionary<string, FuzzyVariable> fuzzyInput = fuzzyficator.getUpdatedFuzzyVariables(this);
            FuzzyVariable fear = fuzzyInput["NearPlayer"] * fuzzyInput["LowHealth"];
            attack = fuzzyInput["FriendsAttacking"].Inverted() + fuzzyInput["Nuggets"] + fuzzyInput["PlayerLowHealth"];
            FuzzyVariable join =  fuzzyInput["LowHealth"];
            FuzzyVariable separate = (fuzzyInput["NearFriends"] * fuzzyInput["FriendsAttacking"].Inverted()) + fuzzyInput["Crowded"];

            //if nearplayer and lowhealth = run
            //if nuggets or playerlowhealth = attack
            //if nearfriends and not friendsattacking = separate
            //if lowhealth = join
            Dictionary<string, FuzzyVariable> fuzzyStatus = new Dictionary<string, FuzzyVariable>();
            fuzzyStatus.Add("Fear", fear);
            fuzzyStatus.Add("Attack", attack);
            fuzzyStatus.Add("Join", join);
            fuzzyStatus.Add("Separate", separate);
            Vector3 newDest = defuzzyficator.Defuzzify(fuzzyStatus, pDest, fDest);
            script.direction = newDest;

            script.updatePosition();
        }
    }
}
