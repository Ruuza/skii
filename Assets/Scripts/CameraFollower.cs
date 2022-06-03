using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    public Transform player;

    private float offsetY;


	void Start () {
        offsetY = transform.position.y - player.position.y;

	}
	
	void LateUpdate () {
        transform.position =  new Vector3(transform.position.x, player.position.y + offsetY, transform.position.z);
	}
}
