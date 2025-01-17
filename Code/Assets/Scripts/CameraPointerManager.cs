//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;


using UnityEngine;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistancePointer = 4.5f;
    [Range(0,1)]
    [SerializeField] private float disPointerObject = 0.95f;
    private readonly string interactableTag = "Interactable";
    private float scaleSize = 0.025f;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;

    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection;
    }

    private void GazeSelection()
    {
        _gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver);
    }
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {

                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter", null, SendMessageOptions.DontRequireReceiver);
                GazeManager.Instance.StartGazeSelection();
            }
            if(hit.transform.CompareTag(interactableTag)){
                PointerOnGaze(hit.point);
            }
            else{
                PointerOutGaze();
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver);
        }
    }
        private void PointerOnGaze(Vector3 hitPoint){
            float scaleFactor = scaleSize*Vector3.Distance(transform.position, hitPoint);
            pointer.transform.localScale = Vector3.one * scaleFactor;
            pointer.transform.parent.position = CalculatePointerPosition(transform.position, hitPoint, disPointerObject);
        }

        private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t){
            float x = p0.x + t * (p1.x - p0.x);
            float y = p0.y + t * (p1.y - p0.y);
            float z = p0.z + t * (p1.z - p0.z);

            return new Vector3(x, y, z);
        }

        private void PointerOutGaze(){
            pointer.transform.localScale = Vector3.one * 0.1f;
            pointer.transform.parent.transform.localPosition = new Vector3(0, 0, maxDistancePointer);
            pointer.transform.parent.transform.rotation = transform.rotation;
            GazeManager.Instance.CancelGazeSelection();
        }
    }
