using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("味方ユニット全部アタッチしてね")]
    public List<MateController> mList;

    [SerializeField, Header("タイムマネージャーアタッチしてね")]
    StageTimeManager STM;

    [SerializeField, Header("ステージ報酬")]
    float stageReward = 100;

    [SerializeField, Header("時間制限（減衰率）")]
    float decreaseFactor = 1;

    public bool gameClear = false;
    public bool gameOver = false;
    public int mateCount;

    void Start()
    {
        STM.decreaseFactor = this.decreaseFactor;
    }

    void Update()
    {
        //Debug.Log(mList.Count);
        //if (mList.Count < 0) SceneManager.LoadScene("GameOverScene");

        if (Input.GetKeyDown(KeyCode.C))
        {
            gameClear = true;
        }

        CheckMateLost();

        CheckTimeUp();

        CheckGameEnd();
    }

    public void CheckMateLost()
    {
        mateCount = mList.Count;
        for (int i = 0; i < mList.Count; i++)
        {
            if (mList[i] == null) mateCount--;
        }

        if (mateCount <= 0)
        {
            GameOverSceneManager.GameOverNo = 1;
            gameOver = true;
        }
    }

    public void CheckTimeUp()
    {
        if (STM.timer <= 0)
        {
            GameOverSceneManager.GameOverNo = 2;
            gameOver = true;
        }
    }

    public void CheckGameEnd()
    {
        if (gameClear)
        {
            ResultSceneManager.resultTime = STM.timer;
            ResultSceneManager.resultLost = mList.Count - mateCount;
            ResultSceneManager.resultReward = stageReward;
            SceneManager.LoadScene("ResultScene");
        }
        else if (gameOver)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
