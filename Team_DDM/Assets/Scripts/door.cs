
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] GameObject door1;
    [SerializeField] GameObject door2;
    public void Update()
    {
        if (gameManager.instance.doorState == true)
        {
            door1.SetActive(true);
            door2.SetActive(true);
        }
        else
        {
            door1.SetActive(false);
            door2.SetActive(false);
        }
    }
}
