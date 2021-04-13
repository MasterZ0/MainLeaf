using UnityEngine;

public static class UtilityFunctions {
    

    /// <summary>
    /// Calculation: (1 - <paramref name="t"/>) <paramref name="p0"/> + <paramref name="t"/> * <paramref name="p1"/>
    /// </summary>
    public static float LineBezier(float t, float p0, float p1) {
        float u = 1 - t;
        // transition -= Time.deltaTime / animationDuration;
        return (u * p0) + (t * p1);
    }
    /// <summary>
    /// Calculation: (1 - <paramref name="t"/>) <paramref name="p0"/> + <paramref name="t"/> * <paramref name="p1"/>
    /// </summary>
    public static Vector3 LineBezier(float t, Vector3 p0, Vector3 p1) {
        float u = 1 - t;

        return (u * p0) + (t * p1);
    }
    public static float SquareBezier(float t, float p0, float p1, float p2) {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }

    /// <summary>
    /// Calculation: (1 - <paramref name="t"/>)² <paramref name="p0"/> + 2<paramref name="t"/>(1 - <paramref name="t"/>) <paramref name="p1"/> + <paramref name="t"/>² <paramref name="p2"/>
    /// </summary>
    public static Vector3 SquareBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }
    /// <summary>
    /// Calculation: (1 - <paramref name="t"/>)³ <paramref name="p0"/> + 3<paramref name="t"/>(1 - <paramref name="t"/>)² <paramref name="p1"/> + 3<paramref name="t"/>²(1 - <paramref name="t"/>) <paramref name="p2"/>  + <paramref name="t"/>³ <paramref name="p3"/>
    /// </summary>
    public static float CubicBezierPoint(float t, float p0, float p1, float p2, float p3) {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float ttt = tt * t;
        float uuu = uu * u;

        return (uuu * p0) + (3 * t * uu * p1) + (3 * tt * u * p2) + (ttt * p3);
    }

    /// <summary>
    /// Calculation: (1 - <paramref name="t"/>)³ <paramref name="p0"/> + 3<paramref name="t"/>(1 - <paramref name="t"/>)² <paramref name="p1"/> + 3<paramref name="t"/>²(1 - <paramref name="t"/>) <paramref name="p2"/>  + <paramref name="t"/>³ <paramref name="p3"/>
    /// </summary>
    public static Vector3 CubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float ttt = tt * t;
        float uuu = uu * u;

        return (uuu * p0) + (3 * t * uu * p1) + (3 * tt * u * p2) + (ttt * p3);
    }
    
}
