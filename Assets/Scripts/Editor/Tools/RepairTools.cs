using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using AdventureGame.Data;
using Object = UnityEngine.Object;
using Sirenix.OdinInspector;

namespace AdventureGame.Editor
{
    [InlineProperty, HideLabel, HideReferenceObjectPicker]
    public class RepairTools
    {
        #region Missing Script
        [Button, Title("Auto Repair")]
        private void FindAndRemoveMissingInSelected()
        {
            // EditorUtility.CollectDeepHierarchy does not include inactive children
            var deeperSelection = Selection.gameObjects.SelectMany(go => go.GetComponentsInChildren<Transform>(true)).Select(t => t.gameObject);
            var prefabs = new HashSet<Object>();
            int compCount = 0;
            int goCount = 0;

            foreach (var go in deeperSelection)
            {
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);

                if (count > 0)
                {
                    if (PrefabUtility.IsPartOfAnyPrefab(go))
                    {
                        RecursivePrefabSource(go, prefabs, ref compCount, ref goCount);
                        count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);

                        if (count == 0)
                            continue;
                    }

                    Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                    compCount += count;
                    goCount++;
                }
            }

            Debug.Log($"Found and removed {compCount} missing scripts from {goCount} GameObjects");
        }

        private void RecursivePrefabSource(GameObject instance, HashSet<Object> prefabs, ref int compCount, ref int goCount)
        {
            var source = PrefabUtility.GetCorrespondingObjectFromSource(instance);

            if (source == null || !prefabs.Add(source))
                return;

            RecursivePrefabSource(source, prefabs, ref compCount, ref goCount);
            int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(source);

            if (count > 0)
            {
                Undo.RegisterCompleteObjectUndo(source, "Remove missing scripts");
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(source);
                compCount += count;
                goCount++;
            }
        }
        #endregion

        #region Animator Override
        [Title("Animator Override Repair"), PropertyOrder(-2f)]
        public AnimationClip clipToReplace;

        [Button, PropertyOrder(-2f)]
        private void ReplaceEmptyClipsFromSelection()
        {
            foreach (Object obj in Selection.objects)
            {
                if (obj is AnimatorOverrideController animator)
                {
                    List<KeyValuePair<AnimationClip, AnimationClip>> clips = new List<KeyValuePair<AnimationClip, AnimationClip>>();
                    animator.GetOverrides(clips);

                    int repaired = 0;
                    for (int i = 0; i < clips.Count; i++)
                    {
                        if (clips[i].Value == null)
                        {
                            KeyValuePair<AnimationClip, AnimationClip> newPair = new KeyValuePair<AnimationClip, AnimationClip>(clips[i].Key, clipToReplace);
                            clips[i] = newPair;
                            repaired++;
                        }
                    }

                    if (repaired > 0)
                    {
                        animator.ApplyOverrides(clips);
                        Debug.Log($"Were repaired in '{animator.name}' animator {repaired} clips.");
                    }
                }
                else
                {
                    Debug.LogError($"The selected object named '{obj.name}' is not an Animator Override Controller");
                }
            }
        }
        #endregion
    }
}