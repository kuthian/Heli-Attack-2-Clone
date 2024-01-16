using UnityEngine;

static class Utils
{
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public static T RandomInRange<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static bool RandomBool()
    {
        return Random.Range(0, 2) == 1;
    }

    // This function returns true based on a given percentage chance.
    // The chance is provided as an integer from 0 to 100.
    public static bool Chance(int percentage)
    {
        // Generate a random number between 0 and 100
        int randomNumber = Random.Range(0, 101);

        // Check if the random number is less than or equal to the given chance
        return randomNumber <= percentage;
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