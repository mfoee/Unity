﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float speed;
    public GameObject shot;
    public Transform shotSpawn;

    public float fireRate;
    private float nextFire;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);
	}

    void Update() {
        
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire) {
            //Debug.Log("fire");
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }
}
