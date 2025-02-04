using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialog : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public string[] lines;
    public float textspeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI.text = string.Empty;
        StartDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(0)) { 
            if(textMeshProUGUI.text == lines[index])
            {
                Nextline();
            }
            else
            {
                StopAllCoroutines();
                textMeshProUGUI.text += lines[index];
            }
        }
    }

    void StartDialog()
    {
        index = 0;
        StartCoroutine(Typeline());
    }

    IEnumerator Typeline()
    {
        foreach(char line in lines[index].ToCharArray())
        {
            textMeshProUGUI.text += line;
            yield return new WaitForSeconds(textspeed);
        }
    }

    void Nextline()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textMeshProUGUI.text = string.Empty;
            StartCoroutine (Typeline());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
