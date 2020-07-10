using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Vector3 direction;
    public float FOVAngle;
    public float speed;

    public float TotalSteer;
    public float BigSteer;
    public float SmallSteer;

    public float distanceObstacle;
    public float distancePlayer;

    public float secondsUntilAttack;

    Rigidbody rb;
    RaycastHit centerHit;
    RaycastHit leftHit;
    RaycastHit rightHit;
    bool center;
    bool right;
    bool left;

    bool isAttacking = false;
    bool nearPlayer = false;


    private IEnumerator corAttacking;
    WaitForSeconds waitAttack;

    Vector3 newDirection;

    Animation anim;
    Collider collider;


    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
        corAttacking = attackPlayer();
        waitAttack = new WaitForSeconds(secondsUntilAttack);
        anim = GetComponent<Animation>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {

        Vector3 newPos = rb.transform.position + newDirection * speed * Time.deltaTime;

        if (Physics.BoxCast(collider.bounds.center, transform.localScale / 2, direction, out centerHit, transform.rotation, distancePlayer))
        {
            if (centerHit.collider.gameObject.tag == "Player")
                {
                    nearPlayer = true;

                    if (!isAttacking)
                    {
                        StartCoroutine(corAttacking);
                        isAttacking = true;
                    }
                }
                else
                {
                    nearPlayer = false;
                    isAttacking = false;
                    StopCoroutine(corAttacking);
                }
            }
        else {
            nearPlayer = false;
            StopCoroutine(corAttacking);
            isAttacking = false;
        }
        
            float x = newPos.x - transform.position.x;
            float z = newPos.z - transform.position.z;

            float Angle = Mathf.Atan2(newDirection.x,newDirection.z);
        Angle = Angle / Mathf.PI * 180;
        transform.eulerAngles = new Vector3(0, Angle, 0);
        if (!nearPlayer)
        {
            rb.MovePosition(newPos);
        }
        }


    IEnumerator attackPlayer()
    {
        while (true)
        {

            yield return waitAttack;
            if (isAttacking)
                anim.Play();
        }

    }

    public void updatePosition()
{
            center = false;
            right = false;
            left = false;

            direction.y = 0;
            Vector3 rightPosition = new Vector3(direction.x * Mathf.Cos(FOVAngle) - direction.z * Mathf.Sin(FOVAngle), 0, direction.x * Mathf.Sin(FOVAngle) + direction.z * Mathf.Cos(FOVAngle));
            Vector3 leftPosition = new Vector3(direction.x * Mathf.Cos((-1) * FOVAngle) - direction.z * Mathf.Sin((-1) * FOVAngle), 0, direction.x * Mathf.Sin((-1) * FOVAngle) + direction.z * Mathf.Cos((-1) * FOVAngle));

        /*

            Debug.DrawRay(transform.position, direction);
            Debug.DrawRay(transform.position, rightPosition);
            Debug.DrawRay(transform.position, leftPosition);
            */
                if (Physics.Raycast(transform.position, direction, out centerHit, distanceObstacle))
                {
                    if (centerHit.collider.gameObject.tag != "Player" && centerHit.collider.gameObject.tag != "Enemy")
                        center = true;
                }
                if (Physics.Raycast(transform.position, rightPosition, out centerHit, distanceObstacle))
                {
                    if (centerHit.collider.gameObject.tag != "Player" && centerHit.collider.gameObject.tag != "Enemy")
                        right = true;
                }
                if (Physics.Raycast(transform.position, leftPosition, out centerHit, distanceObstacle))
                {
                    if (centerHit.collider.gameObject.tag != "Player" && centerHit.collider.gameObject.tag != "Enemy")
                        left = true;
                }
                if (center && right && left)
                {
                    newDirection = new Vector3(direction.x * Mathf.Cos(TotalSteer) - direction.z * Mathf.Sin(TotalSteer), 0, direction.x * Mathf.Sin(TotalSteer) + direction.z * Mathf.Cos(TotalSteer));
                }
                else if (center && right)
                {
                    newDirection = new Vector3(direction.x * Mathf.Cos(BigSteer) - direction.z * Mathf.Sin(BigSteer), 0, direction.x * Mathf.Sin(BigSteer) + direction.z * Mathf.Cos(BigSteer));
                }
                else if (center && left)
                {
                    newDirection = new Vector3(direction.x * Mathf.Cos((-1) * BigSteer) - direction.z * Mathf.Sin((-1) * BigSteer), 0, direction.x * Mathf.Sin((-1) * BigSteer) + direction.z * Mathf.Cos((-1) * BigSteer));
                }
                else if (center)
                {
                    newDirection = new Vector3(direction.x * Mathf.Cos(SmallSteer) - direction.z * Mathf.Sin(SmallSteer), 0, direction.x * Mathf.Sin(SmallSteer) + direction.z * Mathf.Cos(SmallSteer));
                }
                else if (right)
                {
                    newDirection = new Vector3(direction.x * Mathf.Cos(SmallSteer) - direction.z * Mathf.Sin(SmallSteer), 0, direction.x * Mathf.Sin(SmallSteer) + direction.z * Mathf.Cos(SmallSteer));
                }
                else if (left)
                {
                    newDirection = new Vector3(direction.x * Mathf.Cos((-1) * SmallSteer) - direction.z * Mathf.Sin((-1) * SmallSteer), 0, direction.x * Mathf.Sin((-1) * SmallSteer) + direction.z * Mathf.Cos((-1) * SmallSteer));
                }
                else
                {
                    newDirection = direction;
                }
            
        /*

            if (center)
                Debug.Log("center");
            if (right)
                Debug.Log("right");
            if (left)
                Debug.Log("left");*/
  
}
}