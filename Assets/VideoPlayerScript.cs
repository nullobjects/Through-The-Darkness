using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour {
    public Canvas VideoPlayerCanvas;
    public VideoPlayer videoPlayer;

    private void Start () {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp) {
        Destroy(VideoPlayerCanvas.gameObject);
        Destroy(gameObject);
    }
}
