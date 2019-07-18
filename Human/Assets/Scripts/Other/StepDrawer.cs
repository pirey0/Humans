using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepDrawer : MonoBehaviour
{
    [SerializeField] Material stampMat;
    [SerializeField] Texture2D stampTexture;



    private void TryDrawFrom(Vector3 pos)
    {
        RaycastHit hit;
        Debug.DrawLine(pos, pos + Vector3.down);
        if (Physics.Raycast(pos, Vector3.down, out hit))
        {
            OverlaySystem os = hit.transform.GetComponent<OverlaySystem>();
            if (os != null)
            {
                os.DrawAt(hit.textureCoord, stampTexture, stampMat);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAA");
        TryDrawFrom(transform.position + Vector3.up * 0.3f);
    }

}
