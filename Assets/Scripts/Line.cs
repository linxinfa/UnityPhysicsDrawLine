using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 线脚本
/// </summary>
public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rigidBody;

    /// <summary>
    /// 点数组
    /// </summary>
    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointCount = 0;

    /// <summary>
    /// 画线过程中点与点的最小距离
    /// </summary>
    float pointsMinDistance = 0.1f;

    float circleColliderRadius;

    /// <summary>
    /// 添加点
    /// </summary>
    /// <param name="newPoint"></param>
    public void AddPoint(Vector2 newPoint)
    {
        if (pointCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
            return;

        points.Add(newPoint);
        ++pointCount;

        // add circle Collider
        var circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = circleColliderRadius;


        // Line Renderer
        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(pointCount - 1, newPoint);


        // Edge Collider
        if (pointCount > 1)
            edgeCollider.points = points.ToArray();
    }


    /// <summary>
    /// 获取最后一个点
    /// </summary>
    /// <returns></returns>
    public Vector2 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointCount - 1);
    }

    /// <summary>
    /// 是否启用物理特性
    /// </summary>
    public void UsePhysics(bool usePhysics)
    {
        rigidBody.isKinematic = !usePhysics;
    }

    /// <summary>
    /// 设置线颜色
    /// </summary>
    /// <param name="colorGradient"></param>
    public void SetLineColor(Gradient colorGradient)
    {
        lineRenderer.colorGradient = colorGradient;
    }

    /// <summary>
    /// 设置画线的点与点之间的最小距离
    /// </summary>
    /// <param name="distance"></param>
    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }

    /// <summary>
    /// 设置线宽度
    /// </summary>
    /// <param name="width"></param>
    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        circleColliderRadius = width / 2f;
        edgeCollider.edgeRadius = circleColliderRadius;
    }
}
