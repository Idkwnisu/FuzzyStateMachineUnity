using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    public GameObject target;
    public Camera toMove;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        toMove.transform.position = new Vector3(target.transform.position.x, toMove.transform.position.y, target.transform.position.z - 5.0f);
	}
}
