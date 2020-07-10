using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    Rigidbody rb;
    public float speed;
    public float speedDamp;
    public float maxHealth;
    private float health;
    public float damageTaken;
    public Text healthText;
    public Text collectibleText;
    public int maxCollectibles;
    public GameObject exit;
    private int collectibles;


	// Use this for initialization
	void Start () {
        rb = GetComponentInChildren<Rigidbody>();
        health = maxHealth;
        healthText.text = "Health: " + health;
        collectibles = 0;
    }
    
    public float getHealth()
    {
        return health;
    }

    public int getCollectibles()
    {
        return collectibles;
    }

    // Update is called once per frame
    void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement.Normalize();
        /*
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            float x = hit.point.x - transform.position.x;
            float z = hit.point.z - transform.position.z;
            if (Mathf.Abs(x) > Mathf.Abs(z))
            {
                if (x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, -90, 0);
                }
            }
            else if (Mathf.Abs(z) > Mathf.Abs(x))
            {
                if (z > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }

            }
        }

    */
        
        
        rb.AddForce(movement * speed * Time.deltaTime);

        Vector3 spd = rb.velocity;

        if (Mathf.Abs(moveHorizontal) < 0.99f)
        {
            spd.x = spd.x * speedDamp;
        }
        if (Mathf.Abs(moveVertical) < 0.99f)
        {
            spd.y = spd.y * speedDamp;
        }

        rb.velocity = spd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fists"))
        {
            health -= damageTaken;
            healthText.text = "Health: " + health;
        }
        else if(other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            collectibles += 1;
            collectibleText.text = "Nuggets: " + collectibles + "/"+maxCollectibles;
            if(collectibles == maxCollectibles)
            {
                collectibleText.text = "Go to Exit to restart";
                exit.GetComponent<ExitScript>().activateExit();
            }
        }
    }
}
