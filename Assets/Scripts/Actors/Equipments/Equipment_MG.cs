using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_MG : BaseEquipment
{
    private void Awake()
    {
        MainAction = new Action_Shot();
        SubAction = new Action_Attack();
    }
}
