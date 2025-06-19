using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using static UnityEditor.SceneView;


public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private GameObject player;

    [SerializeField] private float distance = 5f;
    [SerializeField] private float rotateSpeed = 120f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 10f;


    private Camera mainCam;
    private int defaultCullingMask;

    private Action onFocusComplete;


    private float yaw = 0f;
    private float pitch = 20f;

    private Vector3 targetPotision;
    private Quaternion targetQuaternion;

    private enum eCAMERAMODE { FREE, FOCUS_NPC }
    private eCAMERAMODE currentMode = eCAMERAMODE.FREE;

    private void Awake()
    {
        mainCam = Camera.main;
        defaultCullingMask = mainCam.cullingMask;
        if (Instance == null) Instance = this;
    }

    private void LateUpdate()
    {
        switch (currentMode)
        {
            case eCAMERAMODE.FREE:
                HandleFreeCamera();
                break;
            case eCAMERAMODE.FOCUS_NPC:
                HandleNPCFocus();
                break;  
        }
    }

    private void HandleFreeCamera()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            yaw += mouseDelta.x * rotateSpeed * Time.deltaTime * 10f;
            pitch -= mouseDelta.y * rotateSpeed * Time.deltaTime * 10f;
            pitch = Mathf.Clamp(pitch, 10f, 80f);
        }

        //  마우스 휠 줌
        float scrollDelta = Mouse.current.scroll.ReadValue().y;
        distance -= scrollDelta * zoomSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, minZoom, maxZoom);

        // 카메라 위치 및 회전
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position + Vector3.up * 1.5f);
    }

    private void HandleNPCFocus()
    {
        transform.position = Vector3.Lerp(transform.position, targetPotision, Time.deltaTime * 5f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime * 5f);

        if (Vector3.Distance(transform.position, targetPotision) < 0.05f &&  Quaternion.Angle(transform.rotation, targetQuaternion) < 1f)
        {
            if (onFocusComplete != null)
            {
                onFocusComplete.Invoke();
                onFocusComplete = null;
            }
        }
    }

    /// <summary>
    /// NPC 상호작용 시 카메라를 앵커 위치로 이동시킴
    /// </summary>
    public void FocusOn(Transform anchor, Action onComplete = null)
    {
        GameManager.Instance.uiManager.SetInGameUIVisible(false);
        targetPotision = anchor.position;
        targetQuaternion = anchor.rotation;
        currentMode = eCAMERAMODE.FOCUS_NPC;
        onFocusComplete = onComplete;

        // 플레이어 레이어 제외
        mainCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
    }

    /// <summary>
    /// 상호작용 종료 시 플레이어 중심 자유 시점으로 복귀
    /// </summary>
    public void ReturnToPlayer()
    {
        GameManager.Instance.uiManager.SetInGameUIVisible(true);
        currentMode = eCAMERAMODE.FREE;
        // 플레이어 레이어 다시 포함
        mainCam.cullingMask = defaultCullingMask;
    }
}