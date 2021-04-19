using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*

[CustomEditor(typeof(AIController))]
public class AIMovementEditor : Editor {

    AIController aiController;
    Transform transform;

    private const float visualizeRadius = 2;
    private void OnEnable() {
        aiController = target as AIController;
        transform = aiController.transform;
    }
    private void OnSceneGUI() {


        if (aiController.EyePoint) {
            Handles.color = new Color(1, 1, 1, .1f);
            //Vector3 direction = Quaternion.Euler(0, -aiController.ViewAngle / 2, 0) * transform.forward;
            Vector3 direction = Quaternion.AngleAxis(-aiController.ViewAngle / 2, Vector3.up) * aiController.EyePoint.forward;
            Handles.DrawSolidArc(aiController.EyePoint.position, aiController.EyePoint.up, direction, aiController.ViewAngle, aiController.ViewRadius);
        }
        if (aiController.Target)
            DrawIntencity();

    }

    private void DrawIntencity() {
        Handles.color = Color.white;
        Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, visualizeRadius);
        
        Handles.color = Color.yellow;
        Handles.DrawLine(transform.position, PlaceOnRadius(aiController.Target.position));
        Handles.color = Color.green;


        //Vector3[] intencities = aiController.Intensity();
        //Vector3 maxIntencity = Vector3.right;
        //foreach (Vector3 intencity in intencities) {
        //    if (maxIntencity.magnitude < intencity.magnitude) {
        //        maxIntencity = intencity;
        //        Debug.Log(intencity.magnitude);

        //    }
        //}

        //float magnitude = maxIntencity.magnitude;
        //for (int i = 0; i < intencities.Length; i++) {
        //    intencities[i] /= magnitude;
        //}
        //foreach (Vector3 intencity in intencities) {
        //    Handles.DrawLine(transform.position, transform.position + (intencity * visualizeRadius));
        //}
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

    //private void DrawCone(Transform pivot, float angle, float radius) {
    //    Vector3 pivotPosition = pivot.position;
    //    Handles.DrawLine(pivotPosition, PlaceOnCircle(pivot, -angle, radius));
    //    Handles.DrawLine(pivotPosition, PlaceOnCircle(pivot, angle, radius));

    //    Vector3 lastPoint = PlaceOnCircle(pivot, -angle, radius);
    //    Vector3 newPoint;
    //    float step = 10f;
    //    for (float i = -angle + step; i < angle; i += step) {
    //        newPoint = PlaceOnCircle(pivot, i, radius);
    //        Handles.DrawLine(lastPoint, newPoint);
    //        lastPoint = newPoint;
    //    }
    //    Handles.DrawLine(lastPoint, PlaceOnCircle(pivot, angle, radius));
    //}

    //private Vector3 PlaceOnCircle(Transform pivotPosition, float angle, float radius) {
    //    angle /= 2;
    //    angle += pivotPosition.eulerAngles.y;
    //    Vector3 dirFromAngle = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));

    //    return pivotPosition.position + dirFromAngle * radius;
    //}
}
*/