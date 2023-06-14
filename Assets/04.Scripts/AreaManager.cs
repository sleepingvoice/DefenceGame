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

    public void SetList()
    {
        MainGameModel.PointList = new List<List<Vector3>>();
        Vector3 TempSize = AreaObj.GetComponent<Renderer>().bounds.size;
        float widthLength = TempSize.x / width;
        float heigthLength = TempSize.z / height;
        for (int i = -width / 2; i < width / 2; i++)
        {
            List<Vector3> WidthList = new List<Vector3>();

            for (int j = -height / 2; j < height / 2; j++)
            {
                Vector3 newPos = new Vector3(widthLength * i + widthLength / 2, 1f, heigthLength * j + heigthLength / 2);
                WidthList.Add(newPos);
            }
            MainGameModel.PointList.Add(WidthList);
        }
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
