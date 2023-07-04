using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class RoundStart : MonoBehaviour
{
    GameManager gameManger;
    GameRule Rule = MainGameData.MainGameRule;
    IEnumerator CheckTimeIenumerator = null;

    private void Awake()
    {
        MainGameData.ProgressValue.AddListener(SetEnd);
        MainGameData.NowRound.AddListener(SetRound);
        MainGameData.RoundTime.AddListener(SetTime);
        MainGameData.EnemyNum.AddListener(SetMaxEnemy);
    }

    private void SetEnd(GameProgress Progress)
    {
        if (Progress != GameProgress.GamePlay)
            StopAllCoroutines();
    }

    private void SetRound(int value)
    {
        StartCoroutine(RoundEnemy(value));  
    }

    public IEnumerator RoundEnemy(int value)
    {
        if (value == MainGameData.EnemyInfo.EnemyInfo.Count)
        {
            StartCoroutine(FinishGame());
            yield break;
        }
        MainGameData.RoundTime.SetValue(Rule.RoundTime);
        for (int i = 0; i < Rule.RoundEnemy; i++)
        {
            GameManager.ins.EnemyManager.MakeEnemy(value);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SetTime(int value)
    {
        StartCoroutine(CheckTime(value));
    }

    public IEnumerator CheckTime(int value)
    {
        if (value <= 0)
        {
            MainGameData.NowRound.SetValue(MainGameData.NowRound.Value + 1);
            yield break;
        }
        yield return new WaitForSeconds(1f);
        MainGameData.RoundTime.SetValue(value - 1);
    }

    private IEnumerator FinishGame()
    {
        while (MainGameData.EnemyNum.Value > 0)
            yield return new WaitForSeconds(1f);
        MainGameData.ProgressValue.SetValue(GameProgress.End);
    }

    public void SetMaxEnemy(int value)
    {
        if (value >= MainGameData.MainGameRule.LimitEnemy)
        {
            MainGameData.ProgressValue.SetValue(GameProgress.End);
        }
    }
}
