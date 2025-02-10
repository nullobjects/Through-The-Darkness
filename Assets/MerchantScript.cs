using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantScript : MonoBehaviour {
    public TMPro.TMP_Text interactPrompt;
    public Canvas playerCanvas;
    private bool playerInRange = false;
    public GameObject shopUI;
    private static bool first_open = true;
    public PlayerDialog playerDialog;
    public List<string> messages = new List<string>();
    private bool finished_dialog = false;
    private GameObject player;
    private float interactDistance = 3f;

    private void Start() {
        player = GameObject.FindWithTag("Player");

        if (shopUI != null ) {
            shopUI.SetActive(false);
        }

        if (interactPrompt == null) return;
        interactPrompt.gameObject.SetActive(false);
    }

    void Update() {
        if (player != null) {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (shopUI == null || shopUI.activeSelf) return;

            if (distance <= interactDistance && !playerInRange) {
                playerInRange = true;
                interactPrompt.gameObject.SetActive(true);
            } else if (distance > interactDistance && playerInRange) {
                playerInRange = false;
                interactPrompt.gameObject.SetActive(false);
            }

            if (playerInRange && Input.GetKeyDown(KeyCode.E)) {
                OpenMerchantShop();
                interactPrompt.gameObject.SetActive(false);
            }
        }
    }

    public void OnMerchantDialogFinish() {
        finished_dialog = true;
        playerInRange = false;
    }

    private void OpenMerchantShop() {
        if (first_open) {
            first_open = false;
            playerDialog.StartDialog(messages);
            return;
        } else if (finished_dialog) {
            shopUI.SetActive(true);
        }
    }
}
