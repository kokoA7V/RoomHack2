using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    [SerializeField, Header("�X�N���[�����[�h�h���b�v�_�E��")]
    private Dropdown screenDropDown;

    [SerializeField, Header("�𑜓x�h���b�v�_�E��")]
    private Dropdown resolutionDropDown;

    [SerializeField, Header("���C�g")]
    private new Light light;

    [SerializeField, Header("���邳�e�L�X�g")]
    private Text brightnessText;

    [SerializeField, Header("���邳�X���C�_�[")]
    private Slider brightnessSlider;

    private int width = 1920;
    private int height = 1080;
    private bool screenModeFlg = true;

    void Start()
    {
        if (light == null) light = GameObject.Find("Directional Light").GetComponent<Light>();
        Load();
        ChangeScreenMode();
        ChangeResolution();
        SetBrightness(brightnessSlider.value);
        Save();
    }

    private void OnDestroy()
    {
        Save();
    }

    /// <summary>
    /// �X�N���[�����[�h�ύX
    /// </summary>
    public void ChangeScreenMode()
    {
        //�t���X�N���[�����[�h
        if (screenDropDown.value == 0) screenModeFlg = true;

        //�E�B���h�E���[�h
        else if (screenDropDown.value == 1) screenModeFlg = false;

        //�X�V
        ChangeDisplay();
    }

    /// <summary>
    /// �𑜓x�ύX
    /// </summary>
    public void ChangeResolution()
    {
        //1920 * 1080
        if (resolutionDropDown.value == 0)
        {
            width = 1920;
            height = 1080;
        }

        //1680 * 1050
        else if (resolutionDropDown.value == 1)
        {
            width = 1680;
            height = 1050;
        }

        //1440 * 1080
        else if (resolutionDropDown.value == 2)
        {
            width = 1440;
            height = 1080;
        }

        //1280 * 1024
        else if (resolutionDropDown.value == 3)
        {
            width = 1280;
            height = 1024;
        }

        //1440 * 900
        else if (resolutionDropDown.value == 4)
        {
            width = 1440;
            height = 900;
        }

        //1280 * 960
        else if (resolutionDropDown.value == 5)
        {
            width = 1280;
            height = 960;
        }

        //1152 * 864
        else if (resolutionDropDown.value == 6)
        {
            width = 1152;
            height = 864;
        }

        //1280 * 720
        else if (resolutionDropDown.value == 7)
        {
            width = 1280;
            height = 720;
        }

        //1024 * 768
        else if (resolutionDropDown.value == 8)
        {
            width = 1024;
            height = 768;
        }

        //�X�V
        ChangeDisplay();
    }

    /// <summary>
    /// �f�B�X�v���C�ݒ�ύX
    /// </summary>
    private void ChangeDisplay()
    {
        Screen.SetResolution(width, height, screenModeFlg);
    }

    /// <summary>
    /// ��ʂ̖��邳�ύX
    /// </summary>
    /// <param name="brightness"></param>
    public void SetBrightness(float brightness)
    {
        light.intensity = brightness / 50f;
        brightnessText.text = brightness.ToString("F0");
    }








    public void Save()
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
        string SaveFilePath = path + "/display.bytes";

        // �Z�[�u�f�[�^�̍쐬
        DisplaySaveData saveData = CreateSaveData();

        // �Z�[�u�f�[�^��JSON�`���̕�����ɕϊ�
        string jsonString = JsonUtility.ToJson(saveData);

        // �������byte�z��ɕϊ�
        byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

        // AES�Í���
        byte[] arrEncrypted = AesEncrypt(bytes);

        // �w�肵���p�X�Ƀt�@�C�����쐬
        FileStream file = new FileStream(SaveFilePath, FileMode.Create, FileAccess.Write);

        //�t�@�C���ɕۑ�����
        try
        {
            // �t�@�C���ɕۑ�
            file.Write(arrEncrypted, 0, arrEncrypted.Length);
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

    public void Load()
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
        string SaveFilePath = path + "/display.bytes";

        //�Z�[�u�t�@�C�������邩
        if (File.Exists(SaveFilePath))
        {
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
                DisplaySaveData saveData = JsonUtility.FromJson<DisplaySaveData>(decryptStr);

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
            screenDropDown.value = 0;
            resolutionDropDown.value = 0;
            brightnessSlider.value = 1f;

            //�X�V
            ChangeDisplay();
            SetBrightness(brightnessSlider.value);
        }
    }

    // �Z�[�u�f�[�^�̍쐬
    private DisplaySaveData CreateSaveData()
    {
        //�Z�[�u�f�[�^�̃C���X�^���X��
        DisplaySaveData saveData = new DisplaySaveData();

        saveData.screenMode = screenDropDown.value;
        saveData.resolution = resolutionDropDown.value;
        saveData.brightness = brightnessSlider.value;

        return saveData;
    }

    //�f�[�^�̓ǂݍ��݁i���f�j
    private void ReadData(DisplaySaveData saveData)
    {
        screenDropDown.value = saveData.screenMode;
        resolutionDropDown.value = saveData.resolution;
        brightnessSlider.value = saveData.brightness;
    }

    /// <summary>
    ///  AesManaged�}�l�[�W���[���擾
    /// </summary>
    /// <returns></returns>
    private AesManaged GetAesManager()
    {
        //�C�ӂ̔��p�p��16����
        string aesIv = "8974632758937851";
        string aesKey = "7468735999189354";

        AesManaged aes = new AesManaged();
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.IV = Encoding.UTF8.GetBytes(aesIv);
        aes.Key = Encoding.UTF8.GetBytes(aesKey);
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }

    /// <summary>
    /// AES�Í���
    /// </summary>
    /// <param name="byteText"></param>
    /// <returns></returns>
    public byte[] AesEncrypt(byte[] byteText)
    {
        // AES�}�l�[�W���[�̎擾
        AesManaged aes = GetAesManager();
        // �Í���
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// <summary>
    /// AES������
    /// </summary>
    /// <param name="byteText"></param>
    /// <returns></returns>
    public byte[] AesDecrypt(byte[] byteText)
    {
        // AES�}�l�[�W���[�擾
        var aes = GetAesManager();
        // ������
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

    //�Z�[�u�f�[�^�폜
    public void Init()
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
        File.Delete(path + "/display.bytes");

        //�����[�h
        Load();

        Debug.Log("�f�[�^�̏��������I���܂���");
    }
}

[System.Serializable]
public class DisplaySaveData
{
    public int resolution;
    public int screenMode;
    public float brightness;
}