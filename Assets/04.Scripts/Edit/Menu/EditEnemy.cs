using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditEnemy : EditMenuBase
{
	[Header("Function")]
	public Button AddRoundBtn;
	public TMP_Text RoundTex;
	public Button RoundUpBtn;
	public Button RoundDownBtn;

	public Button SpeedBtn;
	public Button HpBtn;

	public GameObject StateWindow;
	public TMP_Text StateTex;
	public TMP_InputField StateValue;

	public Button SaveBtn;

	private bool _nowStateHp = false;
	private Type<int> _nowRoundInt = new Type<int>(1);
	private bool EnemyInfoCheck = false;

	protected override void Awake()
	{
		base.Awake();

		_nowRoundInt.AddListener(RoundCehck);
		AddRoundBtn.onClick.AddListener(() =>
		{
			var info = new RoundEnemyInfo();
			info.RoundNum = _editManager.EnemyList.EnemyInfoList.Count + 1;
			_editManager.EnemyList.EnemyInfoList.Add(info);
			_nowRoundInt.InvokeListener();
		});
		RoundUpBtn.onClick.AddListener(() => _nowRoundInt.SetValue(_nowRoundInt.Value + 1));
		RoundDownBtn.onClick.AddListener(() => _nowRoundInt.SetValue(_nowRoundInt.Value - 1));
		SpeedBtn.onClick.AddListener(SetSpeed);
		HpBtn.onClick.AddListener(SetHp);
		StateValue.onValueChanged.AddListener(SetValue);
		SaveBtn.onClick.AddListener(SaveData);
		MainGameData.s_progressMainGame.AddListener((value) => EnemyInfoCheck = false);
	}

	protected override void Start()
	{
		base.Start();
		Init();
	}

	private void OnEnable()
	{
		if (_editManager != null && !EnemyInfoCheck)
			Init();
	}

	private void SetValue(string str)
	{
		if (_nowStateHp)
		{
			_editManager.EnemyList.EnemyInfoList[_nowRoundInt.Value - 1].hp = int.Parse(str);
		}
		else
		{
			_editManager.EnemyList.EnemyInfoList[_nowRoundInt.Value - 1].Speed = int.Parse(str);
		}
	}

	private void SetSpeed()
	{
		_nowStateHp = false;
		StateWindow.SetActive(true);
		StateTex.text = "MaxSpeed";
		StateValue.text = _editManager.EnemyList.EnemyInfoList[_nowRoundInt.Value - 1].Speed.ToString();
		SpeedBtn.GetComponent<Image>().color = Color.blue;
		HpBtn.GetComponent<Image>().color = Color.white;
	}

	private void SetHp()
	{
		_nowStateHp = true;
		StateWindow.SetActive(true);
		StateTex.text = "MaxHp";
		StateValue.text = _editManager.EnemyList.EnemyInfoList[_nowRoundInt.Value - 1].hp.ToString();
		SpeedBtn.GetComponent<Image>().color = Color.white;
		HpBtn.GetComponent<Image>().color = Color.blue;
	}

	private void Init()
	{
		_editManager.EnemyList = new RoundEnemyInfoList();
		var info = new RoundEnemyInfo();
		info.RoundNum = 1;
		_editManager.EnemyList.EnemyInfoList.Add(info);
		_nowRoundInt.SetValue(1);
	}

	private void RoundCehck(int value)
	{
		RoundTex.text = value.ToString();
		RoundUpBtn.gameObject.SetActive(_editManager.EnemyList.EnemyInfoList.Count > value);
		RoundDownBtn.gameObject.SetActive(value > 1);
		StateWindow.SetActive(false);
		SpeedBtn.GetComponent<Image>().color = Color.white;
		HpBtn.GetComponent<Image>().color = Color.white;
	}

	private void SaveData()
	{
		foreach (var info in _editManager.EnemyList.EnemyInfoList)
		{
			if (info.hp == 0 || info.Speed == 0)
			{
				Debug.LogError("값이 입력되지않았습니다.");
				return;
			}
		}

		MainGameData.s_progressEdit.SetValue(EditProgrss.main);
		EnemyInfoCheck = true;
	}

}
