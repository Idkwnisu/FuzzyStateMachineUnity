using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    Animation anim;
    BoxCollider collider;
    public GameObject bulletPrefab;
    public float projectileSpeed;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();
        collider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {

         
            float x = hit.point.x - transform.position.x;
            float z = hit.point.z - transform.position.z;
            Vector2 speed = new Vector2(x, z).normalized;
            speed = speed * projectileSpeed;
            
            var bullet = (GameObject)Instantiate(
             bulletPrefab,
             transform.position,
             transform.rotation);

            bullet.GetComponent<Rigidbody>().velocity = new Vector3(speed.x, 0, speed.y);

            Destroy(bullet, 5.0f);
            }
        }
       
    }
}
