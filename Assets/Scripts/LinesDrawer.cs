using UnityEngine;

/// <summary>
/// ���߿�����
/// </summary>
public class LinesDrawer : MonoBehaviour
{
    public GameObject linePrefab;

    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;

    [Space(30)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;

    Line currentLine;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();
        if (null != currentLine)
            Draw();
        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    // �����߼�-----------------------------------------------------------------------

    // ��ʼ����
    void BeginDraw()
    {
        // ʵ������Ԥ��
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
        // ���ò���
        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);


    }

    // ���߽�����
    void Draw()
    {
        var pos = cam.ScreenToWorldPoint(Input.mousePosition);
        // ��ֹ������֮�佻��
        RaycastHit2D hit = Physics2D.CircleCast(pos, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);
        if (hit)
            EndDraw();
        else
            currentLine.AddPoint(pos);
    }

    // ���߽���
    void EndDraw()
    {
        if (null == currentLine) return;
        if (currentLine.pointCount < 2)
        {
            Destroy(currentLine.gameObject);
        }
        else
        {
            currentLine.gameObject.layer = cantDrawOverLayerIndex;
            currentLine.UsePhysics(true);
            currentLine = null;
        }
    }

}
