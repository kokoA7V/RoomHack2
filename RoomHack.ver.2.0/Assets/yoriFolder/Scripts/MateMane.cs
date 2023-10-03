using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateMane : MonoBehaviour
{

    [SerializeField]
    private List<MateController> mateList;

    int i = 0;
    // Update is called once per frame
    void Start()
    {

        foreach (var item in mateList)
        {
            Debug.Log("りーだーさがしちゅ");
            if (item != null)
            {
                if (item.leader) i = 0;
                else
                {
                    i++;
                    if (i >= mateList.Count)
                    {
                        Debug.Log("リーダー変わったよ");
                        item.leader = true;
                    }
                }
            }
        }
    }
}

