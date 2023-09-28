using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField]
    List<MateController> mateList;

    // バーチャルカメラ
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in mateList)
        {
            if (item == null)
            {
                mateList.Remove(item);
            }
            if (item.leader)
            {
                virtualCamera.Follow = item.GetComponent<Transform>();
            }
        }
    }
}
