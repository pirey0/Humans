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
            Spawn();
        }
        if (GUI.Button(new Rect(10, 40, 200, 30), "Spawn 10 " + prefab.name))
        {
            for (int i = 0; i < 10; i++)
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        var g = Instantiate(prefab);
        g.transform.position = transform.position;
    }
}
