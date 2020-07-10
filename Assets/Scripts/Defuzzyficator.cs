using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defuzzyficator : MonoBehaviour {

    public float homicidal; //how much killing the player is important
    public float fearful; //how much saving his life is important
    public float social; //how important is to stay united with the group
    public float loneWolf; //how the minion tends to stay on his own

    public float aggressiveness; //how much on a base level the minion tends to attack

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 Defuzzify(Dictionary<string, FuzzyVariable> fuzzyVars, Vector3 playerDirection, Vector3 friendsDirection)
    {
        Vector3 direction = new Vector3();
        direction = homicidal * playerDirection * (float)fuzzyVars["Attack"].getValue() + fearful * (-1) * playerDirection * (float)fuzzyVars["Fear"].getValue() + social * friendsDirection * (float)fuzzyVars["Join"].getValue() + loneWolf * friendsDirection*(-1) * (float)fuzzyVars["Separate"].getValue() + aggressiveness * playerDirection ;
        direction.Normalize();
        // Debug.Log("X: " + direction.x + " Y: " + direction.y + " Z: " + direction.z);
       // Debug.Log("Attack: "+fuzzyVars["Attack"].getValue());
       // Debug.Log("Fear: " + fuzzyVars["Fear"].getValue());
       // Debug.Log("Join: " + fuzzyVars["Join"].getValue());
        return direction;
    }
}
