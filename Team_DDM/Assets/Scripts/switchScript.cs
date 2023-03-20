
using UnityEngine;

public class switchScript : MonoBehaviour, IDamage
{
    [SerializeField] Renderer switchModel;
    [SerializeField] GameObject door;
    [SerializeField] bool doorSwitch;
    [SerializeField] bool switchBool;// used for opening state
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
        if (doorSwitch)
        {
            gameManager.instance.doorSwitch();
        }else if(door != null)
        {
            door.SetActive(!switchBool);
        }

        // Optional can use GetSwitch

    }
    /// <summary>
    /// toggles the switch
    /// </summary>
    void ToggleSwitch()
    {
        switchBool = !switchBool;
    }
    /// <summary>
    /// Gets the bool of whether the switch is on or off
    /// </summary>
    /// <returns>switchBool</returns>
    public bool GetSwitch()
    {
        return switchBool;
    }
}
