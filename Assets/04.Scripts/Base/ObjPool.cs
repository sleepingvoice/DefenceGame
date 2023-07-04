using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gu
{
	public class ObjPool : MonoBehaviour
	{
		[SerializeField] public GameObject _prefab;

		public GameObject SetPrefab
		{
			set
			{
				_prefab = value;
			}
		}

		[SerializeField] private Queue<GameObject> _activePool = new Queue<GameObject>();
		[SerializeField] private Queue<GameObject> _disactivePool = new Queue<GameObject>();

		public GameObject GetObject()
		{
			if (_disactivePool.Count > 0)
			{
				GameObject obj = _disactivePool.Dequeue();
				_activePool.Enqueue(obj);
				obj.SetActive(true);
				return obj;
			}
			else
			{
				GameObject obj = Instantiate(_prefab, this.transform);
				_activePool.Enqueue(obj);
				return obj;
			}
		}

		public List<GameObject> ActivePoolReturn()
		{
			return _activePool.ToList();
		}

		public void DisableObject(GameObject Target)
		{
			if (!_activePool.Contains(Target))
				return;
			for (int i = 0; i < _activePool.Count; i++)
			{
				GameObject obj = _activePool.Dequeue();

				if (obj == Target)
				{
					_disactivePool.Enqueue(obj);
					break;
				}
				else
					_activePool.Enqueue(obj);
			}
			Target.SetActive(false);
		}

		public void ReturnObjectAll()
		{
			int max = _activePool.Count;
			for (int i = 0; i < max; i++)
			{
				GameObject obj = _activePool.Dequeue();
				obj.SetActive(false);
				_disactivePool.Enqueue(obj);
			}
		}

		public void RemoveList()
		{
			for (int i = 0; i < _activePool.Count; i++)
			{
				GameObject obj = _activePool.Dequeue();
				Destroy(obj);
			}

			for (int i = 0; i < _disactivePool.Count; i++)
			{
				GameObject obj = _disactivePool.Dequeue();
				Destroy(obj);
			}
		}
	}
}
