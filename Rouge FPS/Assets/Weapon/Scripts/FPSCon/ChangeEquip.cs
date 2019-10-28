
using UnityEngine;
using System.Collections;

public class ChangeEquip : MonoBehaviour
{
    [SerializeField]
    public GameObject Weapon1;
    [SerializeField]
    public GameObject Weapon2;


    // Use this for initialization
    void Start()
    {
        Weapon1.SetActive(true);
        Weapon2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
  