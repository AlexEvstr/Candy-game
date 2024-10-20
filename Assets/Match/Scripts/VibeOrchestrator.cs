using System.Runtime.InteropServices;
using UnityEngine;

public class VibeOrchestrator : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void InvokeGentleVibe();

    [DllImport("__Internal")]
    private static extern void InvokeBalancedVibe();

    [DllImport("__Internal")]
    private static extern void InvokeMightyVibe();

    [DllImport("__Internal")]
    private static extern void InvokeErrorBuzz();

    private void SummonVibe(System.Action vibeMethod)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            vibeMethod?.Invoke();
        }
    }

    public void TriggerGentlePulse()
    {
        SummonVibe(InvokeGentleVibe);
    }

    public void TriggerBalancedPulse()
    {
        SummonVibe(InvokeBalancedVibe);
    }

    public void TriggerMightyPulse()
    {
        SummonVibe(InvokeMightyVibe);
    }

    public void TriggerErrorBuzz()
    {
        SummonVibe(InvokeErrorBuzz);
    }
}