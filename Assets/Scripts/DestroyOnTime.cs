using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour {

    public float timeToDestroy = 5f;
	void Start () {
        Destroy(this.gameObject, timeToDestroy);
	}
	
	void Update () {
		
	}
}
