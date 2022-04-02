using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int catsReturned;

    public SaveData (CatsReturnedCounter catsReturnedCounter)
    {
        catsReturned = catsReturnedCounter.catsReturned;
    }
}
