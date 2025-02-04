using System.Collections.Generic;
using UnityEngine;

public class DialogCollisionScript : MonoBehaviour {
    public List<string> messages = new List<string>();
    private PlayerDialog playerDialog;
    private bool enqueued;

    void Start() {
        GameObject player = GameObject.Find("Player");
        if (player != null) {
            playerDialog = player.GetComponent<PlayerDialog>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!enqueued && other.CompareTag("Player")) {
            enqueued = true;
            if (playerDialog != null) {
                playerDialog.StartDialog(messages);
            }
        }
    }
}
