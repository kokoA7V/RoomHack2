using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField]
    List<MateController> mateList;

    // バーチャルカメラ
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    // Update is called once per frame
    void Update()
    {

        foreach (var item in mateList ?? new List<MateController>())
        {
            if (item != null)
            {
                if (item.leader)
                {
                    virtualCamera.Follow = item.GetComponent<Transform>();
                }
            }
        }
    }
}
