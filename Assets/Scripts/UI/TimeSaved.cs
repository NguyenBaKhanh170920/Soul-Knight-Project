using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSaved : MonoBehaviour
{
    [SerializeField]
    public Text recordTimeTxt;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        recordTimeTxt.text = "You have hold yourself for " + (TimeController.finishTime - TimeController.startTime).ToString("F2") + "s";
    }

}
