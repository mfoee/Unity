using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float speed;

    public Text countText;
    public Text winText;

    private int count;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        winText.text = "";
        SetCountText();
	}

    //Called before performing any physics calculations
    void FixedUpdate() {
        float moveHoriziontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHoriziontal, moveVertical);

        rb2d.AddForce(movement * speed);
    }
	
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if(count >= 12) {
            winText.text = "You Win!!";
        }
    }
}
