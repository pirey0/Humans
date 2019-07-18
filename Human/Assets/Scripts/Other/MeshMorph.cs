using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMorph : MonoBehaviour
{
    [Range(0f, 1f)]
    public float MorphPercent;
    public Mesh OriginalMesh, FinalMesh;

    private Mesh MorphedMesh, NewOriginalMesh;



    void Start()
    {
        CreateSameVertexAmountMesh();
        MorphedMesh = Mesh.Instantiate(NewOriginalMesh);
        GetComponent<SkinnedMeshRenderer>().sharedMesh = MorphedMesh;

    }

    void Update()
    {
        MorphMeshes();
    }

    public void CreateSameVertexAmountMesh()
    {
        NewOriginalMesh = Mesh.Instantiate(FinalMesh);

        Vector3[] Ov, Fv, Nv;
        bool[] UsedV;


        Ov = OriginalMesh.vertices;
        Fv = FinalMesh.vertices;
        Nv = NewOriginalMesh.vertices;
        UsedV = new bool[Fv.Length];
        for (int t = 0; t < UsedV.Length; t++)
        {
            UsedV[t] = false;
        }
        // 3 Steps : 
        // Find out closest Vertex in the original for all ;

        for (int x = 0; x < Ov.Length; x++)
        {
            float dist = Mathf.Infinity;
            int closestVertexIndex = 0;

            for (int y = 0; y < Fv.Length; y++)
            {
                if (!UsedV[y])
                {
                    float curdist = Vector3.Distance(Ov[x], Fv[y]);
                    if (curdist < dist)
                    {
                        dist = curdist;
                        closestVertexIndex = y;
                    }
                }
            }

            Nv[closestVertexIndex] = Ov[x];
            UsedV[closestVertexIndex] = true;
        }


        for (int q = 0; q < Fv.Length; q++)
        {
            float dist = Mathf.Infinity;
            if (!UsedV[q])
            {
                for (int w = 0; w < Ov.Length; w++)
                {
                    float curdist = Vector3.Distance(Fv[q], Ov[w]);
                    if (curdist < dist)
                    {
                        dist = curdist;
                        Nv[q] = Ov[w];
                    }

                }
            }
        }

        NewOriginalMesh.vertices = Nv;
        NewOriginalMesh.RecalculateBounds();
    }



    public void MorphMeshes()
    {
        Vector3[] Nv, Fv, Mv;
        Nv = NewOriginalMesh.vertices;
        Fv = FinalMesh.vertices;
        Mv = new Vector3[Fv.Length];

        for (int i = 0; i < Mv.Length; i++)
        {
            Mv[i] = Vector3.Lerp(Nv[i], Fv[i], MorphPercent);
        }

        MorphedMesh.vertices = Mv;
        MorphedMesh.RecalculateBounds();

    }
}