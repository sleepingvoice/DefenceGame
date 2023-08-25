using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class SortDicAction<T>
{
	private T _value;
	public T Value {get {return _value;}}
	private string dicName;
	public SortedDictionary<int, UnityAction<T>> DicValue;
	private Queue<UnityAction<T>> ActQueue;

	/// <summary>
	/// 이름을 정함으로써 에러생겼을때 확인
	/// </summary>
	public SortDicAction(string DicName,T init = default)
	{
		_value  = init;
		dicName = DicName;
	}

	public void SetValue(T value)
	{
		_value = value;
		InvokeDic(_value);
	}

	public void InvokeDic(T value)
	{
		for (int i = 0; i < ActQueue.Count; i++)
		{
			var tmpEvent = ActQueue.Dequeue();

			try
			{
				tmpEvent.Invoke(value);
			}
			catch (Exception e)
			{
				UnityEngine.Debug.LogError(dicName + " : " + e);
			}

			ActQueue.Enqueue(tmpEvent);
		}
	}

	/// <summary>
	/// 이벤트 추가
	/// </summary>
	public void InsertDic(UnityAction<T> value, int num = 0)
	{
		if (DicValue == null)
			DicValue = new SortedDictionary<int, UnityAction<T>>();

		if (num == 0)
			num = DicValue.Count + 1;

		if (DicValue.Count > num && DicValue[num] != null)
		{
			InsertDic(DicValue[num], num + 1); // 다음번호로 미룬다
			DicValue.Remove(num); // 남은 값은 지운다
		}

		DicValue.Add(num, value); // 겹치는것이 없으면 추가

		ActQueue = new Queue<UnityAction<T>>();
		var tmpList = DicValue.Keys.ToList();
		for (int i = 0; i < tmpList.Count; i++)
		{
			ActQueue.Enqueue(DicValue[tmpList[i]]);
		}
	}

	/// <summary>
	/// 순서 변경
	/// </summary>
	public void ReLocationDic(int fromValue, int toValue)
	{
		var tmpvalue = DicValue[fromValue];
		RenameKey(DicValue, fromValue, toValue);
		DicValue.Add(toValue,tmpvalue);
	}

	private void RenameKey<TKey, TValue>(IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
	{
		TValue value = dic[fromKey];
		dic.Remove(fromKey);
		dic[toKey] = value;
	}
}
