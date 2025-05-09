using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class DrawingManager : MonoBehaviour
{
    [Header("Settings")]
    public float drawTime = 3f;
    public int drawsNeeded = 3;
    public float lineWidth = 0.1f;
    public Color lineColor = Color.white;
    public float smoothness = 0.5f;
    public float edgeRoundness = 0.3f;
    public float creamEffect = 0.2f;

    [Header("References")]
    public Transform tableLinesParent;
    public Transform cookieTransform;
    public Collider2D cookieCollider;
    public Material creamMaterial;
    public Texture2D creamTexture;

    private LineRenderer currentLine;
    private List<GameObject> allLines = new List<GameObject>();
    private int drawsCompleted;
    private float drawTimer;
    private bool isDrawing;
    private List<Vector3> drawingPoints = new List<Vector3>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDrawing && drawsCompleted < drawsNeeded)
        {
            StartDrawing();
        }

        if (isDrawing)
        {
            ContinueDrawing();
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            EndDrawing();
        }
    }

    void StartDrawing()
    {
        GameObject lineObj = new GameObject("Line");
        currentLine = lineObj.AddComponent<LineRenderer>();

        // Инициализация материала с текстурой
        Material lineMaterial = new Material(creamMaterial != null ? creamMaterial : new Material(Shader.Find("Sprites/Default")));
        lineMaterial.mainTexture = creamTexture;
        lineMaterial.color = lineColor;
        lineMaterial.SetFloat("_CreamEffect", creamEffect);

        currentLine.material = lineMaterial;
        currentLine.startColor = currentLine.endColor = lineColor;
        currentLine.startWidth = currentLine.endWidth = lineWidth;
        currentLine.positionCount = 0;
        currentLine.sortingOrder = 2;
        currentLine.numCapVertices = Mathf.RoundToInt(edgeRoundness * 10);
        currentLine.numCornerVertices = Mathf.RoundToInt(smoothness * 10);
        currentLine.textureMode = LineTextureMode.Tile;
        currentLine.material.mainTextureScale = new Vector2(1f, lineWidth * 10f);

        lineObj.transform.SetParent(tableLinesParent);
        allLines.Add(lineObj);
        isDrawing = true;
        drawTimer = 0f;
        drawingPoints.Clear();
    }

    void ContinueDrawing()
    {
        drawTimer += Time.deltaTime;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        drawingPoints.Add(mousePos);

        Vector3[] smoothedPoints = SmoothLine(drawingPoints.ToArray());
        currentLine.positionCount = smoothedPoints.Length;
        currentLine.SetPositions(smoothedPoints);

        if (drawTimer >= drawTime)
        {
            EndDrawing();
        }
    }

    void EndDrawing()
    {
        isDrawing = false;
        drawsCompleted++;

        if (drawsCompleted >= drawsNeeded)
        {
            PrepareLinesForAnimation();
            HandController.Instance.StartAnimation();
        }
    }

    Vector3[] SmoothLine(Vector3[] inputPoints)
    {
        if (inputPoints.Length < 2) return inputPoints;

        List<Vector3> smoothedPoints = new List<Vector3>();
        smoothedPoints.Add(inputPoints[0]);

        for (int i = 1; i < inputPoints.Length - 1; i++)
        {
            Vector3 prev = inputPoints[i - 1];
            Vector3 curr = inputPoints[i];
            Vector3 next = inputPoints[i + 1];

            Vector3 smoothed = (prev + curr * 2f + next) * 0.25f;
            smoothedPoints.Add(smoothed);
        }

        smoothedPoints.Add(inputPoints[inputPoints.Length - 1]);
        return smoothedPoints.ToArray();
    }

    void PrepareLinesForAnimation()
    {
        GameObject cookieLinesContainer = new GameObject("CookieLinesContainer");
        cookieLinesContainer.transform.SetParent(cookieTransform);

        foreach (GameObject lineObj in allLines)
        {
            LineRenderer originalLine = lineObj.GetComponent<LineRenderer>();
            List<Vector3> allPoints = new List<Vector3>();

            // Сохраняем материал оригинальной линии
            Material originalMaterial = originalLine.material;

            for (int i = 0; i < originalLine.positionCount; i++)
            {
                allPoints.Add(originalLine.GetPosition(i));
            }

            // Передаем оригинальный материал в SplitLine
            SplitLine(allPoints, cookieLinesContainer.transform, originalMaterial);

            Destroy(lineObj);
        }
    }

    void SplitLine(List<Vector3> points, Transform cookieParent, Material originalMaterial)
    {
        if (points.Count < 2) return;

        List<Vector3> currentSegment = new List<Vector3> { points[0] };
        bool wasInside = cookieCollider.OverlapPoint(points[0]);

        for (int i = 1; i < points.Count; i++)
        {
            bool isInside = cookieCollider.OverlapPoint(points[i]);

            if (isInside != wasInside)
            {
                Vector3 intersection = FindBorderIntersection(points[i - 1], points[i], wasInside);

                currentSegment.Add(intersection);
                CreateLineSegment(currentSegment, wasInside ? cookieParent : tableLinesParent, wasInside, originalMaterial);

                currentSegment = new List<Vector3> { intersection, points[i] };
                wasInside = isInside;
            }
            else
            {
                currentSegment.Add(points[i]);
            }
        }

        if (currentSegment.Count > 1)
        {
            CreateLineSegment(currentSegment, wasInside ? cookieParent : tableLinesParent, wasInside, originalMaterial);
        }
    }

    Vector3 FindBorderIntersection(Vector3 p1, Vector3 p2, bool p1Inside)
    {
        float min = 0f;
        float max = 1f;
        Vector3 result = p1;

        for (int j = 0; j < 10; j++)
        {
            float mid = (min + max) / 2f;
            Vector3 testPoint = Vector3.Lerp(p1, p2, mid);

            if (cookieCollider.OverlapPoint(testPoint) == p1Inside)
            {
                result = testPoint;
                if (p1Inside) max = mid;
                else min = mid;
            }
            else
            {
                if (p1Inside) min = mid;
                else max = mid;
            }
        }

        return result;
    }

    void CreateLineSegment(List<Vector3> points, Transform parent, bool isCookiePart, Material originalMaterial)
    {
        GameObject segmentObj = new GameObject(isCookiePart ? "CookieLine" : "TableLine");
        LineRenderer segment = segmentObj.AddComponent<LineRenderer>();

        // Используем копию оригинального материала
        Material segmentMaterial = new Material(originalMaterial);
        segment.material = segmentMaterial;

        segment.startColor = lineColor;
        segment.endColor = lineColor;
        segment.startWidth = lineWidth;
        segment.endWidth = lineWidth;
        segment.numCapVertices = Mathf.RoundToInt(edgeRoundness * 10);
        segment.numCornerVertices = Mathf.RoundToInt(smoothness * 10);

        // Изменяем порядок сортировки в зависимости от типа линии
        if (isCookiePart)
        {
            segment.sortingOrder = 2; // Линии на печенье поверх печенья
            segment.sortingLayerName = "CookieLines";
        }
        else
        {
            segment.sortingOrder = 1; // Линии на столе под печеньем
            segment.sortingLayerName = "TableLines";
        }

        segment.positionCount = points.Count;
        segment.textureMode = LineTextureMode.Tile;

        if (isCookiePart)
        {
            List<Vector3> localPoints = new List<Vector3>();
            foreach (Vector3 point in points)
            {
                localPoints.Add(point - cookieTransform.position);
            }
            segment.SetPositions(localPoints.ToArray());
            segment.useWorldSpace = false;
        }
        else
        {
            segment.SetPositions(points.ToArray());
            segment.useWorldSpace = true;
        }

        segmentObj.transform.SetParent(parent);
    }
}