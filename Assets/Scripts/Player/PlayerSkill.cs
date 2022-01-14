using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private static PlayerSkill s_Instance;
    public static PlayerSkill Instance { get { Init(); return s_Instance; } }

    public static int[] abllityList = new int[4];

    private void Start()
    {
        Init();
        abllityList[0] = 1;
    }

    private static void Init()
    {
        if (s_Instance == null)
        {
            GameObject obj = GameObject.Find("@PlayerSkill");
            if (obj == null)
            {
                obj = new GameObject { name = "@PlayerSkill" };
                obj.AddComponent<PlayerSkill>();
            }

            DontDestroyOnLoad(obj);

            s_Instance = obj.GetComponent<PlayerSkill>();
        }
    }

    public void MultiShotActive()
    {
        abllityList[0]++;
    }

    public void RicochetActive()
    {
        abllityList[1]++;
        abllityList[3]++;
    }

    public void WallBounceActive()
    {
        abllityList[2]++;
        abllityList[3]++;
    }

    public void PiercingActive()
    {
        abllityList[3]++;
    }
}
