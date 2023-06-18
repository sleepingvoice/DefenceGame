using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gu
{
    [Serializable]
    public class NotMovePoint
    {
        public List<Vector2> NotMove = new List<Vector2>();
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

        [Header("AStar")]
        public AstarCheck CheckNode;

        //테스트 객체 나중에 지우기
        public GameObject Test;
        public GameObject LineTest;
        public GameObject StartLastPos;

        private float widthLength;
        private float heightLength;
        private Vector3 AreaSize;

        private AreaInfo MapInfo = MainGameInfo.MapInfo;


        private void Awake()
        {
            MapInfo.NotMoveList = JsonUtility.FromJson<NotMovePoint>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/NotMoveList.json"));
            SetList();

            //Astar 체크용
            List<MapAreaInfo> TestInfo = CheckNode.PathFindingAstar(MapInfo.PointList[new Vector2Int(0, 0)], MapInfo.PointList[new Vector2Int(7, 7)]);
            Instantiate(StartLastPos).transform.position = MapInfo.PointList[new Vector2Int(0, 0)].CenterPoint;
            Instantiate(StartLastPos).transform.position = MapInfo.PointList[new Vector2Int(7, 7)].CenterPoint;
            for (int i = 0; i < TestInfo.Count; i++)
            {
                Instantiate(LineTest).transform.position = TestInfo[i].CenterPoint;
            }
        }

        void Update()
        {
            TouchCheck();
        }

        private void TouchCheck()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = GameManager.ins.MainCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 6))
                {
                    Vector2Int MapNum = new Vector2Int((int)((int)(hit.point.x + AreaSize.x / 2) / widthLength), (int)((int)(hit.point.z + AreaSize.z / 2) / heightLength));
                    MapInfo.TouchMap.SetValue(MapInfo.PointList[MapNum]);
                }
            }
        }

		#region InitValue

		public void SetList()
        {
            AreaSize = AreaObj.GetComponent<Renderer>().bounds.size;


            SetLength(AreaSize);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector3 newPos = new Vector3(widthLength * i - AreaSize.x / 2, 1f, heightLength * j - AreaSize.z / 2);
                    MapAreaInfo info = new MapAreaInfo(new Vector2Int(i, j), newPos, newPos + new Vector3(widthLength / 2, 0, heightLength / 2), MapInfo.NotMoveList.NotMove.Contains(new Vector2(i, j)));

                    if (info.NotMove)
                    {
                        Instantiate(Test).transform.position = info.CenterPoint;
                    }
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