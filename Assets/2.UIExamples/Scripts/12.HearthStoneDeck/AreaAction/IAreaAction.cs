using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAreaAction
{
    void Init(CardPoolMgr poolMgr, DraggingRoot draggingRoot);
    void EnterArea();
    void ExitArea();
}
