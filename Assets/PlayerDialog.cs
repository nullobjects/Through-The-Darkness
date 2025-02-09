using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering.UI; // Needed for LayoutRebuilder

public class PlayerDialog : MonoBehaviour {
    public GameObject dialogBox;
    public TMP_Text messageText;
    public RectTransform messagePanel;
    private CanvasGroup canvasGroup;
    public Queue<string> messageQueue = new Queue<string>();
    public bool isTyping = false;
    public MerchantScript MerchantScript;

    void Start() {
        if (dialogBox != null) {
            messageText.text = "";
            ResizeBubble();

            canvasGroup = dialogBox.GetComponent<CanvasGroup>();
            if (canvasGroup != null) {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }
    }

    public void StartDialog(List<string> messages) {
        foreach (var msg in messages) {
            messageQueue.Enqueue(msg);
        }

        if (messageQueue.Count > 0 && !isTyping) {
            isTyping = true;
            StartCoroutine(GenerateText(messageQueue.Dequeue()));
        }
    }

    IEnumerator GenerateText(string text) {
        if (messageText != null) {
            //ResizeBubble();
            messageText.text = "";

            LayoutRebuilder.ForceRebuildLayoutImmediate(messageText.rectTransform);
            yield return null;

            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;

            foreach (char letter in text) {
                messageText.text += letter;
                messageText.ForceMeshUpdate();
                ResizeBubble();
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(1f);

            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            messageText.text = "";
            ResizeBubble();

            if (messageQueue.Count > 0) {
                StartCoroutine(GenerateText(messageQueue.Dequeue()));
            } else {
                isTyping = false;

                if (MerchantScript != null) {
                    // Merchants only have the dialog popup once so
                    // We can do a callback for the player to interact again for the shopui
                    MerchantScript.OnMerchantDialogFinish();
                }
            }
        }
    }

    void ResizeBubble() {
        if (messagePanel != null && messageText != null) {
            LayoutRebuilder.ForceRebuildLayoutImmediate(messageText.rectTransform);

            float newWidth = Mathf.Max(messageText.textBounds.size.x, 0);
            float newHeight = Mathf.Max(messageText.textBounds.size.y * 1.75f, 0);

            messagePanel.offsetMax = new Vector2(-2.5f + newWidth, -1.5f + newHeight);
            messagePanel.offsetMin = new Vector2(messagePanel.offsetMin.x, messagePanel.offsetMin.y);

            messageText.rectTransform.anchoredPosition = new Vector2(0.125f, -1.55f + newHeight * 0.8f);
        }
    }
}
