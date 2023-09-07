using UnityEngine;

[System.Serializable]
public class RayCircle
{
    [Header("���C�̖{��")]
    public int numberOfRays = 12;

    [Header("���C�̍ő勗��")]
    public float detectionRange = 5f;

    private GameObject obj;

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

            Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

            // ���C�L���X�g�������Ƀq�b�g�����ꍇ
            if (hit.collider != null)
            {
                obj = hit.collider.gameObject;
                break;
            }
            else obj = null;

            //// ���C�̎n�_�ƏI�_��ݒ肵�ĉ���
            //lineRenderer.SetPosition(i * 2, transform.position); // �n�_
            //lineRenderer.SetPosition(i * 2 + 1, (Vector3)transform.position + (Vector3)(direction * detectionRange)); // �I�_
        }
        return obj;
    }
}

