using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedRunController : MonoBehaviour
{
    [SerializeField]
    public Text durationTxt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        durationTxt.text = "You have hold yourself for "+(TimeController.endTime-TimeController.startTime).ToString("F2")+"s";
    }
}
