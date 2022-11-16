using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FamilyTree : ScriptableObject
{
    //public static FamilyTree tree;
    public float exp = 0;
    // Start is called before the first frame update

    public void GetExp(float gain) {
        exp += gain;
        Debug.Log("Gained");
    }
    public float DisplayEXP() {
        return exp;
    }

    public void SpendingEXP(float spendsExp) {
        exp -= spendsExp;
    }
}
