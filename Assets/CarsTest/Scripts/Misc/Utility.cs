using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Vector3 OrthoNormalize(Vector3 vector1, Vector3 vector2)
    {
        vector1.Normalize();
        Vector3 temp = Vector3.Cross(vector1, vector2);
        temp.Normalize();
        vector2 = Vector3.Cross(temp, vector2);
        return vector2;
    }
 
 
    public static void AddTorqueAtPosition(this Rigidbody thisRigidbody, Vector3 torque, Vector3 position)
    {
        Vector3 torqueAxis = torque.normalized;
        Vector3 ortho = new Vector3(1, 0, 0);
        if ((torqueAxis - ortho).sqrMagnitude < float.Epsilon)
        {
            ortho = new Vector3(0, 1, 0);
        }
         
        var orthoNorm = OrthoNormalize(torqueAxis, ortho);
        Vector3 force = Vector3.Cross( 0.5f * torque , orthoNorm);
        thisRigidbody.AddForceAtPosition(force, position + orthoNorm);
        thisRigidbody.AddForceAtPosition(-force, position - orthoNorm);
         
    }
}
