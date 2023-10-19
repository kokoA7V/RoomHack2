using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitDie : MonoBehaviour
{
    [SerializeField]
    private GameManager gMng;

    private int dieNum = 0;
    public void Die()
    {
        //if (this.TryGetComponent<MateController>(out MateController pc))
        //{
        //    dieNum++;
        //    gMng = gMng.GetComponent<GameManager>();
        //    Debug.Log(dieNum + "dieNum");
        //    if (dieNum>=gMng.mList.Count)
        //    {
        //        SceneManager.LoadScene("GameOverScene");
        //    }
        //}
        Destroy(gameObject);
    }
}
