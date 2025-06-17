// Editor���� ����� �� �ִ� SkinnedMesh ��Ÿ���� ����
// ������ ������ SkinnedMeshRenderer�� �� ĳ���� �� ������ �ڵ����� ���� �缳����

using UnityEngine;
using UnityEditor;

public class SkinnedMeshRetargetTool : EditorWindow
{
    private SkinnedMeshRenderer sourceSkinnedMesh;
    private Transform targetRootBone;

    [MenuItem("Tools/Skinned Mesh Retarget Tool")]
    public static void ShowWindow()
    {
        GetWindow<SkinnedMeshRetargetTool>("Skinned Mesh Retarget");
    }

    private void OnGUI()
    {
        GUILayout.Label("Skinned Mesh ��Ÿ�� ����", EditorStyles.boldLabel);

        sourceSkinnedMesh = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("���� SkinnedMesh", sourceSkinnedMesh, typeof(SkinnedMeshRenderer), true);
        targetRootBone = (Transform)EditorGUILayout.ObjectField("Ÿ�� ��Ʈ �� (��: Hips)", targetRootBone, typeof(Transform), true);

        if (GUILayout.Button("��Ÿ�� ����") && sourceSkinnedMesh != null && targetRootBone != null)
        {
            RetargetBones();
        }
    }

    private void RetargetBones()
    {
        Transform[] newBones = new Transform[sourceSkinnedMesh.bones.Length];

        for (int i = 0; i < sourceSkinnedMesh.bones.Length; i++)
        {
            var sourceBone = sourceSkinnedMesh.bones[i];

            if (sourceBone == null)
            {
                Debug.LogWarning($"[��Ÿ�� ���] sourceSkinnedMesh.bones[{i}] �� null�Դϴ�.");
                continue;
            }

            string boneName = sourceBone.name;
            Transform found = FindChildRecursive(targetRootBone, boneName);

            if (found == null)
                Debug.LogWarning($"'{boneName}' ���� Ÿ�� ��Ʈ���� ã�� ���߽��ϴ�.");

            newBones[i] = found;
        }
    }

    private Transform FindChildRecursive(Transform parent, string name)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true))
        {
            if (child.name == name)
                return child;
        }
        return null;
    }
}
