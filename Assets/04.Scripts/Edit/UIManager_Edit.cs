using UnityEngine;

public class UIManager_Edit : MonoBehaviour
{
    
    public Material AreaMat;
    public Color NormalMatColor;
    [HideInInspector]public Material NormalMat;
    [HideInInspector]public CodinateList EditNode = new CodinateList();
    [HideInInspector]public RoundEnemyInfoList EnemyList = new RoundEnemyInfoList();

    private void Awake()
    {
        MainGameData.s_progressMainGame.AddListener(Active);
        NormalMatReturn();
     }

	private void Start()
	{
        MainGameData.s_progressEdit.SetValue(EditProgrss.main);
    }

	private void Active(GameProgress progress)
    {
        this.gameObject.SetActive(progress == GameProgress.Edit);

        if (progress == GameProgress.Edit)
        {
            GameManager.ins.AreaManager.SetMapObj();
        }
    }

    private void NormalMatReturn()
    {
        NormalMat = Instantiate(AreaMat);
        NormalMat.SetColor("_OutlineColor", NormalMatColor);
    }
}
