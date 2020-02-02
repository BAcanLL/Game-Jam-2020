using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector3 ConvertRotationToDirection(Vector3 rotation)
    {
        return Quaternion.Euler(rotation) * Vector3.down;
    }

    public static Vector3 ConvertRotationToDirection(Quaternion rotation)
    {
        return rotation * Vector3.down;
    }
}
