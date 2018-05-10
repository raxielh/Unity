using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    private float lifeTime = 2f;
    public float speed = 5.0f;
    public int damage = 1;


	// Use this for initialization
	void Start () {
        Destroy(gameObject,lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.up*speed*Time.deltaTime);
	}
}
