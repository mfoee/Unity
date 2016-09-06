using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        updateCountText();
        winText.text = "";
    }

    void updateCountText() {
        countText.text = "Count: " + count.ToString();
        if(count >= 12) {
            winText.text = "You Win!";
        }
    }

    void FixedUpdate() {
        if (Input.GetKeyUp(KeyCode.W))
            Debug.Log("key W");

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) {
        //Destroy(other.gameObject);
        if(other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
            count++;
            updateCountText();
        }
    }

}