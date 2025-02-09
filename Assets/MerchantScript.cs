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

    private void Start() {
        interactPrompt.gameObject.SetActive(false);
        shopUI.SetActive(false);
    }

    private void Update() {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) {
            OpenMerchantShop();
            interactPrompt.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
            interactPrompt.gameObject.SetActive(true);
        }
    }

    public void OnMerchantDialogFinish() {
        interactPrompt.gameObject.SetActive(true);
        finished_dialog = true;
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            if (interactPrompt != null) {
                interactPrompt.gameObject.SetActive(false);
            }
        }
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
