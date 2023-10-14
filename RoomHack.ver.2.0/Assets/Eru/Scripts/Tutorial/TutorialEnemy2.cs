using UnityEngine;

public class TutorialEnemy2 : MonoBehaviour,IUnitHack
{
    [HideInInspector]
    public int hp = 3;

    [HideInInspector]
    public TutorialManager tutorialManager;

    void Update()
    {
        if (hp <= 0) Destroy(gameObject);
    }
    public string[] word;

    public bool randomFlg;

    public bool hacked { get; set; } = false;

    public Sprite icon;

    [Multiline]
    public string titleStr;
    [Multiline]
    public string lvStr;
    [Multiline]
    public string hackLvStr;
    [Multiline]
    public string comentStr;

    public SpriteRenderer frameSR;

    public Sprite frameSprite;

    [SerializeField]
    private Sprite frameEnemySprite;

    [SerializeField]
    private int q = 1;


    public void StatusDisp()
    {
        if(q == 1) tutorialManager.q1 = true;
        else if(q == 2) tutorialManager.q2 = true;
    }
}
