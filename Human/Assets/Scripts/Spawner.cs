using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] GameObject prefab;
    [SerializeField] Vector2 area;

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

        if (GUI.Button(new Rect(10, 130, 200, 30), "Spawn 10 in radius "))
        {
            SpawnInCube(10, transform.position, area);
        }
        
    }

    public void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        var g = Instantiate(prefab);
        g.transform.position = new Vector3(Random.value*100 -50, -0.27f, Random.value*100 -50);
    }

    public void SpawnInCube(int amount, Vector3 center, Vector2 scale)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 newPos = center + new Vector3(center.x + Random.value * scale.x*2 - scale.x, 0, center.z + Random.value*scale.y*2 - scale.y);
            SpawnAt(newPos);
        }
    }

    public void SpawnAt(Vector3 pos)
    {
        var g = Instantiate(prefab);
        g.transform.position = pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(area.x * 2, 0, area.y * 2));
    }
}
