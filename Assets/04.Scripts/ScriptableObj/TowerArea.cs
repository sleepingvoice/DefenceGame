using UnityEngine;

// 에디터에서 쉽게 사용할 수 있도록 메뉴를 만듬
[CreateAssetMenu(fileName = "TowerAreaObj", menuName = "Scriptable Object Asset/TowerAreaObj")]
public class TowerAtea : ScriptableObject
{
	public AreaInfo info;
}
