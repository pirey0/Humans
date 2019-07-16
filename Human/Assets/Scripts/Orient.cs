using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orient : MonoBehaviour
{
    Transform target;
    [Range(0, 1)] [SerializeField] float neckTurn;

    Vector3 prevAngle;
    float tStamp;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Focus").transform;
    }

    void LateUpdate()
    {


        if(Vector3.Dot(transform.forward, (target.position - transform.position).normalized) > neckTurn)
        {
            transform.LookAt(target);
            prevAngle = transform.localEulerAngles;
            tStamp = Time.time;
        }
        else
        {
            Vector3 newA;
            newA.x = Mathf.LerpAngle(prevAngle.x, 0, Time.time - tStamp);
            newA.y = Mathf.LerpAngle(prevAngle.y, 0, Time.time - tStamp);
            newA.z = Mathf.LerpAngle(prevAngle.z, 0, Time.time - tStamp);

            transform.localEulerAngles = newA;
        }

    }
}
