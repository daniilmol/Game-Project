using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public MapData mapData;
    public MapSystem mapSystem;

    void Start()
    {
        BuildMap();
    }


    void Update()
    {

    }

    void BuildMap()
    {
        if (mapSystem == null) return;
        mapSystem.RandRoomDatas();
    }
}
