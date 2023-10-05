using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text CleanLevelText;
    public Text DigeLevelText;
    public Text ComputerLevelText;
    public Text AriconLevelText;
    public Text AlarmLevelText;
    public Text TurretLevelText;
    public Text EnemyLevelText;
    public Text DoorLevelText;
    public Text CameraLevelText;

    public int CleanLevelcounter = 1;
    public int DigeLevelcounter = 1;
    public int ComputerLevelcounter = 1;
    public int AriconLevelcounter = 1;
    public int AlarmLevelcounter = 1;
    public int TurretLevelcounter = 1;
    public int EnemyLevelcounter = 1;
    public int DoorLevelcounter = 1;
    public int CameraLevelcounter = 1;
    // Start is called before the first frame update
    void Start()
    {
        TextUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        TextUpdate();
    }

    void TextUpdate()
    {
       CleanLevelText.text = "" + CleanLevelcounter.ToString();
       DigeLevelText.text = "" + DigeLevelcounter.ToString();
       ComputerLevelText.text = "" + ComputerLevelcounter.ToString();
       AriconLevelText.text = "" + AriconLevelcounter.ToString();
       AlarmLevelText.text = "" + AlarmLevelcounter.ToString();
       TurretLevelText.text = "" + TurretLevelcounter.ToString();
       EnemyLevelText.text = "" + EnemyLevelcounter.ToString();
       DoorLevelText.text = "" + DoorLevelcounter.ToString();
       CameraLevelText.text = "" + CameraLevelcounter.ToString();
    }
}
