using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FinalVideoTrigger : MonoBehaviour {
    private bool Used = false;
    public VideoPlayer videoplayer;
    public Canvas VideoPlayerCanvas;
    public GameObject square;
    private LevelLoader LevelLoader;

    private void Start() {
        videoplayer.enabled = false;
        VideoPlayerCanvas.enabled = false;
        videoplayer.Pause();
        square.SetActive(false);
        LevelLoader = FindAnyObjectByType<LevelLoader>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (Used) return;

        if (other.CompareTag("Player")) {
            Used = true;
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.SetCanDie(false);

            videoplayer.enabled = true;
            VideoPlayerCanvas.enabled = true;
            videoplayer.Play();
            square.SetActive(true);
        }
    }

    void Update() {
        if (videoplayer == null || videoplayer.frame == (long)videoplayer.frameCount) {
            GoToMainMenu();
        }
    }

    void GoToMainMenu() {
        LevelLoader.LoadMainMenu();
    }
}
