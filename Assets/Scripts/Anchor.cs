using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Management;
using MagicLeap.OpenXR.Subsystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpatialAnchorsTest : MonoBehaviour
{
    // Reference to the ARAnchorManager component, used for anchor creation and management.
    [SerializeField]
    private ARAnchorManager anchorManager;

    // Reference to the XRController that will be used to position the Anchors
    [SerializeField]
    private Transform controllerTransform;

    // Input actions for capturing position, rotation, menu interaction, and the bumper press.
    [SerializeField]
    public InputAction menuInputAction = new InputAction(binding: "<XRController>/menuButton", expectedControlType: "Button");
    [SerializeField]
    public InputAction bumperInputAction = new InputAction(binding: "<XRController>/gripButton", expectedControlType: "Button");

    // Active subsystem used for querying anchor confidence.
    private MLXrAnchorSubsystem activeSubsystem;

    // List of active anchors tracked by the script.
    private List<ARAnchor> activeAnchors = new List<ARAnchor>();

    // Reference to the ErrorLogger for displaying errors
    private ErrorLogger errorLogger;

    [SerializeField]
    private GameObject[] modelPrefabs;  // Array of model prefabs to choose from

    [SerializeField] 
    private TMP_Dropdown modelDropdown;

    // Coroutine started on MonoBehaviour Start to ensure subsystems are loaded before enabling input actions.
    private IEnumerator Start()
    {
        // Finding the ErrorLogger component in the scene
        errorLogger = FindObjectOfType<ErrorLogger>();

        // Waits until AR subsystems are loaded before proceeding.
        yield return new WaitUntil(AreSubsystemsLoaded);

        // Enabling input actions.
        menuInputAction.Enable();
        bumperInputAction.Enable();

        // Registering input action callbacks.
        menuInputAction.performed += OnMenu;
        bumperInputAction.performed += OnBumper;
    }

    // Cleanup of input actions when the GameObject is destroyed.
    void OnDestroy()
    {
        menuInputAction.Dispose();
        bumperInputAction.Dispose();
    }

    // Update loop to log the status of active anchors every frame.
    void Update()
    {
        string anchorsStatusText = "Anchor Position | Tracking State | Confidence";

        // Iterating through active anchors to log their position, tracking state, and confidence.
        foreach (ARAnchor anchor in activeAnchors)
        {
            MLXrAnchorSubsystem.AnchorConfidence anchorCon = MLXrAnchorSubsystem.AnchorConfidence.NotFound;
            if (anchor.trackingState == TrackingState.Tracking && activeSubsystem != null)
            {
                anchorCon = activeSubsystem.GetAnchorConfidence(anchor);
            }
            anchorsStatusText += $"{anchor.gameObject.transform.position} - {anchor.trackingState} - {anchorCon}\n";
        }

        // Only log the status of anchors if there is at least 1 active anchor.
        if (activeAnchors.Count > 0)
        {
            Debug.Log(anchorsStatusText);
            //errorLogger?.DisplayError(anchorsStatusText);
        }
    }

    // Checks if the Magic Leap Anchor Subsystem is loaded 
    private bool AreSubsystemsLoaded()
    {
        if (XRGeneralSettings.Instance == null || XRGeneralSettings.Instance.Manager == null || XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            string errorMsg = "Failed to load AR subsystems.";
            Debug.LogError(errorMsg);
            errorLogger?.DisplayError(errorMsg);
            return false;
        }
        activeSubsystem = XRGeneralSettings.Instance.Manager.activeLoader.GetLoadedSubsystem<XRAnchorSubsystem>() as MLXrAnchorSubsystem;
        if (activeSubsystem == null)
        {
            string errorMsg = "Magic Leap Anchor Subsystem is not available.";
            Debug.LogError(errorMsg);
            errorLogger?.DisplayError(errorMsg);
            return false;
        }
        return true;
    }

    private void OnBumper(InputAction.CallbackContext obj)
    {
        Pose currentPose = new Pose(controllerTransform.position, controllerTransform.rotation);

        Debug.Log("SpatialAnchorsTest: Bumper hit, creating Anchor at " + currentPose);

        // Instantiating a new anchor at the current pose and adding it to the list of active anchors.
        GameObject newAnchor = Instantiate(anchorManager.anchorPrefab, currentPose.position, currentPose.rotation);
        ARAnchor newAnchorComponent = newAnchor.GetComponent<ARAnchor>();
        if (newAnchorComponent == null)
        {
            newAnchorComponent = newAnchor.AddComponent<ARAnchor>();
        }
        activeAnchors.Add(newAnchorComponent);

        errorLogger?.DisplayError("SpatialAnchorsTest: Bumper hit, creating Anchor at " + currentPose);

        if (activeAnchors.Count == 4)
        {
            SpawnModelOnPlane();
        }
    }

    private void SpawnModelOnPlane()
    {
        if (activeAnchors.Count == 4)
        {
            Vector3 averagePosition = CalculateAveragePosition();
            int selectedModelIndex = modelDropdown.value;  // Get selected model index from dropdown
            GameObject selectedModelPrefab = modelPrefabs[selectedModelIndex];  // Get selected model prefab
            SpawnModel(selectedModelPrefab, averagePosition, Quaternion.identity);
        }
        else
        {
            errorLogger?.DisplayError("Incorrect amount of anchor points");
        }
    }

    private Vector3 CalculateAveragePosition()
    {
        Vector3 averagePosition = Vector3.zero;
        foreach (ARAnchor anchor in activeAnchors)
        {
            averagePosition += anchor.transform.position;
        }
        averagePosition /= activeAnchors.Count;
        return averagePosition;
    }

    private void SpawnModel(GameObject modelPrefab, Vector3 position, Quaternion rotation)
    {
        Instantiate(modelPrefab, position, rotation);
    }

    // Callback for deleting the most recently added anchor when the menu button is pressed.
    private void OnMenu(InputAction.CallbackContext obj)
    {
        if (activeAnchors.Count > 0)
        {
            ARAnchor anchorToDelete = activeAnchors[activeAnchors.Count - 1];
            Debug.Log("SpatialAnchorsTest: Menu hit, attempting to delete Anchor at " + anchorToDelete.transform.position + " with tracking state " + anchorToDelete.trackingState);
            errorLogger?.DisplayError("SpatialAnchorsTest: Menu hit, attempting to delete Anchor at " + anchorToDelete.transform.position + " with tracking state " + anchorToDelete.trackingState);

            if (activeSubsystem != null)
            {
                if (anchorToDelete.trackingState == TrackingState.Tracking)
                {
                    if (activeSubsystem.TryRemoveAnchor(anchorToDelete.trackableId))
                    {
                        Destroy(anchorToDelete.gameObject);
                        activeAnchors.RemoveAt(activeAnchors.Count - 1);
                    }
                    else
                    {
                        string errorMsg = "Error deleting local anchor: Failed to delete anchor from subsystem.";
                        Debug.LogError(errorMsg);
                        errorLogger?.DisplayError(errorMsg);
                    }
                }
                else
                {
                    string warningMsg = "Cannot delete anchor: Anchor is not currently tracking.";
                    Debug.LogWarning(warningMsg);
                    errorLogger?.DisplayError(warningMsg);
                }
            }
            else
            {
                string errorMsg = "Active subsystem is null, cannot delete anchor.";
                Debug.LogError(errorMsg);
                errorLogger?.DisplayError(errorMsg);
            }
        }
    }

    public void RestartApplication()
    {
        // Get the active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the active scene to restart the application
        SceneManager.LoadScene(currentScene.name);
    }

}

