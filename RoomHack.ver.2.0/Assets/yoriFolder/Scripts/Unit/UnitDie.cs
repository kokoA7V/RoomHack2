using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitDie : MonoBehaviour
{
    [SerializeField, Header("死んだときのエフェクト")]
    GameObject deathEfect;
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
