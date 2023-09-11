using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace Gu
{
    public class AreaManager : MonoBehaviour
    {
        [Header("GizmosCheck")]
        public bool CheckGizmos;

        [Header("Object")]
        public GameObject AreaObj;

        [Header("OutLine")]
        public ObjPool AreaPool;
        public Material CanBuildMat;
        public Material NotBuildMat;

        //�׽�Ʈ ��ü ���߿� �����
        [Header("�׽�Ʈ")]
        public GameObject LineTest;
        public GameObject StartLastPos;
        [SerializeField]private bool CheckAStar = false;

        private bool _setMapSize = false;
        private float widthLength;
        private float heightLength;
        [HideInInspector]public Vector3 AreaSize;

        private ServerData _mapInfo = MainGameData.s_serverData;
        private ClientData _clientInfo = MainGameData.s_clientData;

		void Update()
        {
            // AStarüũ��(������ ����)
            if (CheckAStar)
            {

                foreach (var Target in _mapInfo.Codinate)
                {
                    Instantiate(LineTest, this.transform).transform.position = Target.CenterPoint;
                }

                CheckAStar = false;
            }
        }

        public void SetMap(int mapID)
        {
            foreach (var data in _mapInfo.MapinfoSever.List)
            {
                if (data.mapid == mapID)
                {
                    _mapInfo.NowMap.SetValue(data);
                    break;
                }
            }
        }

		#region InitValue

		public void SetMapObj(MapInfoList Mapinfolist = null)
        {
            if (!_setMapSize)
			{
                AreaSize = AreaObj.GetComponent<Renderer>().bounds.size;
                SetLength(AreaSize);
                _setMapSize = true;
            }

            AreaPool.ReturnObjectAll();

            List<MapAreaInfoSave> list = new List<MapAreaInfoSave>();

            if (Mapinfolist != null)
                list = Mapinfolist.InfoList;

            _mapInfo.AreaDic = new Dictionary<Vector2Int, AreaInfo>();
            for (int i = 0; i < _clientInfo.Hegith; i++)
            {
                for (int j = 0; j < _clientInfo.Width; j++)
                {
                    bool MoveCheck = false;
                    foreach (var Targetinfo in list)
                    {
                        if (Targetinfo.NodeNum == new Vector2Int(i, j))
                        {
                            MoveCheck = Targetinfo.NotMove; // ��ġ�� ���� ������ ��ȯ
                            break;
                        }
                    }

                    Vector3 newPos = new Vector3(widthLength * i - AreaSize.x / 2, 1f, heightLength * j - AreaSize.z / 2);
                    
                    GameObject outline = AreaPool.GetObject();
                
                    outline.GetComponent<MeshRenderer>().material = MoveCheck ? CanBuildMat : NotBuildMat;

                    AreaInfo info = new AreaInfo(new Vector2Int(i, j), newPos, newPos + new Vector3(widthLength / 2, 0, heightLength / 2), MoveCheck, outline);
                    _mapInfo.AreaDic.Add(new Vector2Int(i, j), info);
                }
            }
        }

        private void SetLength(Vector3 AreaSize)
        {
            _clientInfo.AreaWidthLength = AreaSize.x / _clientInfo.Width;
            _clientInfo.AreaHeigthLength = AreaSize.z / _clientInfo.Hegith;

            widthLength = _clientInfo.AreaWidthLength;
            heightLength = _clientInfo.AreaHeigthLength;
        }
        #endregion

        #region �׽�Ʈ�� Gizmos

        private void OnDrawGizmos()
        {
            if (!CheckGizmos)
                return;

            Gizmos.color = Color.white;
            Vector3 TempSize = AreaObj.GetComponent<Renderer>().bounds.size;
            float widthLength = TempSize.x / _clientInfo.Width;
            float heigthLength = TempSize.z / _clientInfo.Hegith;
            for (int i = 0; i < _clientInfo.Width; i++)
            {
                for (int j = 0; j < _clientInfo.Hegith; j++)
                {
                    Vector3 newPos = new Vector3(widthLength * i - TempSize.x / 2, 1f, heigthLength * j - TempSize.z / 2);
                    Gizmos.DrawSphere(newPos, 0.5f);
                }
            }
        }

		#endregion
	}
}