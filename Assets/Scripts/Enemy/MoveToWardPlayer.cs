using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWardPlayer : MonoBehaviour
{
    private Transform target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
