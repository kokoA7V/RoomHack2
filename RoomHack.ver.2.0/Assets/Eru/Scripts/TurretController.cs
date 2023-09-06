using UnityEngine;

public class TurretController : MonoBehaviour, IUnitHack
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
    private GameObject top;

    [SerializeField]
    private RayCircle rayCircle = new RayCircle();


    private bool atkEnemyFlg = false;
    private bool shotFlg = false;

    void Start()
    {
        
    }

    void Update()
    {
        GameObject obj = rayCircle.CircleChk(transform);

        if (obj == null) return;
        ObjRotation(obj);
        if (!shotFlg) return;

        if (!hacked)
        {
            //if (obj.TryGetComponent<PlayerController>(out PlayerController pc)) Shoting(obj);
        }
        else if(atkEnemyFlg)
        {
            //if (obj.TryGetComponent<EnemyController>(out EnemyController ec)) Shoting(obj);
        }
    }

    private void ObjRotation(GameObject obj)
    {
        // �G�̈ʒu���玩���̈ʒu�������āA�G�����������x�N�g�����v�Z
        Vector3 direction = obj.transform.position - top.transform.position;

        // �x�N�g�����p�x�ɕϊ����ēG������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(top.transform.rotation, targetRotation, 5f);

        // �ڕW�̊p�x�ɑ΂��鋖�e�덷���ɂȂ�ƌ��������ƂƂ݂Ȃ�
        float currentAngle = transform.eulerAngles.z;
        if (Mathf.Abs(angle - currentAngle) < 3f || Mathf.Abs(angle - currentAngle) > 357f) shotFlg = true;
        else shotFlg = false;
    }

    private void Shoting(GameObject obj)
    {
        //�e����
    }

    public void StatusDisp()
    {
        atkEnemyFlg = true;
    }
}
