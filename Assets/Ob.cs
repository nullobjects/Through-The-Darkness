using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ob : MonoBehaviour {
    public GameObject diaolagBox;
    public Text diaolagText;
    public string diaolog;
    public bool dialogActive;

    void Update() {
        if (dialogActive && diaolagBox != null && !diaolagBox.activeInHierarchy) {
            diaolagBox.SetActive(true);
            if (diaolagText != null) {
                diaolagText.text = diaolog;
            }
        } else if (!dialogActive && diaolagBox != null && diaolagBox.activeInHierarchy) {
            diaolagBox.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            dialogActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            dialogActive= false;
            if (diaolagBox != null) {
                diaolagBox.SetActive(false);
            }
        }
    }
}
