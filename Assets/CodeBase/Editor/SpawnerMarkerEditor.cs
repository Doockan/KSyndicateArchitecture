using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(SpawnMarker))]
  public class SpawnerMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
    {
      SphereGizmo(spawner.transform, 0.5f, Color.red);
    }

    private static void SphereGizmo(Transform transform, float radios, Color color)
    {
      Gizmos.color = color;

      var pos = transform.position;

      Gizmos.DrawSphere(pos, radios);
    }
  }
}