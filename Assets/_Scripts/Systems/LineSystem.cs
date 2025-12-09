using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class LineSystem : Singleton<LineSystem>
{
    public Action<Vector3> OnSetStartDrawLine;
    public Action<Vector3> OnSetEndDrawLine;
    
    [SerializeField] private LineRenderer lineView;
    [SerializeField] private Camera lineCamera;

    private Plane _rayPlane;
    private Coroutine _whileMousePressingCoroutine;
    private Coroutine _drawCurveCoroutine;
    
    protected override void Awake()
    {
        base.Awake();
        Setup();
        
        _rayPlane = new Plane(lineCamera.transform.forward, new Vector3(0, 0, ConstValue.LINE_Z));
    }

    private void Start()
    {
        SetVisible(false);
    }

    private void Setup()
    {
        if (lineView == null) transform.AssignChildVar<LineRenderer>("LineView", ref lineView);
        if (lineCamera == null) transform.AssignChildVar<Camera>("LineCamera", ref lineCamera);

        lineView.startWidth = lineView.endWidth = ConstValue.LINE_WIDTH;
    }

    private void OnEnable()
    {
        OnSetStartDrawLine += SetStartPoint;
        OnSetEndDrawLine += SetEndPoint;
        // InputManager.Instance.PlayerActions.Default.MouseLeftButton.started += OnMouseLeftClickDown;
        // InputManager.Instance.PlayerActions.Default.MouseLeftButton.canceled += OnMouseLeftClickUp;
    }
    
    private void OnDisable()
    {
        OnSetStartDrawLine -= SetStartPoint;
        OnSetEndDrawLine -= SetEndPoint;
        
        // InputManager.Instance.PlayerActions.Default.MouseLeftButton.started -= OnMouseLeftClickDown;
        // InputManager.Instance.PlayerActions.Default.MouseLeftButton.canceled -= OnMouseLeftClickUp;
    }

    // private void OnMouseLeftClickDown(InputAction.CallbackContext context)
    // {
    //     SetVisible(true);
    //     SetStartPoint();
    //     _whileMousePressingCoroutine = StartCoroutine(WhilePressingCoroutine());
    // }
    
    private void OnMouseLeftClickUp(InputAction.CallbackContext context)
    {
        SetVisible(false);
        if (_whileMousePressingCoroutine != null) StopCoroutine(_whileMousePressingCoroutine);
    }

    public void SetVisible(bool visible)
    {
        lineView.enabled = visible;
    }

    private void SetStartPoint(Vector3 startPoint)
    {
        lineView.SetPosition(0, startPoint);
    }
    
    private void SetEndPoint(Vector3 endPoint)
    {
        lineView.SetPosition(1, endPoint);
    }
    
    public Vector3 GetLinePointPosOfMousePos()
    {
        Ray ray = lineCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (_rayPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        
        return Vector3.zero;
    }

    public Vector3 GetLinePointPosOfWorldPos(Vector3 worldPos)
    {
        Ray ray = new(lineCamera.transform.position, (worldPos - lineCamera.transform.position).normalized);
        if (_rayPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        
        return default;
    }

    private IEnumerator WhilePressingCoroutine()
    {
        float time = 0;
        
        while (true)
        {
            if (time > 60) yield break;
            
            SetEndPoint(GetLinePointPosOfMousePos());
            
            time += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator DrawCurveCoroutine()
    {
        float time = 0;
        
        while (true)
        {
            if (time > 60) yield break;
            
            SetEndPoint(GetLinePointPosOfMousePos());
            
            Debug.Log("Pressing");
            time += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void StartDrawCurve()
    {
        SetStartPoint(GetLinePointPosOfMousePos());
        SetVisible(true);
        _drawCurveCoroutine = StartCoroutine(DrawCurveCoroutine());
    }

    public void StopDrawCurve()
    {
        SetVisible(false);
        if (_drawCurveCoroutine != null) StopCoroutine(_drawCurveCoroutine);
    }
}