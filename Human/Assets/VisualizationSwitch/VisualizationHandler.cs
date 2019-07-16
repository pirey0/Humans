using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationHandler : MonoBehaviour {

    [SerializeField] Camera normalCam, modelCam;
    [SerializeField] Material imageEffectMat;

    [Range(0, 1)] public float ModelCamPercent;

    private int oldWidth;



    private void Awake()
    {
        SetRenderTextures();
        oldWidth = Screen.width;
    }

    private void SetRenderTextures()
    {
        var normalTex = new RenderTexture(Screen.width, Screen.height, 24);
        var modelTex = new RenderTexture(Screen.width, Screen.height, 24);

        if (normalCam.targetTexture != null)
        {
            normalCam.targetTexture.Release();
        }

        if (modelCam.targetTexture != null)
        {
            modelCam.targetTexture.Release();
        }

        normalCam.targetTexture = normalTex;
        modelCam.targetTexture = modelTex;

        Shader.SetGlobalTexture("_NormalTex", normalTex);
        Shader.SetGlobalTexture("_ModelTex", modelTex);
    }

    private void Update()
    {
        if (Input.GetKey (KeyCode.E))
        {
            ModelCamPercent += Time.deltaTime;
        }else if (Input.GetKey(KeyCode.Q))
        {
            ModelCamPercent -= Time.deltaTime;
        }

        ModelCamPercent = Mathf.Clamp01(ModelCamPercent);


        if(Screen.width != oldWidth)
        {
            oldWidth = Screen.width;
            SetRenderTextures();
            Debug.Log("Updating Rendering resolution");
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        imageEffectMat.SetFloat("_ModelPercent", ModelCamPercent);
        Graphics.Blit(source, destination, imageEffectMat);
    }
}
