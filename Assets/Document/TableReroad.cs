using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TableReroad : MonoBehaviour
{
    [MenuItem("CS_Util/Table/CSV &F1", false, 1)]
    static public void Parser_Table_CSV()
    {
        TableMgr mgr = new TableMgr();


        mgr.Init();
        mgr.Save();
    }
}
