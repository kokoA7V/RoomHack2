using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour ,IUnitHack
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
    private GameObject left, right;

    public SpriteRenderer leftFrameSR, rightFrameSR;

    public Sprite frameSprite;

    private BoxCollider2D bc2d;

    [SerializeField]
    private Transform leftBoard, rightBoard;

    [SerializeField]
    private float speed;

    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        bc2d.isTrigger = false;
    }

    void Update()
    {
        
    }

    public void StatusDisp()
    {
        bc2d.isTrigger = true;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (Vector3.Distance(left.transform.position, leftBoard.position) > 0.01f || Vector3.Distance(right.transform.position, rightBoard.position) > 0.01f)
        {
            left.transform.position = Vector3.MoveTowards(left.transform.position, leftBoard.position, speed * Time.deltaTime);
            right.transform.position = Vector3.MoveTowards(right.transform.position, rightBoard.position, speed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
}
