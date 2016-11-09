using System.Collections.Generic;
using UnityEngine;

public static class MathUtil {

    public static float Map (float value, float leftMin, float leftMax, float rightMin, float rightMax) {
        var rightSpan = rightMax - rightMin;

        //# Convert the left range into a 0-1 range (float)
        var valueScaled = Normalize (value, leftMin, leftMax);

        //# Convert the 0-1 range into a value in the right range.
        return rightMin + (valueScaled * rightSpan);
    }

    public static float Normalize (float value, float min, float max) {
        //# Figure out how 'wide' each range is
        var span = max - min;

        //# Convert the left range into a 0-1 range (float)
        var valueScaled = (value - min) / span;

        return valueScaled;
    }

    public static float Median (List<float> values) {
        if (0 == values.Count) {
            return 0f;
        }

        values.Sort ();

        if (0 == values.Count % 2) {
            var almostMid = values.Count / 2;
            return (values[almostMid] + values[almostMid + 1]) / 2f;
        }
        else {
            return values[values.Count / 2];
        }
    }

    public static Vector3 Median (List<Vector3> values) {
        if (0 == values.Count) {
            return Vector3.zero;
        }

        var xValues = new List<float>();
        var yValues = new List<float>();
        var zValues = new List<float>();
        foreach (var v in values) {
            xValues.Add (v.x);
            xValues.Add (v.y);
            xValues.Add (v.z);
        }

        var xMedian = Median (xValues);
        var yMedian = Median (yValues);
        var zMedian = Median (zValues);

        return new Vector3 (xMedian, yMedian, zMedian);
    }
}
