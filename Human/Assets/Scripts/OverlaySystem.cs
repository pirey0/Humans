using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlaySystem : MonoBehaviour
{
    [SerializeField] Material overlayMat;
    [SerializeField] int texSize;

    [SerializeField] Texture testingTexture;

    Material myMat;
    RenderTexture myTex;

    void Start()
    {
        var renderer = GetComponent<Renderer>();

        Material[] newMats = new Material[renderer.materials.Length + 1];

        for (int i = 0; i < renderer.materials.Length; i++)
        {
            newMats[i] = renderer.materials[i];
        }


        myMat = Material.Instantiate(overlayMat);
        newMats[newMats.Length - 1] = myMat;
        renderer.materials = newMats;


        myTex = new RenderTexture(texSize,texSize,10);
        myMat.SetTexture("_MainTex",myTex);

        Graphics.Blit(testingTexture, myTex);
    }


    public void DrawAt(Vector2 coord)
    {

    }

   
}
