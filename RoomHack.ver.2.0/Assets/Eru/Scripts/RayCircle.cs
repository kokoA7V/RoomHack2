using UnityEngine;

[System.Serializable]
public class RayCircle
{
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

    [SerializeField,Header("���C�̉���")]
    private bool rayFlg = false;

    private GameObject obj;

    public GameObject CircleChk(Transform transform)
    {
        obj = null;
        
        int q;
        if (chkType == CHK_TYPE.QUARTER) q = (int)gameType;
        else q = 0;

        for (int i = 0; i < numberOfRays; i++)
        {
            // ���C�̊p�x���v�Z
            float angle = (i * (int)chkType / numberOfRays) + q + transform.rotation.eulerAngles.z;

            // �p�x�����W�A���ɕϊ�
            float radians = angle * Mathf.Deg2Rad;

            // ���C�̕����x�N�g�����v�Z
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // ���C�L���X�g�𔭎�
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, layerMask);

            if(rayFlg) Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

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

