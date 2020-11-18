using InitOnlyPropertyLib;
using UnityEngine;

public record TestRecord(int Value);

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        var testInstance1 = new TestRecord(42);
        Debug.Log(testInstance1);

        var testInstance2 = testInstance1 with { Value = 24 };
        Debug.Log(testInstance2);

        var testInstance3 = new ClassWithInitOnlyProperty { NormalProperty = 3, InitOnlyProperty = 2 };
        Debug.Log(testInstance3);
    }
}

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}