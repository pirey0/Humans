using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orient : MonoBehaviour
{
    Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Focus").transform;
    }

    void Update()
    {
        transform.LookAt(target);
    }
}
