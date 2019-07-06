using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    [SerializeField] string tag;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag)
        {
            Destroy(other.gameObject);
        }
    }
}
