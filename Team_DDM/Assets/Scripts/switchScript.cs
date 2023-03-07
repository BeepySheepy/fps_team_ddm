using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScript : MonoBehaviour, IDamage
{
    [SerializeField] Renderer switchModel;
    bool switchBool;
    Color modelColorOrig;
    private void Start()
    {
        modelColorOrig = switchModel.material.color;
        switchBool = false;
    }

    public void takeDamage(int dmg)
    {
        Debug.Log(name + " enters damage script");
        ToggleSwitch();
        if (switchBool)
        {
            switchModel.material.color = Color.red;
        }
        else
        {
            switchModel.material.color = modelColorOrig;
        }
    }

    void ToggleSwitch()
    {
        switchBool = !switchBool;
    }

    public bool GetSwitch()
    {
        return switchBool;
    }
}
