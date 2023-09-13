using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    [SerializeField, Header("スクリーンモードドロップダウン")]
    private Dropdown screenDropDown;

    [SerializeField, Header("解像度ドロップダウン")]
    private Dropdown resolutionDropDown;

    [SerializeField, Header("ライト")]
    private new Light light;

    [SerializeField, Header("明るさテキスト")]
    private Text brightnessText;

    [SerializeField, Header("明るさスライダー")]
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
    /// スクリーンモード変更
    /// </summary>
    public void ChangeScreenMode()
    {
        //フルスクリーンモード
        if (screenDropDown.value == 0) screenModeFlg = true;

        //ウィンドウモード
        else if (screenDropDown.value == 1) screenModeFlg = false;

        //更新
        ChangeDisplay();
    }

    /// <summary>
    /// 解像度変更
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

        //更新
        ChangeDisplay();
    }

    /// <summary>
    /// ディスプレイ設定変更
    /// </summary>
    private void ChangeDisplay()
    {
        Screen.SetResolution(width, height, screenModeFlg);
    }

    /// <summary>
    /// 画面の明るさ変更
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
        //UnityEditor上なら
        //Assetファイルの中のSaveファイルのパスを入れる
        string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //セーブファイルのパスを設定
        string SaveFilePath = path + "/display.bytes";

        // セーブデータの作成
        DisplaySaveData saveData = CreateSaveData();

        // セーブデータをJSON形式の文字列に変換
        string jsonString = JsonUtility.ToJson(saveData);

        // 文字列をbyte配列に変換
        byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

        // AES暗号化
        byte[] arrEncrypted = AesEncrypt(bytes);

        // 指定したパスにファイルを作成
        FileStream file = new FileStream(SaveFilePath, FileMode.Create, FileAccess.Write);

        //ファイルに保存する
        try
        {
            // ファイルに保存
            file.Write(arrEncrypted, 0, arrEncrypted.Length);
        }
        finally
        {
            // ファイルを閉じる
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void Load()
    {
#if UNITY_EDITOR
        //UnityEditor上なら
        //Assetファイルの中のSaveファイルのパスを入れる
        string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //セーブファイルのパスを設定
        string SaveFilePath = path + "/display.bytes";

        //セーブファイルがあるか
        if (File.Exists(SaveFilePath))
        {
            //ファイルモードをオープンにする
            FileStream file = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read);
            try
            {
                // ファイル読み込み
                byte[] arrRead = File.ReadAllBytes(SaveFilePath);

                // 復号化
                byte[] arrDecrypt = AesDecrypt(arrRead);

                // byte配列を文字列に変換
                string decryptStr = Encoding.UTF8.GetString(arrDecrypt);

                // JSON形式の文字列をセーブデータのクラスに変換
                DisplaySaveData saveData = JsonUtility.FromJson<DisplaySaveData>(decryptStr);

                //データの反映
                ReadData(saveData);

            }
            finally
            {
                // ファイルを閉じる
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

            //更新
            ChangeDisplay();
            SetBrightness(brightnessSlider.value);
        }
    }

    // セーブデータの作成
    private DisplaySaveData CreateSaveData()
    {
        //セーブデータのインスタンス化
        DisplaySaveData saveData = new DisplaySaveData();

        saveData.screenMode = screenDropDown.value;
        saveData.resolution = resolutionDropDown.value;
        saveData.brightness = brightnessSlider.value;

        return saveData;
    }

    //データの読み込み（反映）
    private void ReadData(DisplaySaveData saveData)
    {
        screenDropDown.value = saveData.screenMode;
        resolutionDropDown.value = saveData.resolution;
        brightnessSlider.value = saveData.brightness;
    }

    /// <summary>
    ///  AesManagedマネージャーを取得
    /// </summary>
    /// <returns></returns>
    private AesManaged GetAesManager()
    {
        //任意の半角英数16文字
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
    /// AES暗号化
    /// </summary>
    /// <param name="byteText"></param>
    /// <returns></returns>
    public byte[] AesEncrypt(byte[] byteText)
    {
        // AESマネージャーの取得
        AesManaged aes = GetAesManager();
        // 暗号化
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// <summary>
    /// AES復号化
    /// </summary>
    /// <param name="byteText"></param>
    /// <returns></returns>
    public byte[] AesDecrypt(byte[] byteText)
    {
        // AESマネージャー取得
        var aes = GetAesManager();
        // 復号化
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

    //セーブデータ削除
    public void Init()
    {
#if UNITY_EDITOR
        //UnityEditor上なら
        //Assetファイルの中のSaveファイルのパスを入れる
        string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //ファイル削除
        File.Delete(path + "/display.bytes");

        //リロード
        Load();

        Debug.Log("データの初期化が終わりました");
    }
}

[System.Serializable]
public class DisplaySaveData
{
    public int resolution;
    public int screenMode;
    public float brightness;
}