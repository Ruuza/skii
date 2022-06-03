using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderFollow : MonoBehaviour {

    public Transform player;

	void Start () {
		
	}
	
	void Update () {
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
	}
}
