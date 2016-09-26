using UnityEngine;
using System.Collections;

public class PlayGame : MonoBehaviour {

    public GameObject playerone;
    public GameObject playertwo;
    public GameObject back_btn;

    public void onClick() {
        GameObject.FindWithTag("options").SetActive(false);
        GameObject.FindWithTag("sound").SetActive(false);
        GameObject.FindWithTag("play").SetActive(false);

        playerone.SetActive(true);
        playertwo.SetActive(true);
        back_btn.SetActive(true);
    }

}
