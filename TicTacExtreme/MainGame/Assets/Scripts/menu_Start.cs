using UnityEngine;
using System.Collections;

public class menu_Start : MonoBehaviour { 

    public GameObject playerone;
    public GameObject playertwo;

    public GameObject back_btn;

// Use this for initialization
    void Start () {
        playerone.SetActive(false);
        playertwo.SetActive(false);
        back_btn.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
