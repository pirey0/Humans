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
            Spawn(10);
        }
        if (GUI.Button(new Rect(10, 70, 200, 30), "Spawn 100 " + prefab.name))
        {
            Spawn(100);
        }
        if (GUI.Button(new Rect(10, 100, 200, 30), "Spawn 1000 " + prefab.name))
        {
            Spawn(1000);
        }
    }

    private void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        var g = Instantiate(prefab);
        g.transform.position = transform.position;
    }
}
