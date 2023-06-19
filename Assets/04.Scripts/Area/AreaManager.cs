using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [Header("AStar")]
        public AstarCheck CheckNode;

        //�׽�Ʈ ��ü ���߿� �����
        [Header("�׽�Ʈ")]
        public GameObject Test;
        public GameObject LineTest;
        public GameObject StartLastPos;
        public GameObject Parents;
        public bool CheckAStar = false;
        public Vector2Int StartPos;
        public Vector2Int EndPos;

        private float widthLength;
        private float heightLength;
        private Vector3 AreaSize;

        private AreaInfo MapInfo = MainGameInfo.MapInfo;


        private void Awake()
        {
            MapInfo.NotMoveList = JsonUtility.FromJson<NotMovePoint>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/NotMoveList.json"));
            SetList();
        }

        

        void Update()
        {
            TouchCheck();

            // AStarüũ��
            if (CheckAStar)
            {
                foreach (var obj in Parents.GetComponentsInChildren<Transform>())
                {
                    if(obj != Parents.GetComponent<Transform>())
                        Destroy(obj.gameObject);
                }

                List<MapAreaInfo> TestInfo = CheckNode.PathFindingAstar(MapInfo.PointList[StartPos], MapInfo.PointList[EndPos]);
                Instantiate(StartLastPos, Parents.transform).transform.position = MapInfo.PointList[StartPos].CenterPoint;
                Instantiate(StartLastPos, Parents.transform).transform.position = MapInfo.PointList[EndPos].CenterPoint;
                for (int i = 0; i < TestInfo.Count; i++)
                {
                    Instantiate(LineTest, Parents.transform).transform.position = TestInfo[i].CenterPoint;
                }
                CheckAStar = false;
            }
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
                    MapAreaInfo info = new MapAreaInfo(new Vector2Int(i, j), newPos, newPos + new Vector3(widthLength / 2, 0, heightLength / 2), MapInfo.NotMoveList.NotMove.Contains(new Vector2Int(i, j)));

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

		#region �׽�Ʈ�� Gizmos

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