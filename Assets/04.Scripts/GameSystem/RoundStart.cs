using UnityEngine;
using System.Collections;

public class RoundStart : MonoBehaviour
{
    GameManager gameManger = GameManager.ins;
    GameRule Rule = MainGameData.MainGameRule;

    private void Awake()
    {
        MainGameData.NowRound.AddListener(SetRound);
    }

    private void SetRound(int value)
    {
        StartCoroutine(RoundEnemy(value));  
    }

    public IEnumerator RoundEnemy(int value)
    {
        MainGameData.RoundTime.SetValue(Rule.RoundTime);
        for (int i = 0; i < Rule.RoundEnemy; i++)
        {
            gameManger.EnemyManager.MakeEnemy(value);
            yield return new WaitForSeconds(1f);
        }
    }
}
