using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor {

    private Transform transform;
    private FieldOfView fow;

    private void OnEnable() {
        fow = target as FieldOfView;
        transform = fow.transform;
    }

    private void OnSceneGUI() {

        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.ViewRadius);
        Vector3 viewAngleA = DirFromAngle(-fow.ViewAngle / 2);
        Vector3 viewAngleB = DirFromAngle(fow.ViewAngle / 2);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.ViewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.ViewRadius);

    }

    public Vector3 DirFromAngle(float angleInDegress) {
        angleInDegress += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegress * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegress * Mathf.Deg2Rad));
    }
}
