using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
	}
	
}
