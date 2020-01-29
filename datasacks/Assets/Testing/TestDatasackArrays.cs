using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDatasackArrays : MonoBehaviour
{
    void DebugOut(string s)
    {
        DSM.Testing.Debug.Value += s;
    }

    void PrintArray(Datasack ds)
    {
        string s = ds.FullName;

        int length = ds.GetArrayLength();

        s += System.String.Format(": {0} elements. [", length);

        for (int i = 0; i < length; i++)
        {
            s = s + System.String.Format("{0}{1}", ds.GetArrayValue(i), (i < length - 1) ? "," : "");
        }

        s += "]\n";

        DebugOut(s);
    }

    void TestBasicOperation()
    {
        DSM.Testing.Debug.Value = "TestBasicOperation():\n";

        DSM.Testing.Array1.Clear();
        DSM.Testing.Array2.Clear();

        DSM.Testing.Array1.SetArray(new string[] {
            "123",
            "2345",
            "34567",
        });

        PrintArray(DSM.Testing.Array1);
        PrintArray(DSM.Testing.Array2);

        DSM.Testing.Array2.Value = "Kurt!";

        PrintArray(DSM.Testing.Array2);
    }

    void Start()
    {
        TestBasicOperation();
    }
}
