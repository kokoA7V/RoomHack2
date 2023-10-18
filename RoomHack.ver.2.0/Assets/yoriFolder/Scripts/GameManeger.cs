using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    [SerializeField]
    public List<MateController> mList;

    [SerializeField]
    StageTimeManager STM;

    bool gameClear = false;
    bool gameOver = false;
    int mateCount;

    void Start()
    {
        mateCount = mList.Count;
    }

    void Update()
    {
        //Debug.Log(mList.Count);
        //if (mList.Count < 0) SceneManager.LoadScene("GameOverScene");

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

        if (STM.timer <= 0)
        {
            GameOverSceneManager.GameOverNo = 2;
            gameOver = true;
        }

        if (gameClear)
        {
            
        }
        else if (gameOver)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
