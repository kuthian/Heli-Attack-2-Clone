using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

static class Utils
{ 

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public static T RandomInRange<T>( T[] array )
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    // Function to draw a square
    public static void DrawSquare(Vector3 center, float width)
    {
        // Half of the width to calculate corners
        float halfWidth = width / 2;

        // Calculate the corners of the square
        Vector3 topLeft = center + new Vector3(-halfWidth, halfWidth, 0);
        Vector3 topRight = center + new Vector3(halfWidth, halfWidth, 0);
        Vector3 bottomLeft = center + new Vector3(-halfWidth, -halfWidth, 0);
        Vector3 bottomRight = center + new Vector3(halfWidth, -halfWidth, 0);

        // Draw the lines between the corners
        Debug.DrawLine(topLeft, topRight, Color.red, duration: 0f, depthTest: false);
        Debug.DrawLine(topRight, bottomRight, Color.red, duration: 0f, depthTest: false);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red, duration: 0f, depthTest: false);
        Debug.DrawLine(bottomLeft, topLeft, Color.red, duration: 0f, depthTest: false);
    }

};