using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    [SerializeField] gunScript[] gunArray;
    [SerializeField] Transform[] shootPos;// not most optimal way to do this, but rushed way
    [SerializeField] int[] currentNumOfShootPos;

    int shootPosIter;

    private void Start()
    {
        int currentNumOfShootPosSum = 0;// finds sum
        foreach (int numOfShootPos in currentNumOfShootPos)
        {
            currentNumOfShootPosSum += numOfShootPos;
        }
        if (currentNumOfShootPosSum != shootPos.Length - 1)
        {
            Debug.Log(currentNumOfShootPosSum + "!=" + (shootPos.Length - 1));// outputs if the sum and the num of shootPos' don't equal the same amount
        }
    }
}
