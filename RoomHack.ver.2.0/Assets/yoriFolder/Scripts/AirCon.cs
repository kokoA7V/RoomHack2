using System.Collections.Generic;
using UnityEngine;

public class AirCon : MonoBehaviour,IUnitHack
{
    public string[] word;

    public bool randomFlg;

    public bool hacked { get; set; } = false;

    public Sprite icon;

    [Multiline]
    public string titleStr;
    [Multiline]
    public string[] lvStr = new string[2];
    [Multiline]
    public string comentStr;

    public SpriteRenderer frameSR;

    public Sprite frameSprite;

    [SerializeField]
    private Sprite frameEnemySprite;

    [SerializeField]
    private float[] hackTime = new float[3];

    private float time;

    private bool hackedFlg = false;

    private bool flg = false;

    private float exitTime = 0;

    private List<EnemyController> emList;

    private int methodNo = 0;

    [SerializeField]
    private GameObject exitObj;

    void Start()
    {
        emList = new List<EnemyController>();
    }

    void Update()
    {
        if (hackedFlg && GameData.AriConditionerLv == 1)
        {
            Debug.Log(emList.Count);
            exitTime += Time.deltaTime;
            if (exitTime <= 5f)
            {
                do
                {
                    emList[emList.Count - 1].unit = exitObj;
                } while (emList == null);
            }
        }
        else if (hackedFlg && GameData.AriConditionerLv == 2)
        {
            Debug.Log("暴走");
            flg = true;
        }
    }

    public void StatusDisp()
    {
        if (!hacked) return;
        if (time <= 0) time = hackTime[GameData.AriConditionerLv - 1];
        hackedFlg = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyController>(out EnemyController pc))
        {
            emList.Add(pc);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyController>(out EnemyController pc))
        {
            emList.Remove(pc);
        }
    }
}
