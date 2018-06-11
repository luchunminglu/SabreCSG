﻿#if UNITY_EDITOR || RUNTIME_CSG

using System;
using System.Linq;
using UnityEngine;

namespace Sabresaurus.SabreCSG
{
    /// <summary>
    /// Applies forces to rigid bodies inside of the volume.
    /// </summary>
    /// <seealso cref="Sabresaurus.SabreCSG.Volume"/>
    [Serializable]
    public class PhysicsVolume : Volume
    {
        /// <summary>
        /// The force mode applied to rigid bodies.
        /// </summary>
        [SerializeField]
        public PhysicsVolumeForceMode forceMode = PhysicsVolumeForceMode.Force;

        /// <summary>
        /// The force applied to rigid bodies.
        /// </summary>
        [SerializeField]
        public Vector3 force = new Vector3(0.0f, 10.0f, 0.0f);

        /// <summary>
        /// The relative force mode applied to rigid bodies.
        /// </summary>
        [SerializeField]
        public PhysicsVolumeForceMode relativeForceMode = PhysicsVolumeForceMode.None;

        /// <summary>
        /// The relative force applied to rigid bodies.
        /// </summary>
        [SerializeField]
        public Vector3 relativeForce = new Vector3(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// The torque force mode applied to rigid bodies.
        /// </summary>
        [SerializeField]
        public PhysicsVolumeForceMode torqueForceMode = PhysicsVolumeForceMode.None;

        /// <summary>
        /// The torque applied to rigid bodies.
        /// </summary>
        [SerializeField]
        public Vector3 torque = new Vector3(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// The relative torque force mode applied to rigid bodies.
        /// </summary>
        [SerializeField]
        public PhysicsVolumeForceMode relativeTorqueForceMode = PhysicsVolumeForceMode.None;

        /// <summary>
        /// The relative torque applied to rigid bodies.
        /// </summary>
        [SerializeField]
        public Vector3 relativeTorque = new Vector3(0.0f, 0.0f, 0.0f);

#if UNITY_EDITOR

        /// <summary>
        /// Called when the inspector GUI is drawn in the editor.
        /// </summary>
        /// <param name="selectedVolumes">The selected volumes in the editor (for multi-editing).</param>
        /// <returns>True if a property changed or else false.</returns>
        public override bool OnInspectorGUI(Volume[] selectedVolumes)
        {
            var physicsVolumes = selectedVolumes.Cast<PhysicsVolume>();
            bool invalidate = false;

            // global force:

            GUILayout.BeginVertical("Box");
            {
                UnityEditor.EditorGUILayout.LabelField("Force Options", UnityEditor.EditorStyles.boldLabel);
                GUILayout.Space(4);

                UnityEditor.EditorGUI.indentLevel = 1;
                GUILayout.BeginVertical();
                {
                    PhysicsVolumeForceMode previousPhysicsVolumeForceMode;
                    forceMode = (PhysicsVolumeForceMode)UnityEditor.EditorGUILayout.EnumPopup(new GUIContent("Force Mode", "The force mode."), previousPhysicsVolumeForceMode = forceMode);
                    if (previousPhysicsVolumeForceMode != forceMode)
                    {
                        foreach (PhysicsVolume volume in physicsVolumes)
                            volume.forceMode = forceMode;
                        invalidate = true;
                    }

                    Vector3 previousVector3;
                    UnityEditor.EditorGUIUtility.wideMode = true;
                    force = UnityEditor.EditorGUILayout.Vector3Field(new GUIContent("Force", "The amount of force."), previousVector3 = force);
                    UnityEditor.EditorGUIUtility.wideMode = false;
                    if (previousVector3 != force)
                    {
                        foreach (PhysicsVolume volume in physicsVolumes)
                            volume.force = force;
                        invalidate = true;
                    }
                }
                GUILayout.EndVertical();
                UnityEditor.EditorGUI.indentLevel = 0;
            }
            GUILayout.EndVertical();

            // relative force:

            GUILayout.BeginVertical("Box");
            {
                UnityEditor.EditorGUILayout.LabelField("Relative Force Options", UnityEditor.EditorStyles.boldLabel);
                GUILayout.Space(4);

                UnityEditor.EditorGUI.indentLevel = 1;
                GUILayout.BeginVertical();
                {
                    PhysicsVolumeForceMode previousPhysicsVolumeRelativeForceMode;
                    relativeForceMode = (PhysicsVolumeForceMode)UnityEditor.EditorGUILayout.EnumPopup(new GUIContent("Force Mode", "The relative force mode."), previousPhysicsVolumeRelativeForceMode = relativeForceMode);
                    if (previousPhysicsVolumeRelativeForceMode != relativeForceMode)
                    {
                        foreach (PhysicsVolume volume in physicsVolumes)
                            volume.relativeForceMode = relativeForceMode;
                        invalidate = true;
                    }

                    Vector3 previousVector3;
                    UnityEditor.EditorGUIUtility.wideMode = true;
                    relativeForce = UnityEditor.EditorGUILayout.Vector3Field(new GUIContent("Force", "The amount of relative force."), previousVector3 = relativeForce);
                    UnityEditor.EditorGUIUtility.wideMode = false;
                    if (previousVector3 != relativeForce)
                    {
                        foreach (PhysicsVolume volume in physicsVolumes)
                            volume.relativeForce = relativeForce;
                        invalidate = true;
                    }
                }
                GUILayout.EndVertical();
                UnityEditor.EditorGUI.indentLevel = 0;
            }
            GUILayout.EndVertical();

            // global torque:

            GUILayout.BeginVertical("Box");
            {
                UnityEditor.EditorGUILayout.LabelField("Torque Options", UnityEditor.EditorStyles.boldLabel);
                GUILayout.Space(4);

                UnityEditor.EditorGUI.indentLevel = 1;
                GUILayout.BeginVertical();
                {
                    PhysicsVolumeForceMode previousPhysicsVolumeTorqueForceMode;
                    torqueForceMode = (PhysicsVolumeForceMode)UnityEditor.EditorGUILayout.EnumPopup(new GUIContent("Force Mode", "The torque force mode."), previousPhysicsVolumeTorqueForceMode = torqueForceMode);
                    if (previousPhysicsVolumeTorqueForceMode != torqueForceMode)
                    {
                        foreach (PhysicsVolume volume in physicsVolumes)
                            volume.torqueForceMode = torqueForceMode;
                        invalidate = true;
                    }

                    Vector3 previousVector3;
                    UnityEditor.EditorGUIUtility.wideMode = true;
                    torque = UnityEditor.EditorGUILayout.Vector3Field(new GUIContent("Force", "The amount of torque force."), previousVector3 = torque);
                    UnityEditor.EditorGUIUtility.wideMode = false;
                    if (previousVector3 != torque)
                    {
                        foreach (PhysicsVolume volume in physicsVolumes)
                            volume.torque = torque;
                        invalidate = true;
                    }
                }
                GUILayout.EndVertical();
                UnityEditor.EditorGUI.indentLevel = 0;
            }
            GUILayout.EndVertical();

            // relative torque:

            GUILayout.BeginVertical("Box");
            {
                UnityEditor.EditorGUILayout.LabelField("Relative Torque Options", UnityEditor.EditorStyles.boldLabel);
                GUILayout.Space(4);

                UnityEditor.EditorGUI.indentLevel = 1;
                GUILayout.BeginVertical();
                {
                    PhysicsVolumeForceMode previousPhysicsVolumeRelativeTorqueForceMode;
                    relativeTorqueForceMode = (PhysicsVolumeForceMode)UnityEditor.EditorGUILayout.EnumPopup(new GUIContent("Force Mode", "The relative torque force mode."), previousPhysicsVolumeRelativeTorqueForceMode = relativeTorqueForceMode);
                    if (previousPhysicsVolumeRelativeTorqueForceMode != relativeTorqueForceMode)
                    {
                        foreach (PhysicsVolume volume in physicsVolumes)
                            volume.relativeTorqueForceMode = relativeTorqueForceMode;
                        invalidate = true;
                    }

                    Vector3 previousVector3;
                    UnityEditor.EditorGUIUtility.wideMode = true;
                    relativeTorque = UnityEditor.EditorGUILayout.Vector3Field(new GUIContent("Force", "The amount of relative torque force."), previousVector3 = relativeTorque);
                    UnityEditor.EditorGUIUtility.wideMode = false;
                    if (previousVector3 != relativeTorque)
                    {
                        foreach (PhysicsVolume volume in physicsVolumes)
                            volume.relativeTorque = relativeTorque;
                        invalidate = true;
                    }
                }
                GUILayout.EndVertical();
                UnityEditor.EditorGUI.indentLevel = 0;
            }
            GUILayout.EndVertical();

            return invalidate; // true when a property changed, the brush invalidates and stores all changes.
        }

#endif

        /// <summary>
        /// Called when the volume is created in the editor.
        /// </summary>
        /// <param name="volume">The generated volume game object.</param>
        public override void OnCreateVolume(GameObject volume)
        {
            PhysicsVolumeComponent component = volume.AddComponent<PhysicsVolumeComponent>();
            component.forceMode = forceMode;
            component.force = force;
            component.relativeForceMode = relativeForceMode;
            component.relativeForce = relativeForce;
            component.torqueForceMode = torqueForceMode;
            component.torque = torque;
            component.relativeTorqueForceMode = relativeTorqueForceMode;
            component.relativeTorque = relativeTorque;
        }
    }
}

#endif