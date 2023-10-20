using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Rescue : GameManager
{
    [SerializeField, Header("レスキューユニット全部アタッチしてね")]
    public List<MateController> rList;

    [SerializeField, Header("ゴールエリアをアタッチしてね")]
    public Rigidbody2D goal;

    [SerializeField]
    public List<MateController> goalInObj;

    int rescueCount;

    private void Update()
    {
        CheckMateLost();

        CheckTimeUp();

        CheckGameEnd();

        CheckRescueLost();

        CheckRescueClear();
    }

    void CheckRescueLost()
    {
        rescueCount = rList.Count;
        for (int i = 0; i < rList.Count; i++)
        {
            if (rList[i] == null) rescueCount--;
        }

        if (rescueCount <= 0)
        {
            GameOverSceneManager.GameOverNo = 3;
            gameOver = true;
        }
    }

    void CheckRescueClear()
    {
        if (goalInObj.Count >= mateCount + rescueCount)
        {
            gameClear = true;
        }
    }
}
