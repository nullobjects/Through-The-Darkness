using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerDialog : MonoBehaviour {
    GameObject dialogBox;
    TMP_Text messageText;
    CanvasGroup canvasGroup;
    Queue<string> messageQueue = new Queue<string>();
    bool isTyping = false;

    void Start() {
        dialogBox = transform.Find("Canvas_DialogBox")?.gameObject;
        if (dialogBox != null) {
            Transform messageTextTransform = dialogBox.transform.Find("MessageText");
            if (messageTextTransform != null) {
                messageText = messageTextTransform.GetComponent<TMP_Text>();
            }
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
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;

            messageText.text = "";
            foreach (char letter in text) {
                messageText.text += letter;
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(1f);

            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            if (messageQueue.Count > 0) {
                StartCoroutine(GenerateText(messageQueue.Dequeue()));
            } else {
                isTyping = false;
            }
        }
    }
}
