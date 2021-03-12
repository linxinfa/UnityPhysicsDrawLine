using UnityEngine;

/// <summary>
/// 画线控制器
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

    // 画线逻辑-----------------------------------------------------------------------

    // 开始画线
    void BeginDraw()
    {
        // 实例化线预设
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
        // 设置参数
        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);


    }

    // 画线进行中
    void Draw()
    {
        var pos = cam.ScreenToWorldPoint(Input.mousePosition);
        // 防止线与线之间交叉
        RaycastHit2D hit = Physics2D.CircleCast(pos, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);
        if (hit)
            EndDraw();
        else
            currentLine.AddPoint(pos);
    }

    // 画线结束
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
