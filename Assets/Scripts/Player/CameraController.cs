using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {    // Controla a camera
    [Header("Camera Controller")]
    [SerializeField] private float minCamRotation = -30f;
    [SerializeField] private float maxCamRotation = 30f;

    [Header(" - Config")]
    [SerializeField] private Transform followPoint;
    [SerializeField] private GameObject aimReticle;

    [Header(" - Cameras")]
    [SerializeField] private GameObject standardCamera;
    [SerializeField] private GameObject aimCamera;
    [SerializeField] private GameObject sprintCamera;
    [SerializeField] private GameObject deathCamera;

    private float xRotation = 0f;
    public void UpdateCameraRotation(float lookY) {
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, minCamRotation, maxCamRotation);
        followPoint.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void SetAim(bool active) {
        standardCamera.SetActive(!active);
        aimCamera.SetActive(active);

        if (active) {
            StartCoroutine(ShowReticle());
        }
        else {
            StopAllCoroutines();
            aimReticle.SetActive(false);
        }
    }
    public void SetSprint(bool active) {
        standardCamera.SetActive(!active);
        sprintCamera.SetActive(active);
    }

    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(enabled);
    }

    public void PlayerDeath() {
        deathCamera.SetActive(true);
    }
}
