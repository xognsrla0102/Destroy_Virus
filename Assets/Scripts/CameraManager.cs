using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    private const float CAMERA_FORCE = 10f;

    public static CameraManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraManager>();
            }
            return instance;
        }
    }

    private CinemachineImpulseSource shake;

    private void Start()
    {
        shake = GetComponent<CinemachineImpulseSource>();
    }    

    public void DamagedShake()
    {
        shake.m_ImpulseDefinition.m_AmplitudeGain = 5f;
        shake.m_ImpulseDefinition.m_FrequencyGain = 0.05f;
        shake.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = 0.2f;
        shake.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = 0.3f;

        shake.GenerateImpulse(CAMERA_FORCE);
    }

    public void EnemyHitShake()
    {
        shake.m_ImpulseDefinition.m_AmplitudeGain = 1f;
        shake.m_ImpulseDefinition.m_FrequencyGain = 1f;
        shake.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = 0.025f;
        shake.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = 0.025f;

        shake.GenerateImpulse(CAMERA_FORCE);
    }
}
