using System.Text.RegularExpressions;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;



public class LoginUI : MonoBehaviour
{
	[Header("SocketBtn")]
	public Button SignBtn;
	public Button FindBtn;

	[Header("Windows")]
	public GameObject LoginWindow;
	public GameObject SignUpWindow;
	public GameObject IDWindow;
	public GameObject PwdWindow;

	[Header("Login")]
	public TMP_InputField InputLoginId;
	public TMP_InputField InputLoginPwd;
	public Button LoginBtn;

	[Header("SignUp")]
	public TMP_InputField InputSignId;
	public TMP_InputField InputSignPwd;
	public TMP_InputField InputSignEmail;
	public Button CheckEmailBtn;
	public Button SignExitBtn;
	public Button SignUpBtn;

	[Header("FindID")]
	public TMP_InputField InputFindIDEmail;
	public Button FindIDExitBtn;
	public Button FindIDBtn;

	[Header("DindPwd")]
	public TMP_InputField inputFindPwdID;
	public TMP_InputField inputFindPwdEmail;
	public Button FindPwdExitBtn;
	public Button FindPwdBtn;

	private Type<LoginProgress> _loginProgress = new Type<LoginProgress>(LoginProgress.main);

	public void Awake()
	{
		_loginProgress.AddListener(LoginProgressEvent);

		SignBtn.onClick.AddListener(() => _loginProgress.SetValue(LoginProgress.signup));
		FindBtn.onClick.AddListener(() => _loginProgress.SetValue(LoginProgress.findID));
		LoginBtn.onClick.AddListener(CheckLogin);
		CheckEmailBtn.onClick.AddListener(CheckSignEmail);
		SignUpBtn.onClick.AddListener(SignUp);

		Exitwindow();
	}

	private void Start()
	{
		_loginProgress.SetValue(LoginProgress.main);
	}

	private void LoginProgressEvent(LoginProgress progress)
	{
		LoginWindow.SetActive(progress == LoginProgress.main);
		SignUpWindow.SetActive(progress == LoginProgress.signup);
		IDWindow.SetActive(progress == LoginProgress.findID);
		PwdWindow.SetActive(progress == LoginProgress.findPwd);
	}

	private void Exitwindow()
	{
		SignExitBtn.onClick.AddListener(() => _loginProgress.SetValue(LoginProgress.main));
		FindIDExitBtn.onClick.AddListener(() => _loginProgress.SetValue(LoginProgress.main));
		FindPwdExitBtn.onClick.AddListener(() => _loginProgress.SetValue(LoginProgress.main));
	}

	public void CheckLogin()
	{
		if (InputLoginId.text == null)
		{
			Debug.Log("ID없음");
			return;
		}
		if (InputLoginPwd.text == null)
		{
			Debug.Log("비밀번호없음");
			return;
		}

		string massage = "Check_Login/" + InputLoginId.text + "/" + InputLoginPwd.text;

		Socket.ins.ws_SendMessage(massage);
	}

	public void CheckSignEmail()
	{
		if (InputLoginId.text == null || Regex.IsMatch(InputLoginId.text, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?"))
		{
			Debug.Log("이메일 양식에 맞지않음");
			return;
		}

		string massage = "Check_Email/" + InputSignEmail.text;

		Socket.ins.ws_SendMessage(massage);
	}

	public void SignUp()
	{
		string massage = "Add_ID/" + InputSignId.text + "/" + InputSignPwd.text + "/" + InputSignEmail.text;

		Socket.ins.ws_SendMessage(massage);
	}
}
