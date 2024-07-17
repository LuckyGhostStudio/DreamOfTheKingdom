using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public float offsetSpeed = 0.1f;    // ÎÆÀíÆ«ÒÆËÙ¶È

    public void Update()
    {
        if (lineRenderer)
        {
            Vector2 offset = lineRenderer.material.mainTextureOffset;
            offset.x += offsetSpeed * Time.deltaTime;
            lineRenderer.material.mainTextureOffset = offset;
        }
    }
}
