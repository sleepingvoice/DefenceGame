using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace Gu
{
    [Serializable]
    public class NotMovePoint
    {
        public List<Vector2Int> NotMove = new List<Vector2Int>();
    }


    public class AreaManager : MonoBehaviour
    {
        [Header("AreaSize")]
        public int width;
        public int height;

        [Header("GizmosCheck")]
        public bool CheckGizmos;

        [Header("Object")]
        public GameObject AreaObj;

        [Header("OutLine")]
        public ObjPool OutLinePool;
        public Material CanBuildMat;
        public Material NotBuildMat;

        //테스트 객체 나중에 지우기
        [Header("테스트")]
        public GameObject LineTest;
        public GameObject StartLastPos;
        [SerializeField]private bool CheckAStar = false;

        private float widthLength;
        private float heightLength;
        [HideInInspector]public Vector3 AreaSize;

        private MapData MapInfo = MainGameData.s_mapInfo;

		void Update()
        {
            // AStar체크용(에디터 전용)
            if (CheckAStar)
            {
                Instantiate(StartLastPos, this.transform).transform.position = MapInfo.PointList[new Vector2Int(0, 0)].CenterPoint;
                Instantiate(StartLastPos, this.transform).transform.position = MapInfo.PointList[new Vector2Int(7, 7)].CenterPoint;
                Instantiate(StartLastPos, this.transform).transform.position = MapInfo.PointList[new Vector2Int(7, 0)].CenterPoint;
                Instantiate(StartLastPos, this.transform).transform.position = MapInfo.PointList[new Vector2Int(0, 7)].CenterPoint;

                foreach (var Target in MainGameData.s_enemyInfo.TargetList)
                {
                    Instantiate(LineTest, this.transform).transform.position = Target.CenterPoint;
                }

                CheckAStar = false;
            }
        }

		#region InitValue

		public void SetList()
        {
            OutLinePool.ReturnObjectAll();

            AreaSize = AreaObj.GetComponent<Renderer>().bounds.size;

            SetLength(AreaSize);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector3 newPos = new Vector3(widthLength * i - AreaSize.x / 2, 1f, heightLength * j - AreaSize.z / 2);
                    MapAreaInfo info = new MapAreaInfo(new Vector2Int(i, j), newPos, newPos + new Vector3(widthLength / 2, 0, heightLength / 2), MapInfo.NotMoveList.NotMove.Contains(new Vector2Int(i, j)));
                    GameObject outline = OutLinePool.GetObject();

                    outline.transform.position = info.CenterPoint + Vector3.up * 0.2f;
                    if (info.NotMove)
                    {
                        outline.GetComponent<MeshRenderer>().material = CanBuildMat;
                        MapInfo.CanBuildObj.Add(outline);
                    }
                    else
                    {
                        outline.GetComponent<MeshRenderer>().material = NotBuildMat;
                        MapInfo.NotBuildObj.Add(outline);
                    }
                    outline.SetActive(false);

                    MapInfo.PointList.Add(info.NodeNum, info);
                }
            }
        }

        private void SetLength(Vector3 AreaSize)
        {
            MapInfo.Hegith = height;
            MapInfo.Width = width;

            MapInfo.AreawidthLength = AreaSize.x / width;
            MapInfo.AreaheigthLength = AreaSize.z / height;

            widthLength = MapInfo.AreawidthLength;
            heightLength = MapInfo.AreaheigthLength;
        }
        #endregion

        #region 테스트용 Gizmos

        private void OnDrawGizmos()
        {
            if (!CheckGizmos)
                return;

            Gizmos.color = Color.white;
            Vector3 TempSize = AreaObj.GetComponent<Renderer>().bounds.size;
            float widthLength = TempSize.x / width;
            float heigthLength = TempSize.z / height;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector3 newPos = new Vector3(widthLength * i - TempSize.x / 2, 1f, heigthLength * j - TempSize.z / 2);
                    Gizmos.DrawSphere(newPos, 0.5f);
                }
            }
        }

		#endregion
	}
}