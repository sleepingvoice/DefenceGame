using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gu
{
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
        private Vector3 AreaSize;

        private AreaInfo MapInfo = MainGameInfo.MapInfo;

        private void Awake()
        {
            SetList();
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
                    Vector2 MapNum = new Vector2((int)((int)(hit.point.x + AreaSize.x / 2) / widthLength), (int)((int)(hit.point.z + AreaSize.z / 2) / heightLength));
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
                Vector2 Infonum = new Vector2();
                for (int j = 0; j < height; j++)
                {
                    MapAreaInfo info = new MapAreaInfo();
                    Vector3 newPos = new Vector3(widthLength * i - AreaSize.x / 2, 1f, heightLength * j - AreaSize.z / 2);
                    info.Node = newPos;
                    info.CenterPoint = newPos + new Vector3(widthLength / 2, 0, heightLength / 2);
                    Infonum = new Vector2(i, j);
                    MapInfo.PointList.Add(Infonum, info);
                }
            }
        }

        private void SetLength(Vector3 AreaSize)
        {
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