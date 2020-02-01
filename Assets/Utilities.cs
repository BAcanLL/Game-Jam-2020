using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector3 convertRotationToDirection(Vector3 rotation)
    {
        return Quaternion.Euler(rotation) * Vector3.up;
    }

    public static Vector3 convertRotationToDirection(Quaternion rotation)
    {
        return rotation * Vector3.up;
    }
}
