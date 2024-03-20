using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class StoreDataAttribute : Attribute
{
    public string VariableName { get; }

    public StoreDataAttribute([CallerMemberName] string variableName = "")
    {
        VariableName = variableName;
    }
}
