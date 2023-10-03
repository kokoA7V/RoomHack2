using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator :MonoBehaviour
{
    private  List<string> mNames;


    private void Start()
    {
        mNames = new List<string>() { "John","kokoA7V","OSHO","Eru","NesikoNoNesiko",
            "V","DayBit","Lucy","esuha","Wick","Ethan", "Bond", "Hunt", "A113","Snake" };
    }
    //private string NameGene()
    //{
    //    for (int i = 0; i < mNames.Count; i++)
    //    {
    //        // workからランダムに1つ選んで値を取り出す
    //        int pos = Random.Range(0, mNames.Count);
    //        string value = mNames[pos];
    //        mNames.RemoveAt(pos); // 取り出した値はリストから取り去る

    //        // 取り出した値を順に返す
    //        yield return value;
    //    }
    //}
}
