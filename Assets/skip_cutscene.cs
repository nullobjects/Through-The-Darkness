using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCutscene : MonoBehaviour
{
    public GameObject cutsceneObject;
    public GameObject cutsceneObject2;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            Skip();
        }
    }

    // Method to skip the cutscene
    private void Skip()
    {
        if (cutsceneObject != null)
        {
            Destroy(cutsceneObject);
        }
        if (cutsceneObject2 != null)
        {
            Destroy(cutsceneObject2);
        }
    }
}