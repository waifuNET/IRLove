using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [System.Serializable]
    public class LightElement
    {
        public string name;
        public Light light;
	}
    public List<LightElement> elements;

    public Color32 totalLightColor;
    public float totalSens;

	void Start()
    {
	}

    void FixedUpdate()
    {
		RoomLightsColorChange(elements, totalLightColor);
		RoomLightSensetivityChange(elements, totalSens);
	}

	public void RoomLightsColorChange(List<LightElement> elements, Color32 color)
    {
        foreach (LightElement element in elements)
        {
            element.light.color = color;
        }
    }
    public void RoomLightSensetivityChange(List<LightElement> elements, float sens)
    {
		foreach (LightElement element in elements)
		{
			element.light.intensity = sens;
		}
	}
}
