using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlowTime : MonoBehaviour
{
    public float slowMotionDuration = 5.0f;
    public float slowMotionFactor = 0.1f;
    private float originalFixedDeltaTime;
    public GameObject player;
    void Start()
    {
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateSlowMotion();
        }
    }
    public void ActivateSlowMotion()
    {
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
        StartCoroutine(RestoreTimeAfterDelay());
    }
    private IEnumerator RestoreTimeAfterDelay()
    {
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
}
