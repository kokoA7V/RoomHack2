using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class ChangeSoundVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider masterSlider;

    [SerializeField]
    private Slider bgmSlider;

    [SerializeField]
    private Slider seSlider;

    [SerializeField]
    private Toggle masterToggle;

    [SerializeField]
    private Toggle bgmToggle;

    [SerializeField]
    private Toggle seToggle;

    private void Start()
    {
        Load();
    }

    private void OnDestroy()
    {
        Save();
    }

    public void SetBGM(float volume)
    {
        if (volume <= -50f)
        {
            volume = -80f;
            bgmToggle.isOn = false;
        }
        audioMixer.SetFloat("BgmVolume", volume);
    }

    public void SetSE(float volume)
    {
        if (volume <= -50f)
        {
            volume = -80f;
            seToggle.isOn = false;
        }
        audioMixer.SetFloat("SeVolume", volume);
    }

    public void SetMASTER(float volume)
    {
        if (volume <= -50f)
        {
            volume = -80f;
            masterToggle.isOn = false;
        }
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void MuteMASTER(bool mute)
    {
        float vol;
        if (!mute) vol = -80f;
        else vol = masterSlider.value;

        audioMixer.SetFloat("MasterVolume", vol);
    }

    public void MuteBGM(bool mute)
    {
        float vol;
        if (!mute) vol = -80f;
        else vol = bgmSlider.value;

        audioMixer.SetFloat("BgmVolume", vol);
    }

    public void MuteSE(bool mute)
    {
        float vol;
        if (!mute) vol = -80f;
        else vol = seSlider.value;

        audioMixer.SetFloat("SeVolume", vol);
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
        string SaveFilePath = path + "/SoundVolume.bytes";

        // セーブデータの作成
        SoundVolumeSaveData saveData = CreateSaveData();

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
        string SaveFilePath = path + "/SoundVolume.bytes";

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
                SoundVolumeSaveData saveData = JsonUtility.FromJson<SoundVolumeSaveData>(decryptStr);

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
            Debug.Log("SoundVolumeのセーブファイルがありません");
        }
    }



    // セーブデータの作成
    private SoundVolumeSaveData CreateSaveData()
    {
        //セーブデータのインスタンス化
        SoundVolumeSaveData saveData = new SoundVolumeSaveData();

        //ゲームデータの値をセーブデータに代入
        //Master
        saveData.masVol = masterSlider.value;   
        saveData.masFlg = masterToggle.isOn;

        //Bgm
        saveData.bgmVol = bgmSlider.value;
        saveData.bgmFlg = bgmToggle.isOn;

        //Se
        saveData.seVol = seSlider.value;
        saveData.seFlg = seToggle.isOn;

        return saveData;
    }

    //データの読み込み（反映）
    private void ReadData(SoundVolumeSaveData saveData)
    {
        float vol;

        //Master
        if (!saveData.masFlg)
        {
            masterToggle.isOn = false;
            vol = -80f;
        }
        else
        {
            masterToggle.isOn = true;
            vol = saveData.masVol;
        }
        masterSlider.value = saveData.masVol;
        audioMixer.SetFloat("MasterVolume", vol);

        //Bgm
        if (!saveData.bgmFlg)
        {
            bgmToggle.isOn = false;
            vol = -80f;
        }
        else
        {
            bgmToggle.isOn = true;
            vol = saveData.bgmVol;
        }
        bgmSlider.value = saveData.bgmVol;
        audioMixer.SetFloat("BgmVolume", vol);

        //Se
        if (!saveData.seFlg)
        {
            seToggle.isOn = false;
            vol = -80f;
        }
        else
        {
            seToggle.isOn = true;
            vol = saveData.seVol;
        }
        seSlider.value = saveData.seVol;
        audioMixer.SetFloat("SeVolume", vol);
    }



    /// <summary>
    ///  AesManagedマネージャーを取得
    /// </summary>
    /// <returns></returns>
    private AesManaged GetAesManager()
    {
        //任意の半角英数16文字(Read.csと同じやつに)
        string aesIv = "1897154867465325";
        string aesKey = "8984557159843457";

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
        File.Delete(path + "/SoundVolume.bytes");

        ////リロード
        Load();

        Debug.Log("データの削除が終わりました");
    }
}

[System.Serializable]
public class SoundVolumeSaveData
{
    public float masVol;
    public float bgmVol;
    public float seVol;
    public bool masFlg;
    public bool bgmFlg;
    public bool seFlg;
}
