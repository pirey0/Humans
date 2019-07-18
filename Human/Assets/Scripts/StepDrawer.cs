using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepDrawer : MonoBehaviour
{
    [SerializeField] Transform footLeft, footRight;
    [SerializeField] Material stampMat;
    [SerializeField] Texture2D stampTexture;



    public void LeftStep()
    {
        Debug.Log("Left");
        TryDrawFrom(footLeft.position);
    }

    public void RightStep()
    {
        Debug.Log("Right");
        TryDrawFrom(footRight.position);
    }



    private void TryDrawFrom(Vector3 pos)
    {
        RaycastHit hit;
        Debug.DrawLine(pos, pos + Vector3.down);
        if(Physics.Raycast(pos, Vector3.down, out hit))
        {
            OverlaySystem os = hit.transform.GetComponent<OverlaySystem>();
            if(os != null)
            {
                os.DrawAt(hit.textureCoord, stampTexture, stampMat);
            }

        }
    }
}
