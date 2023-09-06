using UnityEngine;

[System.Serializable]
public class RayCircle
{
    public int numberOfRays = 12; // ���C�̖{��
    public float detectionRange = 5f; // ���C�L���X�g�̍ő勗��

    private GameObject obj;
    //private LineRenderer lineRenderer; // ���C�̉����p

    //void Start()
    //{
    //    // LineRenderer�R���|�[�l���g���擾�܂��͍쐬
    //    lineRenderer = GetComponent<LineRenderer>();
    //    if (lineRenderer == null)
    //    {
    //        lineRenderer = gameObject.AddComponent<LineRenderer>();
    //    }

    //    // LineRenderer�̐ݒ�
    //    lineRenderer.positionCount = numberOfRays * 2; // ���_�̐���ݒ�i�n�_�ƏI�_��2�{�j
    //    lineRenderer.startWidth = 0.05f; // �J�n��
    //    lineRenderer.endWidth = 0.05f; // �I����
    //}

    public GameObject CircleChk(Transform transform)
    {
        obj = null;

        for (int i = 0; i < numberOfRays; i++)
        {
            // ���C�̊p�x���v�Z
            float angle = i * 360f / numberOfRays;

            // �p�x�����W�A���ɕϊ�
            float radians = angle * Mathf.Deg2Rad;

            // ���C�̕����x�N�g�����v�Z
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // ���C�L���X�g�𔭎�
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange);

            // ���C�L���X�g�������Ƀq�b�g�����ꍇ
            if (hit.collider != null && obj == null) obj = hit.collider.gameObject;
            else obj = null;

            //// ���C�̎n�_�ƏI�_��ݒ肵�ĉ���
            //lineRenderer.SetPosition(i * 2, transform.position); // �n�_
            //lineRenderer.SetPosition(i * 2 + 1, (Vector3)transform.position + (Vector3)(direction * detectionRange)); // �I�_
        }
        return obj;
    }
}

