using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(FieldOfView))]
public class AIMovementEditor : Editor {

    Transform transform;
    FieldOfView fieldOfView;

    private const float visualizeRadius = 2;
    private void OnEnable() {
        fieldOfView = target as FieldOfView;
        transform = fieldOfView.transform;
    }
    private void OnSceneGUI() {
        // Block from radius -> 32 = .96875f, 16 = .9375f, 8 = .875f

        Handles.color = Color.white;
        Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, visualizeRadius);
        if (fieldOfView.Target == null)
            return;

        Handles.color = Color.yellow;
        Handles.DrawLine(transform.position, PlaceOnRadius(fieldOfView.Target.position));
        Handles.color = Color.green;


        Vector3[] intencities = fieldOfView.Intensity();
        Vector3 maxIntencity = Vector3.right;
        foreach (Vector3 intencity in intencities) {
            if (maxIntencity.magnitude < intencity.magnitude) {
                maxIntencity = intencity;
                Debug.Log(intencity.magnitude);

            }
        }

        float magnitude = maxIntencity.magnitude;
        for (int i = 0; i < intencities.Length; i++) {
            intencities[i] /= magnitude;
        }
        foreach (Vector3 intencity in intencities) {
            //Handles.color = block[i] ? Color.red : Color.green;
            Handles.DrawLine(transform.position, transform.position + (intencity * visualizeRadius));
        }

    }
    private Vector3 PlaceOnRadius(Vector3 pos) {
        Vector3 aux = (pos - transform.position).normalized * visualizeRadius;

        if (Mathf.Abs(pos.x) > Mathf.Abs(aux.x)) {
            pos.x = aux.x;
        }
        if (Mathf.Abs(pos.z) > Mathf.Abs(aux.z)) {
            pos.z = aux.z;
        }

        return transform.position + aux;
    }
}
