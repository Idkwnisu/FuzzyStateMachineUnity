using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public float health;
    public float currentHealth;
    public float damage;
    float damageDelay = 1; //in seconds
    Renderer ren;
    ParticleSystem system;

	// Use this for initialization
	void Start () {
        currentHealth = health;
        ren = GetComponent<Renderer>();
        ren.material.SetColor("_Color", new Color(1, 1, 1));
        system = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            currentHealth = currentHealth - damage;
            ren.material.SetColor("_Color", new Color(1, currentHealth / health, currentHealth / health));
            system.Play();

            if(currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
