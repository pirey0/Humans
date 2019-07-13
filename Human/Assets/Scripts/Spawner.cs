using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] bool debug;
    [SerializeField] GameObject prefab;
    [SerializeField] Area[] areas;
    [SerializeField] SpawnTimer[] timers;


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

    public void SpawnInRandomArea(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Area area = areas[Random.Range(0, areas.Length)];
            Vector3 center = transform.position + area.displacedCenter;
            Vector3 scale = area.size;
            Vector3 newPos = center + new Vector3(center.x + Random.value * scale.x*2 - scale.x, 0, center.z + Random.value*scale.y*2 - scale.y);
            SpawnAt(newPos);
        }
    }

    public void SpawnAt(Vector3 pos)
    {
        var g = Instantiate(prefab, pos, Quaternion.identity);
    }


    private void Update()
    {

        if(timers != null)
        {
            foreach (var timer in timers)
            {
                Debug.Log("Spawn time: " + timer.Probability.Evaluate(Time.time));

                if (Random.value < timer.Probability.Evaluate(Time.time) * Time.deltaTime)
                {
                    if(timer.Position == SpawnTimer.PositionType.Random)
                    {
                        Spawn(timer.Amount);
                    }
                    else if(timer.Position == SpawnTimer.PositionType.Areas)
                    {
                        SpawnInRandomArea(timer.Amount);
                    }
                }
            }
        }   
    }

    private void OnGUI()
    {
        if (!debug)
        {
            return;
        }

        if (GUI.Button(new Rect(10, 10, 200, 30), "Spawn " + prefab.name))
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
            SpawnInRandomArea(10);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        if (areas != null)
        {
            foreach (var a in areas)
            {

                Vector3 center = transform.position + a.displacedCenter;
                Vector3 scale = a.size;

                Vector3 p1 = center + new Vector3(center.x + scale.x, 0, center.z + scale.y);
                Vector3 p2 = center + new Vector3(center.x - scale.x, 0, center.z - scale.y);

                Gizmos.DrawLine(p1, p2);
            }
        }
        
    }
}

[System.Serializable]
public class Area
{
    public Vector3 displacedCenter;
    public Vector2 size;
}

[System.Serializable]
public class SpawnTimer
{
    public PositionType Position;
    public AnimationCurve Probability;
    [Range(1,10)] public int Amount;


    public enum PositionType
    {
        Random,
        Areas
    }
}
