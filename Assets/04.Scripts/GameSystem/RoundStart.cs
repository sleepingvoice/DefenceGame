using UnityEngine;
using System.Collections;

public class RoundStart : MonoBehaviour
{
    GameData _userData = MainGameData.s_gameData;
    ClientData _clientData = MainGameData.s_clientData;

    private void Awake()
    {
        MainGameData.s_progressMainGame.AddListener(SetEnd);
        _userData.NowRound.AddListener(SetRound);
        _userData.NowRoundTime.AddListener(SetTime);
        _userData.NowEnemyNum.AddListener(SetMaxEnemy);
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
        if (value == MainGameData.s_serverData.EnemyInfo.Count)
        {
            StartCoroutine(FinishGame());
            yield break;
        }
        _userData.NowRoundTime.SetValue(_clientData.RoundTime);
        for (int i = 0; i < _clientData.RoundEnemy; i++)
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
            _userData.NowRound.SetValue(_userData.NowRound.Value + 1);
            yield break;
        }
        yield return new WaitForSeconds(1f);
        _userData.NowRoundTime.SetValue(value - 1);
    }

    private IEnumerator FinishGame()
    {
        while (_userData.NowEnemyNum.Value > 0)
            yield return new WaitForSeconds(1f);
        MainGameData.s_progressMainGame.SetValue(GameProgress.End);
    }

    public void SetMaxEnemy(int value)
    {
        if (value >= _clientData.LimitEnemy)
        {
            MainGameData.s_progressMainGame.SetValue(GameProgress.End);
        }
    }
}
