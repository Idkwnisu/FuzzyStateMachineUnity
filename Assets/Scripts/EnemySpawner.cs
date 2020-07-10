using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public int maxEnemies;
    public GameObject container;
    public GameObject enemyPrefab;

    public GameObject player;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        int children = container.transform.childCount;
        Vector3 position = new Vector3();
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);
        position.x = x;
        position.z = z;
        position.y = 20.0f;
        if (children < maxEnemies)
        {
            var enemy = (GameObject)Instantiate(
             enemyPrefab,
             position,
             transform.rotation);
            enemy.transform.parent = container.transform;
            FuzzyMachine fm = enemy.GetComponent<FuzzyMachine>();
            Defuzzyficator defuz = enemy.GetComponent<Defuzzyficator>();
            fm.player = player;
            fm.friends = container;

            float Homicidal = Random.Range(0.5f, 2);
            float Fearful = Random.Range(0.5f, 2);
            float Social = Random.Range(0.5f, 2);
            float LoneWolf = Random.Range(0.5f, 2);
            float Aggressiveness = Random.Range(0.5f, 1.5f);

            defuz.homicidal = Homicidal;
            defuz.fearful = Fearful;
            defuz.social = Social;
            defuz.loneWolf = LoneWolf;
            defuz.aggressiveness = Aggressiveness;
        }
       
    }
}
