using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ability
{
    string GetName();
    void Active();
    void DeActive();
    void DisplayName();
}
