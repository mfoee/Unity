using UnityEngine;
using System.Collections;

public class back_btn : MonoBehaviour {

    public GameObject playerone;
    public GameObject playertwo;
    public GameObject sound_btn;
    public GameObject playgame_btn;
    public GameObject option_btn;
    public GameObject back;

    public void onClick() {
        playerone.SetActive(false);
        playertwo.SetActive(false);
        playgame_btn.SetActive(true);
        sound_btn.SetActive(true);
        option_btn.SetActive(true);
        back.SetActive(false);
    }
}
