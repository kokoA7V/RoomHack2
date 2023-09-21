using UnityEngine;

public class HackManager : MonoBehaviour
{
    public GameObject HackUIObj;

    [HideInInspector]
    public bool nowTypingFlg = false;

    [HideInInspector]
    public GameObject nowObj;

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
                // クリック処理
                if (HackUIObj == null) return;
                if (nowObj == hit.collider.gameObject) return;
                nowObj = hit.collider.gameObject;
                Destroy(nowHackUI);

                InstantHackUI(hit, iUnitHack);
            }
        }
    }

    public void InstantHackUI(RaycastHit2D hit,IUnitHack iUnitHack)
    {
        //HackUIを生成
        nowHackUI = Instantiate(HackUIObj);
        HackUI hackUI = nowHackUI.GetComponent<HackUI>();
        hackUI.hackManager = GetComponent<HackManager>();
        hackUI.unitHack = iUnitHack;
        hackUI.hit = hit;

        if (iUnitHack.hacked)
        {
            hackUI.imageBG.sprite = spriteCBG;
            h = 1;
        }
        else h = 0;

        //カメラ
        if (hit.collider.gameObject.TryGetComponent<CameraController>(out CameraController cameraCon))
        {
            hackUI._randomFlg = cameraCon.randomFlg;
            hackUI._word = new string[cameraCon.word.Length];
            for (int i = 0; i < cameraCon.word.Length; i++) hackUI._word[i] = cameraCon.word[i];

            hackUI.imageIcon.sprite = cameraCon.icon;
            hackUI.titleText.text = cameraCon.titleStr;
            hackUI.lvText.text = cameraCon.lvStr[h];
            hackUI.comentText.text = cameraCon.comentStr;

            if(h == 1) cameraCon.frameSR.sprite = cameraCon.frameSprite;
        }

        //ドア
        else if (hit.collider.gameObject.TryGetComponent<DoorController>(out DoorController doorCon))
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

        //タレット
        else if (hit.collider.gameObject.TryGetComponent<TurretController>(out TurretController turretCon))
        {
            hackUI._randomFlg = turretCon.randomFlg;
            hackUI._word = new string[turretCon.word.Length];
            for (int i = 0; i < turretCon.word.Length; i++) hackUI._word[i] = turretCon.word[i];

            hackUI.imageIcon.sprite = turretCon.icon;
            hackUI.titleText.text = turretCon.titleStr;
            hackUI.lvText.text = turretCon.lvStr[h];
            hackUI.comentText.text = turretCon.comentStr;

            if (h == 1) turretCon.frameSR.sprite = turretCon.frameSprite;
        }

        //エネミー
        else if (hit.collider.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyCon))
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

        //警報装置
        else if (hit.collider.gameObject.TryGetComponent<AlarmController>(out AlarmController AlarmCon))
        {
            hackUI._randomFlg = AlarmCon.randomFlg;
            hackUI._word = new string[AlarmCon.word.Length];
            for (int i = 0; i < AlarmCon.word.Length; i++) hackUI._word[i] = AlarmCon.word[i];

            hackUI.imageIcon.sprite = AlarmCon.icon;
            hackUI.titleText.text = AlarmCon.titleStr;
            hackUI.lvText.text = AlarmCon.lvStr[h];
            hackUI.comentText.text = AlarmCon.comentStr;

            if (h == 1) AlarmCon.frameSR.sprite = AlarmCon.frameSprite;
        }

        //お掃除ロボット
        else if (hit.collider.gameObject.TryGetComponent<CleanerController>(out CleanerController CleanerCon))
        {
            hackUI._randomFlg = CleanerCon.randomFlg;
            hackUI._word = new string[CleanerCon.word.Length];
            for (int i = 0; i < CleanerCon.word.Length; i++) hackUI._word[i] = CleanerCon.word[i];

            hackUI.imageIcon.sprite = CleanerCon.icon;
            hackUI.titleText.text = CleanerCon.titleStr;
            hackUI.lvText.text = CleanerCon.lvStr[h];
            hackUI.comentText.text = CleanerCon.comentStr;

            if (h == 1) CleanerCon.frameSR.sprite = CleanerCon.frameSprite;
        }
    }
}