using UnityEngine;

[System.Serializable]
public class RayCircle
{
    [SerializeField, Header("���C�̎n�_")]
    private Transform tr;

    [SerializeField, Header("���C�̖{��")]
    private int numberOfRays = 12;

    [SerializeField, Header("���C�̍ő勗��")]
    private float detectionRange = 5f;

    [SerializeField, Header("�Ώۂ̃��C���[")]
    private LayerMask layerMask;

    private enum CHK_TYPE
    {
        FULL = 360,
        HALF = 180,
        QUARTER = 90,
    }
    [SerializeField, Header("���m�͈�")]
    private CHK_TYPE chkType = CHK_TYPE.FULL;

    private enum GAME_TYPE
    {
        DOWN = 45,
        SIDE = 90,
    }
    [SerializeField, Header("�Q�[���^�C�v"), Tooltip("�����낵 or ��")]
    private GAME_TYPE gameType = GAME_TYPE.DOWN;

    private enum DIR_TYPE
    {
        RIGHT = -1,
        LEFT = 1,
    }
    [SerializeField, Header("��������")]
    private DIR_TYPE dirType = DIR_TYPE.LEFT;

    [SerializeField,Header("���C�̉���")]
    private bool rayFlg = false;

    private GameObject obj;

    public GameObject CircleChk()
    {
        obj = null;
        
        float q = 0;
        if (chkType == CHK_TYPE.QUARTER)
        {
            q = (int)gameType;
            if(tr.localScale.x < 0 && gameType == GAME_TYPE.SIDE) q = 0;
            if (tr.localScale.y < 0)
            {
                if (gameType == GAME_TYPE.SIDE)
                {
                    if (tr.localScale.x > 0) q += 90;
                    else q += 270;
                }
                else q += 180;
            }

            if (dirType == DIR_TYPE.RIGHT) q = (q * -1) + 90;
        }
        else if(chkType == CHK_TYPE.HALF)
        {
            if (tr.localScale.y < 0) q = 180;
        }
        else q = 0;

        for (int i = 0; i < numberOfRays +1; i++)
        {
            // ���C�̊p�x���v�Z
            float angle = (i * (int)chkType / numberOfRays) + q + tr.rotation.eulerAngles.z;

            // �p�x�����W�A���ɕϊ�
            float radians = angle * Mathf.Deg2Rad;

            // ���C�̕����x�N�g�����v�Z
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // ���C�L���X�g�𔭎�
            RaycastHit2D hit = Physics2D.Raycast(tr.position, direction, detectionRange, layerMask);

            if(rayFlg) Debug.DrawRay(tr.position, direction * detectionRange, Color.red);

            // ���C�L���X�g�������Ƀq�b�g�����ꍇ
            if (hit.collider != null)
            {
                obj = hit.collider.gameObject;
                break;
            }
            else obj = null;
        }
        return obj;
    }
}

