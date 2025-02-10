using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_loader : MonoBehaviour {
    public key_collect key;
    public key_collect key2;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            GameObject KeyObject = GameObject.FindWithTag("key");

            if (KeyObject != null) {
                if (key.hasKey && (key2 == null || key2.hasKey)) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            } else {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}