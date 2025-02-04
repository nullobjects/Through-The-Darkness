using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coin_manager : MonoBehaviour
{
    public bool flag = false;
    public TMPro.TMP_Text coinText;
    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            coinText.text = "Level  coin  collected!";
        }
    }
}
