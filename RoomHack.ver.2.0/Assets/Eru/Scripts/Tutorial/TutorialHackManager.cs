using UnityEngine;

public class TutorialHackManager : MonoBehaviour
{
    public TutorialTimeManager timeManager;

    public GameObject HackUIObj;

    public enum HACK_TYPE
    {
        Door1,
        Camera1,
        Enemy2,
        Enemy3,
        Door2,
    }
    [HideInInspector]
    public HACK_TYPE hackType;

    [HideInInspector]
    public bool nowTypingFlg = false;

    [HideInInspector]
    public GameObject nowObj;

    [HideInInspector]
    public TutorialManager tutorialManager;

    private GameObject nowHackUI;

    [SerializeField]
    private Sprite spriteCBG;

    private int h = 0;

    void Update()
    {
        if (nowTypingFlg) return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.TryGetComponent<IUnitHack>(out IUnitHack iUnitHack))
            {
                //ハックすべきものか確認
                if (hackType.ToString() != hit.collider.gameObject.name) return;

                // クリック処理
                if (HackUIObj == null) return;
                if (nowObj == hit.collider.gameObject) return;
                nowObj = hit.collider.gameObject;
                Destroy(nowHackUI);

                InstantHackUI(hit, iUnitHack);
            }
        }
    }

    public void InstantHackUI(RaycastHit2D hit, IUnitHack iUnitHack)
    {
        //HackUIを生成
        nowHackUI = Instantiate(HackUIObj);
        TutorialHackUI hackUI = nowHackUI.GetComponent<TutorialHackUI>();
        hackUI.tutorialHackManager = GetComponent<TutorialHackManager>();
        hackUI.unitHack = iUnitHack;
        hackUI.hit = hit;
        hackUI.timeManager = timeManager;
        hackUI.tutorialManager = tutorialManager;

        if (iUnitHack.hacked)
        {
            hackUI.imageBG.sprite = spriteCBG;
            h = 1;
        }
        else h = 0;

        //カメラ
        if (hit.collider.gameObject.TryGetComponent<TutorialCameraController>(out TutorialCameraController cameraCon))
        {
            hackUI._randomFlg = cameraCon.randomFlg;
            hackUI._word = new string[cameraCon.word.Length];
            for (int i = 0; i < cameraCon.word.Length; i++) hackUI._word[i] = cameraCon.word[i];

            hackUI.imageIcon.sprite = cameraCon.icon;
            hackUI.titleText.text = cameraCon.titleStr;
            hackUI.lvText.text = cameraCon.lvStr[h];
            hackUI.comentText.text = cameraCon.comentStr;

            if (h == 1) cameraCon.frameSR.sprite = cameraCon.frameSprite;
        }

        //ドア
        else if (hit.collider.gameObject.TryGetComponent<TutorialDoor>(out TutorialDoor doorCon))
        {
            hackUI._randomFlg = doorCon.randomFlg;
            hackUI._word = new string[doorCon.word.Length];
            for (int i = 0; i < doorCon.word.Length; i++) hackUI._word[i] = doorCon.word[i];

            hackUI.imageIcon.sprite = doorCon.icon;
            hackUI.titleText.text = doorCon.titleStr;
            hackUI.lvText.text = doorCon.lvStr[h];
            hackUI.comentText.text = doorCon.comentStr;

            if (h == 1)
            {
                doorCon.leftFrameSR.sprite = doorCon.frameMateSprite;
                doorCon.rightFrameSR.sprite = doorCon.frameMateSprite;
            }
        }

        // エネミー
        else if (hit.collider.gameObject.TryGetComponent<TutorialEnemy2>(out TutorialEnemy2 enemyCon))
        {
            hackUI._randomFlg = enemyCon.randomFlg;
            hackUI._word = new string[enemyCon.word.Length];
            for (int i = 0; i < enemyCon.word.Length; i++) hackUI._word[i] = enemyCon.word[i];

            hackUI.imageIcon.sprite = enemyCon.icon;
            hackUI.titleText.text = enemyCon.titleStr;
            hackUI.lvText.text = enemyCon.lvStr[h];
            hackUI.comentText.text = enemyCon.comentStr;

            if (h == 1) enemyCon.frameSR.sprite = enemyCon.frameSprite;
        }

        if (tutorialManager.j == 5) tutorialManager.questFlg = true;
    }
}
