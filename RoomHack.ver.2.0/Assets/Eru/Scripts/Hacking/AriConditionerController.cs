using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriConditionerController : MonoBehaviour, IUnitHack
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

    private List<EnemyController> emList;

    private int methodNo = 0;
    private int methodNo2 = 0;

    [SerializeField]
    private GameObject exitObj1;

    [SerializeField]
    private GameObject exitObj2;

    private float exitTime = 0;

    private bool flg = false;

    private GameObject emObj;

    [SerializeField]
    private DoorController door;
    void Start()
    {
        emList = new List<EnemyController>();
    }

    void Update()
    {
        if (time > 0) time -= Time.deltaTime;
        else if (hackedFlg && time <= 0)
        {
            hacked = false;
            hackedFlg = false;
            frameSR.sprite = frameEnemySprite;
        }

        if (hackedFlg && GameData.AriConditionerLv == 1)
        {
            Debug.Log("エアコン停止");
            if (hackedFlg && GameData.AriConditionerLv == 1)
            {
                switch (methodNo)
                {
                    case 0:
                        Debug.Log(emList.Count);
                        exitTime += Time.deltaTime;
                        if (exitTime >= 5f)
                        {
                            if (!flg)
                            {
                                emObj = emList[emList.Count - 1].gameObject;
                                emList[emList.Count - 1].unit = exitObj1;
                                flg = true;
                                methodNo++;
                            }
                        }
                        break;
                    case 1:
                        if ((Mathf.Abs(emObj.transform.position.x - exitObj1.transform.position.x) <= 0.5f &&
                        Mathf.Abs(emObj.transform.position.y - exitObj1.transform.position.y) <= 0.5f))
                        {
                            if (!door.openFlg)
                            {
                                door.bc2d.isTrigger = !door.bc2d.isTrigger;
                                door.StartCoroutine("Move");
                            }
                            emList[emList.Count - 1].unit = exitObj2;
                            methodNo++;
                        }
                        break;
                    case 2:
                        if (emList.Count <= 0) methodNo++;
                        break;
                }

            }
        }
        else if (hackedFlg && GameData.AriConditionerLv == 2)
        {
            Debug.Log("冷暖房起動");
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
            flg = true;
        }
    }
}
