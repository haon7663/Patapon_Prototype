using System;
using Actor.Unit.Component;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private Camera _mainCamera;
    private Unit _unit;

    [SerializeField] private Image frame;
    [SerializeField] private Image fill;
    [SerializeField] private Vector3 offset;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void Connect(Unit unit)
    {
        _unit = unit;
        _unit.HealthComp.OnDeath += () => Destroy(gameObject);
    }

    public void UpdateUI()
    {
        frame.enabled = true;
        fill.enabled = true;
        
        if (_unit)
            fill.fillAmount = _unit.HealthComp.curHp / _unit.HealthComp.maxHp;
    }
    
    private void LateUpdate()
    {
        if (_unit)
            transform.position = _mainCamera.WorldToScreenPoint(_unit.NormalTrans.position + offset);
    }
}
