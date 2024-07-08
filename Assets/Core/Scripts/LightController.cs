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
    [Header("Global")]
    public Color32 global_color;

    [Header("Day mode")]
    public Color32 day_light;

	public bool dayMode = true;
    public List<GameObject> lights = new List<GameObject>();
	void Start()
    {
        if (dayMode)
        {
			RenderSettings.ambientLight = day_light;
			foreach(GameObject go in lights)
            {
                go.SetActive(true);
            }
		}
        else
        {
			foreach (GameObject go in lights)
			{
				go.SetActive(false);
			}
		}
	}

    void FixedUpdate()
    {
        if (!dayMode)
        {
            RoomLightsColorChange(elements, totalLightColor);
            RoomLightSensetivityChange(elements, totalSens);
            RoomLightGlobalChange(global_color);
        }
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
	public void RoomLightGlobalChange(Color32 color)
	{
        RenderSettings.ambientLight = color;
	}
}
