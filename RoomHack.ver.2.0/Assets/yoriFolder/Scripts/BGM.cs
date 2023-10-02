using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioPlay.instance.BGMPlay(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
