//�f�[�^���Z�[�u������ǂݍ��񂾂肵�܂�

using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    //�N���X�̎Q��
    public Save saveClass;
    public Read readClass;

    //�Z�[�u�t�@�C���w��ϐ�
    public static int saveFile = 1;

    //�Z�[�u�f�[�^�����邩�̔���ϐ�
    public static bool saveData = false;

    void Start()
    {
        Read();
    }

    public void Read()
    {
        //�ǂݍ���
        readClass.enabled = true;
        Debug.Log("�ǂݍ��݂������܂���");
    }

    public void Save()
    {
        //�Z�[�u����
        saveClass.enabled = true;
        readClass.enabled = true;
        Debug.Log("�Z�[�u���ł��܂���");
    }

    private void OnDestroy()
    {
        Save();
    }

    //�Z�[�u�f�[�^�폜
    public void Delete()
    {
#if UNITY_EDITOR
        //UnityEditor��Ȃ�
        //Asset�t�@�C���̒���Save�t�@�C���̃p�X������
        string path = Application.dataPath + "/Save";

#else
        //�����łȂ����
        //.exe������Ƃ����Save�t�@�C�����쐬�������̃p�X������
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //�t�@�C���폜
        File.Delete(path + "/save" + saveFile + ".bytes");

        //�����[�h
        readClass.enabled = true;

        Debug.Log("�f�[�^�̍폜���I���܂���");
    }

    //�t�@�C���P
    public void File1()
    {
        saveFile = 1;
        Read();
    }

    //�t�@�C��2
    public void File2()
    {
        saveFile = 2;
        Read();
    }

    //�t�@�C��3
    public void File3()
    {
        saveFile = 3;
        Read();
    }
}