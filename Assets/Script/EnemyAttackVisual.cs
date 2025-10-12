using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EnemyAttackVisual : MonoBehaviour
{
     [Range(10, 100)]
    public int segments = 30;
    public float radius = 2f;
    public float angle = 90f;
    public Color color = new Color(1, 0, 0, 0.25f);

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.widthMultiplier = 0.05f;
    }


    public void DrawCone()
    {
        lineRenderer.enabled = true;

        float halfAngle = angle / 2f;
        lineRenderer.positionCount = segments + 2;
        lineRenderer.SetPosition(0, Vector3.zero);

        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = -halfAngle + (angle / segments) * i;
            float rad = currentAngle * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
            lineRenderer.SetPosition(i + 1, pos);
        }
    }

    public void HideCone()
    {
        lineRenderer.enabled = false;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        float halfAngle = angle / 2f;
        Vector3 rightDir = Quaternion.Euler(0, 0, halfAngle) * transform.right;
        Vector3 leftDir = Quaternion.Euler(0, 0, -halfAngle) * transform.right;

        Gizmos.DrawRay(transform.position, rightDir * radius);
        Gizmos.DrawRay(transform.position, leftDir * radius);
    }
}
