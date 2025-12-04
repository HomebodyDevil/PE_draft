using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Canvas uiCanvas;
    
    protected override void Awake()
    {
        base.Awake();

        VarSetup();
        Setup();
    }

    private void VarSetup()
    {
        if (uiCanvas == null) transform.AssignChildVar<Canvas>("UICanvas", ref uiCanvas);
    }

    private void Setup()
    {
        if (uiCanvas != null)
        {
            uiCanvas.sortingOrder = ConstValue.UI_ORDER;
        }
    }
}
