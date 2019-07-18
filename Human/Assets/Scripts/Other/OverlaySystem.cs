using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlaySystem : MonoBehaviour
{
    [SerializeField] Material overlayMat;

    [SerializeField] int texSize;

    [Header("Stamps")]
    [SerializeField] Texture2D stampTexture;
    [SerializeField] Material stampMaterial;

    Material myMat;
    RenderTexture myTex;
    RenderTexture auxTexture;


    // mouse debug draw
    private Vector3 _prevMousePosition;
    private bool _mouseDrag = false;

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
        auxTexture = new RenderTexture(texSize, texSize, 10);

        myMat.SetTexture("_MainTex",myTex);
    }

    void Update()
    {
        if (Camera.main == null)
            return;

        bool draw = false;
        float drawThreshold = 0.01f;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.gameObject != gameObject)
                return;
        }

        // force a draw on mouse down
        draw = Input.GetMouseButtonDown(0);
        // set draggin state
        _mouseDrag = Input.GetMouseButton(0);


        if (_mouseDrag && (draw || Vector3.Distance(hit.point, _prevMousePosition) > drawThreshold))
        {
            _prevMousePosition = hit.point;
            DrawAt(hit.textureCoord, stampTexture, stampMaterial);
        }
    }


    public void DrawAt(Vector2 texCoord, Texture2D stampTex, Material stampMat)
    {
        DrawAt(texCoord.x * myTex.width, texCoord.y * myTex.height, 1.0f, stampTex, stampMat);
    }


    void DrawAt(float x, float y, float alpha, Texture2D stampTex, Material stampMat)
    {
        Graphics.Blit(myTex, auxTexture);

        // activate our render texture
        RenderTexture.active = myTex;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, myTex.width, myTex.height, 0);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        // setup rect for our indent texture stamp to draw into
        Rect screenRect = new Rect();
        // put the center of the stamp at the actual draw position
        screenRect.x = x - stampTexture.width * 0.5f;
        screenRect.y = (myTex.height - y) - stampTexture.height * 0.5f;
        screenRect.width = stampTexture.width;
        screenRect.height = stampTexture.height;

        var tempVec = new Vector4();

        tempVec.x = screenRect.x / ((float)myTex.width);
        tempVec.y = 1 - (screenRect.y / (float)myTex.height);
        tempVec.z = screenRect.width / myTex.width;
        tempVec.w = screenRect.height / myTex.height;
        tempVec.y -= tempVec.w;

        stampMat.SetTexture("_MainTex", stampTexture);
        stampMat.SetVector("_SourceTexCoords", tempVec);
        stampMat.SetTexture("_SurfaceTex", auxTexture);

        // Draw the texture
        Graphics.DrawTexture(screenRect, stampTex, stampMat);

        GL.PopMatrix();
        RenderTexture.active = null;
    }

}
