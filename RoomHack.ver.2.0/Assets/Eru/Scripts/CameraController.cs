using System.Collections;
using System.Collections.Generic;
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
    public string[] lvStr = new string[2];
    [Multiline]
    public string comentStr;

    [SerializeField]
    private GameObject noiseObj;

    public SpriteRenderer frameSR;

    public Sprite frameSprite;

    void Start()
    {

    }
    
    void Update()
    {
        
    }

    public void StatusDisp()
    {
        Destroy(noiseObj);
    }
}
