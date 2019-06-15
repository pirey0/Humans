using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;


public class GraphSaver : Singleton<GraphSaver>
{
    public void SaveHumans(HumanController[] humans, string name)
    {
        int nr = 0;
        foreach (var c in humans)
        {
            SaveGraph(c.Graph, name + "-" + nr);
                nr++;
        }
    }

    public void SaveGraph(AI.DecisionTreeGraph graph, string name)
    {
        #if UNITY_EDITOR
        string time = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss_ff");
        string path = "Assets/Other/AIBehaviors/Saved/"+ name + "_" + time + ".asset";

        AssetDatabase.CreateAsset(graph, path);

        foreach (var n in graph.nodes)
        {
            AssetDatabase.AddObjectToAsset(n, path);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        #endif
    }
}
