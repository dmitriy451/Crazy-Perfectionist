using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class AutoToonMaker : MonoBehaviour
{
    public List<Material> materials;

    [Header("First Color")] public float firstSaturationChange = -0.3f;

    public float firstValueChange = 0.3f;

    [Header("Second Color")] public float secondSaturationChange = 0.1f;

    public float secondValueChange = -0.2f;

    [Header("Lighting")] public float lightContribution = 0.2f;

    public float unityShadowPower = 0.3f;

    private float m_Hue;
    private float m_Saturation;
    private float m_Value;

    [Button]
    public void AutoTooning()
    {
        var _counter = -1;
        for (var i = 0; i < materials.Count; i++)
        {
            _counter++;
            if (!materials[_counter].HasProperty("_SelfShadingSize"))
                materials[_counter].shader = Shader.Find("FlatKit/Stylized Surface");

            Color.RGBToHSV(materials[_counter].color, out m_Hue, out m_Saturation, out m_Value);

            materials[_counter].SetColor("_BaseColor",
                Color.HSVToRGB(m_Hue, m_Saturation + firstSaturationChange, m_Value + firstValueChange));
            materials[_counter].SetColor("_ColorDim",
                Color.HSVToRGB(m_Hue, m_Saturation + secondSaturationChange, m_Value + secondValueChange));

            materials[_counter].SetFloat("_SelfShadingSize", 0.7f);
            materials[_counter].SetFloat("_ShadowEdgeSize", 0f);
            materials[_counter].SetFloat("_Flatness", 1.0f);

            materials[_counter].SetFloat("_LightContribution", lightContribution);

            materials[_counter].SetFloat("_UnityShadowMode", 1.0f);
            materials[_counter].SetFloat("_UnityShadowPower", unityShadowPower);
            materials[_counter].SetColor("_UnityShadowColor",
                Color.HSVToRGB(m_Hue, m_Saturation + secondSaturationChange, m_Value + secondValueChange));
        }
    }
}