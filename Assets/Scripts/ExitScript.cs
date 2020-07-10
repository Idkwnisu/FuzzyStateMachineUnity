using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {
    bool active = false;
    public Material activeMaterial;
    Renderer rend;
    Collider collider;

    public ParticleSystem ps;
    
    // Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activateExit()
    {
        active = true;
        rend.enabled = false;
        collider.isTrigger = true;
        ps.Play();
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision");
        if (active)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
