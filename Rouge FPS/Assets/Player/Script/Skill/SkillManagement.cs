using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagement : MonoBehaviour
{
    private bool WalkSkillActivate = false;
    private bool InfinityAmmoActive = false;
    private bool InfinityHealthActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K) && !WalkSkillActivate) { WalkSkillActivate = true;
            Debug.Log("WalkSkillActive=" + WalkSkillActivate);
        }
        else if (Input.GetKey(KeyCode.K) && WalkSkillActivate) { WalkSkillActivate = false;
            Debug.Log("WalkSkillActive=" + WalkSkillActivate);
        }

        if (Input.GetKey(KeyCode.L) && !InfinityAmmoActive) { InfinityAmmoActive = true;
            Debug.Log("InfinityAmmoActive=" + InfinityAmmoActive);
        }
        else if (Input.GetKey(KeyCode.L) && InfinityAmmoActive) { InfinityAmmoActive = false;
            Debug.Log("InfinityAmmoActive=" + InfinityAmmoActive);
        }

        if (Input.GetKey(KeyCode.H) && !InfinityHealthActive) { InfinityHealthActive = true;
            Debug.Log("InfinityHealthActive=" + InfinityHealthActive);
        }
        else if (Input.GetKey(KeyCode.H) && InfinityHealthActive) { InfinityHealthActive = false;
            Debug.Log("InfinityHealthActive=" + InfinityHealthActive);
        }
    }

    public bool GetWalkFlg() {
        return WalkSkillActivate;
    }

    public bool GetAmmoFlg() {
        return InfinityAmmoActive;
    }

    public bool GetHealthFlg()
    {
        return InfinityHealthActive;
    }
}
