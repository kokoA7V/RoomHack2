using UnityEngine;
using UnityEngine.UI;

public class MateStatus : MonoBehaviour
{
    [SerializeField]
    private GameObject mObj;

    private MateController mCon;
    private UnitCore uCore;

    [SerializeField]
    private Image img;

    [SerializeField]
    private Text mName;

    private Text hrNumbar;

    [SerializeField]
    private GameObject leaderObj;

    [SerializeField]
    private GameObject menObj;

    [SerializeField]
    private Animator hRAnim;

    private int mMaxHp;
    private int nowHp;

    private int methodNo = 0;

    private float ctr = 0;



    private void Awake()
    {
        mCon = mObj.GetComponent<MateController>();
        uCore = mObj.GetComponent<UnitCore>();
        mMaxHp = uCore.maxHP;
        nowHp = mMaxHp;
        hRAnim = hRAnim.GetComponent<Animator>();
    }
    void Start()
    {
        img = img.GetComponent<Image>();

        hrNumbar = GetComponent<Text>();

        hrNumbar.text = Random.Range(70, 91).ToString();

        mName = mName.GetComponent<Text>();
        mName.text = mCon.mateName.ToString();
        Debug.Log(mCon.mateName);
    }

    // Update is called once per frame
    void Update()
    {
        if (mCon.isLeader)
        {
            leaderObj.SetActive(true);
            menObj.SetActive(false);
        }
        else
        {
            leaderObj.SetActive(false);
            menObj.SetActive(true);
        }

        if (nowHp != uCore.nowHP)
        {
            nowHp = uCore.nowHP;
        }
        switch (methodNo)
        {
            case 0:
                ctr += Time.deltaTime;
                if (ctr >= 1f)
                {
                    if (nowHp <= mMaxHp / 2)
                    {
                        img.color = new Color32(212, 161, 64, 255);
                        hrNumbar.color = new Color32(212, 161, 64, 255);
                        hRAnim.Play("HR_High");
                        ctr = 0;
                        methodNo++;
                        break;
                    }
                    hrNumbar.text = Random.Range(70, 91).ToString();
                    ctr = 0;
                    methodNo = 0;
                }
                break;
            case 1:
                ctr += Time.deltaTime;
                if (ctr >= 1f)
                {
                    if (nowHp <= mMaxHp / 3)
                    {
                        img.color = new Color32(255, 86, 81, 255);
                        hrNumbar.color = new Color32(255, 86, 81, 255);
                        hrNumbar.text = 0.ToString();
                        ctr = 0;                       
                        methodNo++;
                        break;
                    }
                    hrNumbar.text = Random.Range(41, 69).ToString();
                    ctr = 0;
                    methodNo = 1;
                }
                break;
            case 2:
                ctr += Time.deltaTime;
                if (ctr >= 1f)
                {
                    if (nowHp <= 0)
                    {
                        hRAnim.Play("HR_Die");
                        methodNo = 2;
                        ctr = 0;
                        return;
                    }
                    hrNumbar.text = Random.Range(20, 40).ToString();
                    ctr = 0;
                }
                break;
        }
    }
}
