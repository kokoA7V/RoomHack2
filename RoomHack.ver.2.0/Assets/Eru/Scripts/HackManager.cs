using UnityEngine;

public class HackManager : MonoBehaviour
{
    public StageTimeManager timeManager;

    public GameObject HackUIObj;

    [HideInInspector]
    public bool nowTypingFlg = false;

    [HideInInspector]
    public GameObject nowObj;

    private GameObject nowHackUI;

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

    public void InstantHackUI(RaycastHit2D hit, IUnitHack iUnitHack)
    {
        //HackUIを生成
        nowHackUI = Instantiate(HackUIObj);
        HackUI hackUI = nowHackUI.GetComponent<HackUI>();
        if (iUnitHack.hacked)
        {
            hackUI.unHacked.SetActive(false);
            hackUI.hacked.SetActive(true);
            h = 1;
        }
        else h = 0;

        hackUI.hackManager = GetComponent<HackManager>();
        hackUI.unitHack = iUnitHack;
        hackUI.hit = hit;
        hackUI.timeManager = timeManager;

        //カメラ
        if (hit.collider.gameObject.TryGetComponent<CameraController>(out CameraController cameraCon))
        {
            hackUI._randomFlg = cameraCon.randomFlg;
            hackUI._word = new string[cameraCon.word.Length];

            int rand = cameraCon.lv - GameData.CameraLv;
            if (rand <= 0) rand = 1;
            else if (cameraCon.word.Length < rand) rand = cameraCon.word.Length;

            for (int i = 0; i < rand; i++) hackUI._word[i] = cameraCon.word[i];

            hackUI.imageIcon.sprite = cameraCon.icon;
            hackUI.titleText.text = cameraCon.titleStr;
            hackUI.lvText.text = cameraCon.lvStr;
            hackUI.hackLvText.text = cameraCon.hackLvStr;
            hackUI.comentText.text = cameraCon.comentStr;

            if (h == 1) cameraCon.frameSR.sprite = cameraCon.frameSprite;
        }

        //ドア
        else if (hit.collider.gameObject.TryGetComponent<DoorController>(out DoorController doorCon))
        {
            hackUI._randomFlg = doorCon.randomFlg;
            hackUI._word = new string[doorCon.word.Length];

            int rand = doorCon.lv - GameData.DoorLv;
            if (rand <= 0) rand = 1;
            else if (doorCon.word.Length < rand) rand = doorCon.word.Length;

            for (int i = 0; i < rand; i++) hackUI._word[i] = doorCon.word[i];

            hackUI.imageIcon.sprite = doorCon.icon;
            hackUI.titleText.text = doorCon.titleStr;
            hackUI.lvText.text = doorCon.lvStr;
            hackUI.hackLvText.text = doorCon.hackLvStr;
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

            int rand = turretCon.lv - GameData.TurretLv;
            if (rand <= 0) rand = 1;
            else if (turretCon.word.Length < rand) rand = turretCon.word.Length;

            for (int i = 0; i < rand; i++) hackUI._word[i] = turretCon.word[i];

            hackUI.imageIcon.sprite = turretCon.icon;
            hackUI.titleText.text = turretCon.titleStr;
            hackUI.comentText.text = turretCon.comentStr;

            if (h == 1) turretCon.frameSR.sprite = turretCon.frameSprite;
        }

        //エネミー
        else if (hit.collider.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyCon))
        {
            hackUI._randomFlg = enemyCon.randomFlg;
            hackUI._word = new string[enemyCon.word.Length];

            int rand = enemyCon.lv - GameData.EnemyLv;
            if (rand <= 0) rand = 1;
            else if (enemyCon.word.Length < rand) rand = enemyCon.word.Length;

            for (int i = 0; i < rand; i++) hackUI._word[i] = enemyCon.word[i];

            hackUI.imageIcon.sprite = enemyCon.icon;
            hackUI.titleText.text = enemyCon.titleStr;
            hackUI.comentText.text = enemyCon.comentStr;

            if (h == 1) enemyCon.frameSR.sprite = enemyCon.frameSprite;
        }

        //警報装置
        else if (hit.collider.gameObject.TryGetComponent<AlarmController>(out AlarmController alarmCon))
        {
            hackUI._randomFlg = alarmCon.randomFlg;
            hackUI._word = new string[alarmCon.word.Length];

            int rand = alarmCon.lv - GameData.AlarmLv;
            if (rand <= 0) rand = 1;
            else if (alarmCon.word.Length < rand) rand = alarmCon.word.Length;

            for (int i = 0; i < rand; i++) hackUI._word[i] = alarmCon.word[i];

            hackUI.imageIcon.sprite = alarmCon.icon;
            hackUI.titleText.text = alarmCon.titleStr;
            hackUI.comentText.text = alarmCon.comentStr;

            if (h == 1) alarmCon.frameSR.sprite = alarmCon.frameSprite;
        }

        //お掃除ロボット
        else if (hit.collider.gameObject.TryGetComponent<CleanerController>(out CleanerController cleanerCon))
        {
            hackUI._randomFlg = cleanerCon.randomFlg;
            hackUI._word = new string[cleanerCon.word.Length];

            int rand = cleanerCon.lv - GameData.CleanerLv;
            if (rand <= 0) rand = 1;
            else if (cleanerCon.word.Length < rand) rand = cleanerCon.word.Length;

            for (int i = 0; i < rand; i++) hackUI._word[i] = cleanerCon.word[i];

            hackUI.imageIcon.sprite = cleanerCon.icon;
            hackUI.titleText.text = cleanerCon.titleStr;
 
            hackUI.comentText.text = cleanerCon.comentStr;

            if (h == 1) cleanerCon.frameSR.sprite = cleanerCon.frameSprite;
        }

        //消火設備
        else if (hit.collider.gameObject.TryGetComponent<DigestionController>(out DigestionController digestionCon))
        {
            hackUI._randomFlg = digestionCon.randomFlg;
            hackUI._word = new string[digestionCon.word.Length];

            int rand = digestionCon.lv - GameData.DigestionLv;
            if (rand <= 0) rand = 1;
            else if (digestionCon.word.Length < rand) rand = digestionCon.word.Length;

            for (int i = 0; i < rand; i++) hackUI._word[i] = digestionCon.word[i];

            hackUI.imageIcon.sprite = digestionCon.icon;
            hackUI.titleText.text = digestionCon.titleStr;
   
            hackUI.comentText.text = digestionCon.comentStr;

            if (h == 1) digestionCon.frameSR.sprite = digestionCon.frameSprite;
        }

        //パソコン
        else if (hit.collider.gameObject.TryGetComponent<ComputerController>(out ComputerController computerCon))
        {
            hackUI._randomFlg = computerCon.randomFlg;
            hackUI._word = new string[computerCon.word.Length];

            int rand = computerCon.lv - GameData.ComputerLv;
            if (rand <= 0) rand = 1;
            else if (computerCon.word.Length < rand) rand = computerCon.word.Length;

            for (int i = 0; i < rand; i++) hackUI._word[i] = computerCon.word[i];

            hackUI.imageIcon.sprite = computerCon.icon;
            hackUI.titleText.text = computerCon.titleStr;
            hackUI.comentText.text = computerCon.comentStr;

            if (h == 1) computerCon.frameSR.sprite = computerCon.frameSprite;
        }

        //エアコン
        else if (hit.collider.gameObject.TryGetComponent<AirConditionerController>(out AirConditionerController ariConditionerCon))
        {
            hackUI._randomFlg = ariConditionerCon.randomFlg;
            hackUI._word = new string[ariConditionerCon.word.Length];

            int rand = ariConditionerCon.lv - GameData.AriConditionerLv;
            if (rand <= 0) rand = 1;
            else if (ariConditionerCon.word.Length < rand) rand = ariConditionerCon.word.Length;

            for (int i = 0; i < rand; i++) hackUI._word[i] = ariConditionerCon.word[i];

            hackUI.imageIcon.sprite = ariConditionerCon.icon;
            hackUI.titleText.text = ariConditionerCon.titleStr;
            hackUI.comentText.text = ariConditionerCon.comentStr;

            if (h == 1) ariConditionerCon.frameSR.sprite = ariConditionerCon.frameSprite;
        }
    }
}