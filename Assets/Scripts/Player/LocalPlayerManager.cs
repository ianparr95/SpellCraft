﻿using UnityEngine;
using HoloToolkit.Unity;

/// <summary>
/// Manages local player state.
/// </summary>
public class LocalPlayerManager : Singleton<LocalPlayerManager>
{

    // Send the user's position each frame.
    void Update()
    {
        if (ImportExportAnchorManager.Instance.AnchorEstablished)
        {
            // Grab the current head transform and broadcast it to all the other users in the session
            Transform headTransform = Camera.main.transform;

            Vector3 headPosition = Camera.main.transform.position;
            Quaternion headRotation = headTransform.rotation;
            // Transform the head position and rotation into local space
            //Vector3 headPosition = this.transform.InverseTransformPoint(headTransform.position);
            //Quaternion headRotation = Quaternion.Inverse(this.transform.rotation) * headTransform.rotation;
            int playerHealth = Player.Instance.getHealth();

            Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
            CustomMessages.Instance.SendHeadTransform(anchor.InverseTransformPoint(headPosition), 
                                                      headRotation, 0x1);
            CustomMessages.Instance.UpdatePlayerHealth(playerHealth);
        }
    }
}