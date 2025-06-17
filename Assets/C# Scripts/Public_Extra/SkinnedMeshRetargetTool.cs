// Editor에서 사용할 수 있는 SkinnedMesh 리타겟팅 도구
// 구매한 에셋의 SkinnedMeshRenderer를 내 캐릭터 본 구조에 자동으로 맞춰 재설정함

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
        GUILayout.Label("Skinned Mesh 리타겟 도구", EditorStyles.boldLabel);

        sourceSkinnedMesh = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("원본 SkinnedMesh", sourceSkinnedMesh, typeof(SkinnedMeshRenderer), true);
        targetRootBone = (Transform)EditorGUILayout.ObjectField("타겟 루트 본 (예: Hips)", targetRootBone, typeof(Transform), true);

        if (GUILayout.Button("리타겟 실행") && sourceSkinnedMesh != null && targetRootBone != null)
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
                Debug.LogWarning($"[리타겟 경고] sourceSkinnedMesh.bones[{i}] 가 null입니다.");
                continue;
            }

            string boneName = sourceBone.name;
            Transform found = FindChildRecursive(targetRootBone, boneName);

            if (found == null)
                Debug.LogWarning($"'{boneName}' 본을 타겟 루트에서 찾지 못했습니다.");

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
