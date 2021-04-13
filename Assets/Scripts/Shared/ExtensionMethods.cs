using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
    public static float Remap(this float value, float minIn, float maxIn, float minOut, float maxOut) {
        return minOut + (value - minIn) * (maxOut - minOut) / (maxIn - minIn);
    }
    public static void Shuffle<T>(this IList<T> list) {
        for (int n = list.Count - 1; n > 1; n--) {
            int i = Random.Range(0, list.Count);
            T value = list[i];
            list[i] = list[n];
            list[n] = value;
        }
    }

    public static int Navigate(this int value, int length, bool goRight) {
        if (goRight) {
            value++;
            if (value == length)
                return 0;
        }
        else {
            value--;
            if (value < 0)
                return length - 1;
        }
        return value;
    }
}