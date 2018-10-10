/* Copyright (c) 2012 Small Planet Digital, LLC
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
 * (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
 * publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
 * FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Variable
{
    private int _value;
    private List<PUGameObject> _listening;

    public Variable(string name)
    {
        Name = name;
        _listening = new List<PUGameObject>();
    }

    public string Name { get; set; }

    public int Value
    {
        get { return _value; }
        set
        {
            if (value == _value)
            {
                return;
            }

            _value = value;

            foreach (var obj in _listening)
            {
                obj.gaxb_loadattrs();
            }
        }
    }

    public void AddListener(PUGameObject obj)
    {
        if (!_listening.Contains(obj))
        {
            _listening.Add(obj);
        }
    }
}

public partial class PUCanvas : PUCanvasBase {

	public Canvas canvas;
	public GraphicRaycaster graphicRaycaster;
    private Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

    public Variable GetVariable(string name)
    {
        Variable variable;

        if (!variables.TryGetValue(name, out variable))
        {
            variable = new Variable(name);
            variables.Add(name, variable);
        }

        return variable;
    }

    public IEnumerable<Variable> GetVariables()
    {
        return variables.Values;
    }

	public override void gaxb_init ()
	{
		gameObject = new GameObject("<Canvas/>", typeof(RectTransform));

		canvas = gameObject.AddComponent<Canvas>();
		graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();

		SetFrame (0, 0, 0, 0, 0, 0, "stretch,stretch");

		ScheduleForStart ();
	}

	public override void Start () {

		if (renderMode == PlanetUnity2.CanvasRenderMode.ScreenSpaceOverlay)
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		if (renderMode == PlanetUnity2.CanvasRenderMode.ScreenSpaceCamera) {
			canvas.renderMode = RenderMode.ScreenSpaceCamera;
			GameObject puCamera = GameObject.Find("PUCamera");
			if(puCamera != null)
				canvas.worldCamera = puCamera.GetComponent<Camera>();
			else
				canvas.worldCamera = Camera.main;
			canvas.planeDistance = planeDistance.Value;
		}
		if (renderMode == PlanetUnity2.CanvasRenderMode.WorldSpace)
			canvas.renderMode = RenderMode.WorldSpace;

		canvas.pixelPerfect = pixelPerfect;
		canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
	}

}
