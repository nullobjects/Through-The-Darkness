using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class settings_menu : MonoBehaviour
{   
    public AudioMixer audio_mixer;
    public TMPro.TMP_Dropdown res_drop;
    Resolution[] res;
    // Start is called before the first frame update
    void Start() {
        if (res_drop == null) {
            return;
        }

        res = Screen.resolutions;
        res_drop.ClearOptions();
        List<string> options = new List<string>();
        int curr_res_idx = 0;
        for(int i = 0; i < res.Length; i++)
        {
            string option = res[i].width + " x " + res[i].height;
            options.Add(option);

            if(res[i].width == Screen.width && 
                res[i].height == Screen.height)
            {
                curr_res_idx = i;
            }
        }
        res_drop.AddOptions(options);
        res_drop.value = curr_res_idx;
        res_drop.RefreshShownValue();
    }

    public void quality(int qualIdx) {
        QualitySettings.SetQualityLevel(qualIdx);
    }

    public void set_fullscreen(bool isFs) {
        Screen.fullScreen = isFs;
    }

    public void volume(float vol) {
        audio_mixer.SetFloat("volume", Mathf.Log10(vol) * 20);
    }
}
