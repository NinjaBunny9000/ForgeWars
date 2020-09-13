using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Component which will flicker a linked light while active by changing its
/// intensity between the min and max values given. The flickering can be
/// sharp or smoothed depending on the value of the smoothing parameter.
///
/// Just activate / deactivate this component as usual to pause / resume flicker
/// </summary>
public class FlickerEffect : MonoBehaviour
{

    [Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
    public GameObject torchLight;
    public Light pointLight;
    public GameObject torch;
    public Material lightMat;
    [Tooltip("Minimum random light intensity")]
    float minIntensity = 1f;
    [Tooltip("Maximum random light intensity")]
    float maxIntensity = 2f;
    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
    [Range(1, 50)]
    int smoothing = 30;

    // Continuous average calculation via FIFO queue
    // Saves us iterating every time we update, we just change by the delta
    Queue<float> smoothQueue;
    float lastSum = 0;
    float intensity;

    float factor;

    /// <summary>
    /// Reset the randomness and start again. You usually don't need to call
    /// this, deactivating/reactivating is usually fine but if you want a strict
    /// restart you can do.
    /// </summary>
    public void Reset() {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start() {
        smoothQueue = new Queue<float>(smoothing);
        // External or internal light?
        if (pointLight == null) {
        pointLight = torchLight.GetComponent<Light>();
        lightMat = torch.GetComponent<Renderer>().material;
        }
    }
    
    void Update() {
        if (pointLight == null)
            return;

        // pop off an item if too big
        while (smoothQueue.Count >= smoothing) {
            lastSum -= smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        intensity = lastSum / (float)smoothQueue.Count;

        // Calculate new smoothed average
        pointLight.intensity = intensity;
        // lightMat.SetColor("_EmissionColor", new Color(191,29,0,1) * intensity);  //* WIP
    }
}
