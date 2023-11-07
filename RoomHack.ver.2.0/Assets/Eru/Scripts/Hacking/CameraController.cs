using UnityEngine;

public class CameraController : MonoBehaviour, IUnitHack
{
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

    [SerializeField]
    private GameObject noiseObj;

    public SpriteRenderer frameSR;

    public Sprite frameSprite;

    [Min(1)]
    public int lv = 1;

    [SerializeField]
    private Sprite frameEnemySprite;

    [SerializeField]
    private float[] hackTime = new float[3];

    private float time;

    private bool hackedFlg = false;

    private void Update()
    {
        if (time > 0) time -= Time.deltaTime;
        else if(hackedFlg && time <= 0)
        {
            hacked = false;
            hackedFlg = false;
            frameSR.sprite = frameEnemySprite;
            noiseObj.SetActive(true);
        }
    }

    public void StatusDisp()
    {
        if (!hacked) return;
        if (time <= 0) time = hackTime[GameData.CameraLv - 1];
        hackedFlg = true;
        noiseObj.SetActive(!noiseObj.activeSelf);
    }
}
