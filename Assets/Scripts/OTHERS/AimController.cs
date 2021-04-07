using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {    // Controla a camera
    [Header("Camera Controller")]
    [SerializeField] private float minCamRotation = -30f;
    [SerializeField] private float maxCamRotation = 30f;

    [Header(" - Config")]
    [SerializeField] private Transform followPoint;
    [SerializeField] private GameObject aimReticle;
    [Space]
    [SerializeField] private GameObject standardCamera;
    [SerializeField] private GameObject aimCamera;

    private float xRotation = 0f;
    public void UpdateCameraRotation(float lookY) {
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, minCamRotation, maxCamRotation);
        followPoint.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void OnAim(bool active) {
        standardCamera.SetActive(!active);
        aimCamera.SetActive(active);

        if (active) {
            StartCoroutine(ShowReticle());
        }
        else {
            aimReticle.SetActive(false);
        }
    }

    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(enabled);
    }
}
