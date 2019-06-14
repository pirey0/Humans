using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;


    private void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,200,30), "Spawn " + prefab.name))
        {
            var g = Instantiate(prefab);
            g.transform.position = transform.position;
        }
    }
}
