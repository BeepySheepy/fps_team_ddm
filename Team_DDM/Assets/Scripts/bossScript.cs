
using UnityEngine;

public class bossScript : MonoBehaviour
{
    [SerializeField] gunScript[] gunArray;
    [SerializeField] Transform[] shootPos;// not most optimal way to do this, but rushed way
    [SerializeField] int[] currentNumOfShootPos;
    

    int shootPosIter;
    int gunArrayIter = 0;

    

    /// <summary>
    /// 
    /// </summary>
    /// <returns>the current gun</returns>
    public gunScript GetCurrentGun()
    {
        return gunArray[gunArrayIter];
    }
    /// <summary>
    /// increments the gun iterator
    /// </summary>
    public void NextGun()
    {
        gunArrayIter++;
    }

    public Transform[] GetCurrentShootPositions()
    {
        Transform[] shootPositions = new Transform[currentNumOfShootPos[shootPosIter]];
        int currentNumOfShootPosSum = 0;
        for (int i = 0; i <= shootPosIter; i++)
        {
           currentNumOfShootPosSum += currentNumOfShootPos[i];// double check
        }
        for(int i = (currentNumOfShootPosSum - currentNumOfShootPos[shootPosIter]), ndx = 0;
            i < currentNumOfShootPosSum; i++, ndx++)
        {
            shootPositions[ndx] = shootPos[i];
        }

        return shootPositions;
    }
    public void NextShootPos()
    {
        shootPosIter++;
    }
}
