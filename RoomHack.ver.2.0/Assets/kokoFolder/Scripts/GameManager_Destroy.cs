using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Destroy : GameManager
{
    [SerializeField, Header("敵ユニット全部アタッチしてね")]
    public List<EnemyController> eList;

    int enemyCount;

    private void Update()
    {
        CheckMateLost();

        CheckTimeUp();

        CheckGameEnd();

        CheckDestroy();
    }

    void CheckDestroy()
    {
        enemyCount = eList.Count;
        for (int i = 0; i < eList.Count; i++)
        {
            if (eList[i] == null) enemyCount--;
        }

        if (enemyCount <= 0)
        {
            gameClear = true;
        }
    }
}
