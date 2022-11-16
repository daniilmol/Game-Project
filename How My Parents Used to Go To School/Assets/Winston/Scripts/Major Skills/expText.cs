using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class expText : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    public PlayerController player;
    void Update() {
        text.SetText("Exp: " + player.tree.DisplayEXP());
    }
}
