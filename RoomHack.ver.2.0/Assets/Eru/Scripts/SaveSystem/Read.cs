//�t�@�C���̃f�[�^��ǂݍ��݂܂�

using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Read : MonoBehaviour
{
    void OnEnable()
    {
        DoRead();
    }

    private void DoRead()
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

        //�Z�[�u�t�@�C���̃p�X��ݒ�
        string SaveFilePath = path + "/save" + DataManager.saveFile + ".bytes";

        //�Z�[�u�t�@�C�������邩
        if (File.Exists(SaveFilePath))
        {
            DataManager.saveData = true;

            //�t�@�C�����[�h���I�[�v���ɂ���
            FileStream file = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read);
            try
            {
                // �t�@�C���ǂݍ���
                byte[] arrRead = File.ReadAllBytes(SaveFilePath);

                // ������
                byte[] arrDecrypt = AesDecrypt(arrRead);

                // byte�z��𕶎���ɕϊ�
                string decryptStr = Encoding.UTF8.GetString(arrDecrypt);

                // JSON�`���̕�������Z�[�u�f�[�^�̃N���X�ɕϊ�
                SaveData saveData = JsonUtility.FromJson<SaveData>(decryptStr);

                //�f�[�^�̔��f
                ReadData(saveData);

            }
            finally
            {
                // �t�@�C�������
                if (file != null)
                {
                    file.Close();
                }
            }
        }
        else
        {
            Debug.Log("�Z�[�u�t�@�C��������܂���");
            DataManager.saveData = false;
        }

        this.enabled = false;

    }

    //�f�[�^�̓ǂݍ��݁i���f�j
    private void ReadData(SaveData saveData)
    {
        GameData.Money = saveData.Money;
        GameData.DoorLv = saveData.DoorLv;
        GameData.CameraLv = saveData.CameraLv;
        GameData.TurretLv = saveData.TurretLv;
        GameData.EnemyLv = saveData.EnemyLv;
        GameData.AlarmLv = saveData.AlarmLv;
        GameData.CleanerLv = saveData.CleanerLv;
        GameData.DigestionLv = saveData.DigestionLv;
        GameData.ComputerLv = saveData.ComputerLv;
        GameData.AriConditionerLv = saveData.AriConditionerLv;
    }


    /// AesManaged�}�l�[�W���[���擾
    private AesManaged GetAesManager()
    {
        //�C�ӂ̔��p�p��16����(Save.cs�Ɠ������)
        string aesIv = "8901436610125689";
        string aesKey = "8396289437824386";

        AesManaged aes = new AesManaged();
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.IV = Encoding.UTF8.GetBytes(aesIv);
        aes.Key = Encoding.UTF8.GetBytes(aesKey);
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }

    /// AES������
    public byte[] AesDecrypt(byte[] byteText)
    {
        // AES�}�l�[�W���[�擾
        var aes = GetAesManager();
        // ������
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

}