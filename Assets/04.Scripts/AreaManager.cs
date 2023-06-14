using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [Header("AreaSize")]
    public int width;
    public int height;

    [Header("GizmosCheck")]
    public bool CheckGizmos;

    [Header("Object")]
    public GameObject AreaObj;

    private float widthLength;
    private float heightLength;

    private AreaInfo MapInfo = MainGameModel.MapInfo;

    private void Awake()
    {
        SetList();
    }

    public void SetList()
    {
        MapInfo.PointList = new List<List<Vector3>>();

        SetLength();

        for (int i = -width / 2; i < width / 2; i++)
        {
            List<Vector3> WidthList = new List<Vector3>();

            for (int j = -height / 2; j < height / 2; j++)
            {
                Vector3 newPos = new Vector3(widthLength * i + widthLength / 2, 1f, heightLength * j + heightLength / 2);
                WidthList.Add(newPos);
            }
            MapInfo.PointList.Add(WidthList);
        }
    }

    private void SetLength()
    {
        Vector3 TempSize = AreaObj.GetComponent<Renderer>().bounds.size;
        MapInfo.AreawidthLength = TempSize.x / width;
        MapInfo.AreaheigthLength = TempSize.z / height;

        widthLength = MapInfo.AreawidthLength;
        heightLength = MapInfo.AreaheigthLength;
    }

    private void OnDrawGizmos()
    {
        if (!CheckGizmos)
            return;

        Gizmos.color = Color.white;
        Vector3 TempSize = AreaObj.GetComponent<Renderer>().bounds.size;
        float widthLength = TempSize.x / width;
        float heigthLength = TempSize.z / height;
        for (int i = -width / 2; i < width / 2; i++)
        {
            for (int j = -height / 2; j < height / 2; j++)
            {
                Vector3 newPos = new Vector3(widthLength * i + widthLength / 2, 1f, heigthLength * j + heigthLength / 2);
                Gizmos.DrawSphere(newPos, 0.5f);
            }
        }
    }
}
