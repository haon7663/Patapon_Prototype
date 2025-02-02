using System;
using System.Linq;
using Actor.Unit.Component;
using Actor.Unit.Enums;
using Actor.Unit.Management;
using Cinemachine;
using UnityEngine;

public class UnitGroupFollow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Vector2 baseOffset;

    private void Update()
    {
        var alliances = UnitManager.Units.Where(u => u.alliances == Alliances.Alliance).ToList();
        var enemies = UnitManager.Units.Where(u => u.alliances == Alliances.Enemy).ToList();
        
        if (alliances.Any())
        {
            var allianceMin = alliances.Min(u => u.transform.position.x);
            var allianceMax = alliances.Max(u => u.transform.position.x);
        
            var alliancePos = Vector2.right * (allianceMin + allianceMax) / 2;

            if (enemies.Any() && AllianceActing.commands == UnitCommands.Attack)
            {
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 6f, Time.deltaTime * 5);
                
                var enemyMin = enemies.Min(u => u.transform.position.x);
                var enemyMax = enemies.Max(u => u.transform.position.x);
        
                var enemyPos = Vector2.right * (enemyMin + enemyMax) / 2;

                transform.position = (alliancePos + enemyPos / 2) / 2 + baseOffset;
            }
            else
            {
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 5.2f, Time.deltaTime * 5);
                transform.position = alliancePos + baseOffset;
            }
        }
    }
}
