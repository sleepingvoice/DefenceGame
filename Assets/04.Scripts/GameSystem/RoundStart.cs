using UnityEngine;
using System.Collections;

public class RoundStart : MonoBehaviour
{
    GameManager gameManger;
    GameRule Rule = MainGameData.s_mainGameRule;

    private void Awake()
    {
        MainGameData.s_progressValue.AddListener(SetEnd);
        MainGameData.s_nowRound.AddListener(SetRound);
        MainGameData.s_roundTime.AddListener(SetTime);
        MainGameData.s_enemyNum.AddListener(SetMaxEnemy);
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
        if (value == MainGameData.s_enemyInfo.EnemyInfo.Count)
        {
            StartCoroutine(FinishGame());
            yield break;
        }
        MainGameData.s_roundTime.SetValue(Rule.RoundTime);
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
            MainGameData.s_nowRound.SetValue(MainGameData.s_nowRound.Value + 1);
            yield break;
        }
        yield return new WaitForSeconds(1f);
        MainGameData.s_roundTime.SetValue(value - 1);
    }

    private IEnumerator FinishGame()
    {
        while (MainGameData.s_enemyNum.Value > 0)
            yield return new WaitForSeconds(1f);
        MainGameData.s_progressValue.SetValue(GameProgress.End);
    }

    public void SetMaxEnemy(int value)
    {
        if (value >= MainGameData.s_mainGameRule.LimitEnemy)
        {
            MainGameData.s_progressValue.SetValue(GameProgress.End);
        }
    }
}
