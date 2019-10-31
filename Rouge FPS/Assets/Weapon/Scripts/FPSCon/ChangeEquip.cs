using UnityEngine;
using System.Collections;

public class ChangeEquip : MonoBehaviour
{
    [SerializeField]
    public GameObject Weapon1;
    [SerializeField]
    public GameObject Weapon2;
    public int ownGun;        // (0:武器無し 1:weapon1 2:weapon2)

    // Inspector
    GunController PrimaryScript;
    GunController SecondaryScript;

    // Use this for initialization
    void Start()
    {
        GameObject PrimaryObject = transform.Find("FirstPersonCharacter/" + Weapon1.name).gameObject;
        PrimaryScript = PrimaryObject.GetComponent<GunController>();

        GameObject SecondaryObject = transform.Find("FirstPersonCharacter/" + Weapon2.name).gameObject;
        SecondaryScript = SecondaryObject.GetComponent<GunController>();

        Weapon1.SetActive(true);
        Weapon2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Weapon1.activeInHierarchy == true)
        {
            ownGun = 1;
        }
        else if(Weapon2.activeInHierarchy == true)
        {
            ownGun = 2;
        }
        else
        {
            ownGun = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && PrimaryScript.actionEnabled && SecondaryScript.actionEnabled)
        {
            ChangeWeapon();
        }

    }

    void ChangeWeapon()
    {
        if (Weapon2.activeSelf)
        {
            Weapon2.SetActive(false);
            Weapon1.SetActive(true);

        }
        else
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);

        }

    }
}
  