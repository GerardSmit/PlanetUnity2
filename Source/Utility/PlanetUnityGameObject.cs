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

using UnityEngine;
using System.Xml;
using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;
#endif

public delegate void Task();

public class PlanetUnityOverride {

	public static bool ForceActualSprites = false;

	private static Mathos.Parser.MathParser mathParser = new Mathos.Parser.MathParser();
	public static int minFPS = 10;
	public static int maxFPS = 60;

	public static Func<string, byte[]> bytesFromPath = (path) => {
		TextAsset t = PlanetUnityResourceCache.GetAsset<TextAsset>(path);
		if(t == null){
			return null;
		}
		return t.bytes;
	};

	public static Func<string, string> xmlFromPath = (path) => {
		return PlanetUnityResourceCache.GetTextFile(path);
	};

	public static Func<PUCanvas, bool> orientationDidChange = (canvas) => {
		PlanetUnityGameObject.currentGameObject.ReloadCanvas();
		return true;
	};

	public static Func<string> defaultFont = () => {
		return "Arial";
	};

	public static Func<float> screenDPI = () => {
		return Screen.dpi;
	};

	public static Func<decimal,decimal> app1 = (x) => {
		return (decimal)0.0f;
	};

	public static Func<string,string> appProcessString = (x) => {
		return x;
	};

	public static Func<PUGameObject, string> shaderForObject = (obj) => {
		return null;
	};
    
    public static Func<Type, string, object> LoadResource = (type, pathToResource) => {
        return Resources.Load(pathToResource, type);
    };
    
    public static Func<Type, string, object[]> LoadAllResources = (type, pathToResources) => {
        return Resources.LoadAll(pathToResources, type);
    };

    
	static StringBuilder evalStringBuilder = new StringBuilder();
	private static string evaluateString(string evalListString, object o, float multiplier, Func<decimal, decimal> appOverride = null) {
		var parts = Regex.Split (evalListString, ",(?![^(]*\\))");

		RectTransform rectTransform = null;

		mathParser.LocalVariables ["dpi"] = Convert.ToDecimal (PlanetUnityOverride.screenDPI());
		mathParser.LocalVariables ["screenW"] = Convert.ToDecimal (Screen.width / multiplier);
		mathParser.LocalVariables ["screenH"] = Convert.ToDecimal (Screen.height / multiplier);

		#if UNITY_IOS
		mathParser.LocalVariables ["statusBarHeight"] = Convert.ToDecimal ((0.13f * PlanetUnityOverride.screenDPI()) / multiplier);
		#else
		mathParser.LocalVariables ["statusBarHeight"] = 0;
		#endif

		GameObject parentAsGameObject = o as GameObject;
		PUGameObject parentAsPUGameObject = o as PUGameObject;

	    if (parentAsPUGameObject != null)
	    {
	        var root = parentAsPUGameObject.Scope() as PUCanvas;

	        if (root != null)
	        {
	            foreach (var variable in root.GetVariables())
	            {
	                mathParser.LocalVariables[variable.Name] = variable.Value;
	            }
	        }
	    }

        if (parentAsGameObject != null) {
			rectTransform = parentAsGameObject.GetComponent<RectTransform> ();
		}
		else if (parentAsPUGameObject != null) {
			rectTransform = parentAsPUGameObject.gameObject.GetComponent<RectTransform> ();
		}

		if (rectTransform) {
			// Work around for unity stretching canvas bug
			if (o is PUCanvas && (int)rectTransform.rect.width == 100 && (int)rectTransform.rect.height == 100) {
				mathParser.LocalVariables ["w"] = Convert.ToDecimal (Screen.width / multiplier);
				mathParser.LocalVariables ["h"] = Convert.ToDecimal (Screen.height / multiplier);
			} else {
				mathParser.LocalVariables ["w"] = Convert.ToDecimal (rectTransform.rect.width / multiplier);
				mathParser.LocalVariables ["h"] = Convert.ToDecimal (rectTransform.rect.height / multiplier);
			}
		}

	    mathParser.usedTokens.Clear();
        evalStringBuilder.Length = 0;
		foreach (string part in parts) {
			decimal result = (mathParser.Parse (part) * (decimal)multiplier);
			if (appOverride != null) {
				result = appOverride (result);
			}
			evalStringBuilder.AppendFormat ("{0},", result);
		}
		evalStringBuilder.Length = evalStringBuilder.Length - 1;

		return evalStringBuilder.ToString ();
	}

	public static string processString(object cur, object o, string s)
	{
		if (s == null)
			return null;

		#if USE_LAURETTE
		s = s.Replace("@LANGUAGE", Localizations.GetLanguageCode());
		#endif
		s = s.Replace("\\n", "\n");

		if (s.Equals ("nan")) {
			return "0";
		}

		if (s.StartsWith ("@localization(")) {
			#if USE_LAURETTE
			string evalListString = s.Substring(14, s.Length-15);
			s = Localizations.TranslateKey (evalListString);
			#endif
		} else if (s.StartsWith ("@eval(")) {
			string evalListString = s.Substring(6, s.Length-7);
			s = evaluateString (evalListString, o, 1.0f);

		    PUGameObject current = cur as PUGameObject;
		    if (current != null)
		    {
		        foreach (var token in mathParser.usedTokens)
		        {

                    current.Canvas.GetVariable(token).AddListener(current);
		        }
		    }
		} else if (s.StartsWith ("@dpi(")) {
			string evalListString = s.Substring(5, s.Length-6);
			s = evaluateString (evalListString, o, PlanetUnityOverride.screenDPI());

		} else if (s.StartsWith ("@app1(")) {
			string evalListString = s.Substring(6, s.Length-7);
			s = evaluateString (evalListString, o, PlanetUnityOverride.screenDPI(), PlanetUnityOverride.app1);

		}

		return PlanetUnityOverride.appProcessString (s);
	}

}

public class PlanetUnityGameObject : MonoBehaviour {

	static Thread mainThread;

	public static float desiredFPS;
	public static void RequestFPS(float f) {
		// Called by entities to request a specific fps. PlanetUnity will set the fps dynamically
		// to the highest requested fps.
		if (f > desiredFPS) {
			desiredFPS = f;
		}
	}

	public string xmlPath;

	public Vector2 referenceResolution;
	public bool scaleAutomatically;

	private static GameObject planetUnityContainer;
	private PUCanvas canvas;

	private Canvas rootCanvas;

	private bool shouldReloadMainXML = false;

	static public void SetReferenceResolution(float w, float h) {
		CanvasScaler scaler = planetUnityContainer.AddComponent<CanvasScaler>();
		scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		scaler.referenceResolution = new Vector2(w, h);
	}

	static public PlanetUnityGameObject currentGameObject = null;

	static public GameObject MainContainer() {
		return currentGameObject.Container ();
	}

	static public PUCanvas MainCanvas() {
		return currentGameObject.Canvas ();
	}

	public GameObject Container() {
		return planetUnityContainer;
	}

	public PUCanvas Canvas() {
		return canvas;
	}


	public static bool IsMainThread()
	{
		if (mainThread == null) {
			return true;
		}
		return mainThread.Equals (System.Threading.Thread.CurrentThread);
	}

	#region XML navigation

	private List<string> navigationHistory = new List<string>();

	static public string PeekXML() {
		return PlanetUnityGameObject.currentGameObject.InternalPeekXML ();
	}

	static public void PushXML(string newXMLPath) {
		PlanetUnityGameObject.currentGameObject.InternalPushXML (newXMLPath);
	}

	static public void PopXML() {
		PlanetUnityGameObject.currentGameObject.InternalPopXML ();
	}


	private string InternalPeekXML() {
		if (navigationHistory.Count == 0) {
			return null;
		}
		return navigationHistory [navigationHistory.Count - 1];
	}

	private void InternalPushXML(string newXMLPath) {
		navigationHistory.Add (xmlPath);
		xmlPath = newXMLPath;
		ReloadCanvas ();
	}

	private void InternalPopXML() {
		xmlPath = navigationHistory [navigationHistory.Count - 1];
		navigationHistory.RemoveAt (navigationHistory.Count - 1);
		ReloadCanvas ();
	}

	#endregion


	#region XML dynamic loading

	static public PUGameObject LoadXML(string xmlPath, PUGameObject parent) {
		
		PUGameObject loadedGameObject = (PUGameObject)PlanetUnity2.loadXML (PlanetUnityResourceCache.GetAsset<TextAsset>(xmlPath).bytes, parent, null);

		#if UNITY_EDITOR
		if(planetUnityContainer != null){
			foreach (Transform t in planetUnityContainer.GetComponentsInChildren<Transform>()) {
				t.gameObject.hideFlags = HideFlags.DontSave;
			}
		}
		#endif

		return loadedGameObject;
	}

	static public PUGameObject LoadXML(string xmlPath, GameObject parent) {
		PUGameObject loadedGameObject = (PUGameObject)PlanetUnity2.loadXML (PlanetUnityResourceCache.GetAsset<TextAsset>(xmlPath).bytes, parent, null);

		#if UNITY_EDITOR
		if(planetUnityContainer != null){
			foreach (Transform t in planetUnityContainer.GetComponentsInChildren<Transform>()) {
				t.gameObject.hideFlags = HideFlags.DontSave;
			}
		}
		#endif

		return loadedGameObject;
	}

	#endregion



	// Use this for initialization
	void Start () {
	
		mainThread = System.Threading.Thread.CurrentThread;

		Application.targetFrameRate = 60;

		currentGameObject = this;

		ReloadCanvas ();

		#if UNITY_EDITOR
		NotificationCenter.addObserver(this, PlanetUnity2.EDITORFILEDIDCHANGE, null, (args,name) => {
			string assetPath = args ["path"].ToString();

			if( assetPath.Contains(xmlPath+".xml") ||
				assetPath.EndsWith(".strings"))
			{
				EditorReloadCanvas ();
				ReloadCanvas ();
			}
		});
		#endif
	}

	void OnDestroy () {
		NotificationCenter.removeObserver (this);
		RemoveCanvas ();
	}

	void Update () {

		if (shouldReloadMainXML) {
			shouldReloadMainXML = false;
			#if UNITY_EDITOR
			ReloadCanvas ();
			#endif
		}

		lock (_queueLock)
		{
			// allow us to process tasks for a number of milliseconds before holding off until later
			Stopwatch sw = new Stopwatch();
			sw.Start ();

			// make a copy of the queue and process the copy; this will allow callee's to queue tasks correctly
			List<Task> currentQueue = new List<Task> (TaskQueue);
			TaskQueue.Clear ();
			while (currentQueue.Count > 0 && sw.ElapsedMilliseconds < 60) {
				currentQueue [0] ();
				currentQueue.RemoveAt (0);
			}

			// for any tasks which did not get executed, add them back to the front of the task queue
			for (int i = currentQueue.Count - 1; i >= 0; i--) {
				TaskQueue.Insert (0, currentQueue [i]);
			}
		}
	}

	public void RemoveCanvas () {
		if (canvas != null) {
			canvas.unload ();
			canvas = null;
		}

		SafeRemoveAllChildren ();
	}
		
	public void LoadCanvasXML (string xml) {

		if (xmlPath == null || PlanetUnityOverride.xmlFromPath (xmlPath) == null) {
			return;
		}
			
		RemoveCanvas ();

		Stopwatch sw = Stopwatch.StartNew ();

		planetUnityContainer = GameObject.Find ("PlanetUnityContainer");
		if (planetUnityContainer == null) {
			planetUnityContainer = new GameObject ("PlanetUnityContainer");

			rootCanvas = planetUnityContainer.AddComponent<Canvas> ();
		}
			
		//UnityEngine.Debug.Log ("LoadCanvasXML");
		PUGameObject rootObject = (PUGameObject)PlanetUnity2.loadXML (System.Text.Encoding.UTF8.GetBytes (xml), planetUnityContainer, null);

		if (rootObject is PUCanvas) {
			canvas = rootObject as PUCanvas;
		} else {
			canvas = new PUCanvas (PlanetUnity2.CanvasRenderMode.ScreenSpaceCamera, false, 100);
			canvas.LoadIntoGameObject (planetUnityContainer);
			rootObject.LoadIntoPUGameObject (canvas);
		}


		// This is kind of silly, but we need to do this because we want the root canvas to match
		// the canvas of the loaded scene, but we can't just grab the contents of the sub canvas
		// because unity sets it to inherited and those contents don't persist
		if (canvas.renderMode == PlanetUnity2.CanvasRenderMode.ScreenSpaceOverlay)
			rootCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
		if (canvas.renderMode == PlanetUnity2.CanvasRenderMode.ScreenSpaceCamera) {
			GameObject puCamera = GameObject.Find ("PUCamera");
			if (puCamera != null)
				rootCanvas.worldCamera = puCamera.GetComponent<Camera> ();
			else
				rootCanvas.worldCamera = Camera.main;
			rootCanvas.renderMode = RenderMode.ScreenSpaceCamera;
		}
		if (canvas.renderMode == PlanetUnity2.CanvasRenderMode.WorldSpace)
			rootCanvas.renderMode = RenderMode.WorldSpace;
		rootCanvas.pixelPerfect = canvas.pixelPerfect;
		rootCanvas.planeDistance = canvas.planeDistance.Value;
		// End silly section

		// Unity 5.3: if this canvas does not override sorting, then it doesn't render in the editor (which is annoying)
		canvas.canvas.overrideSorting = true;

		#if UNITY_EDITOR
		if(planetUnityContainer != null){
			foreach (Transform t in planetUnityContainer.GetComponentsInChildren<Transform>()) {
				t.gameObject.hideFlags = HideFlags.DontSave;
			}
		}
		#endif

		sw.Stop ();

		#if !UNITY_EDITOR
		UnityEngine.Debug.Log ("[" + sw.Elapsed.TotalMilliseconds + "ms] Loading canvas " + xmlPath + ".xml");
		#endif

		//Profile.PrintResults ();
		//Profile.Reset ();
	}

	public void CheckForEventSystem() {
		GameObject eventSystem = GameObject.Find ("EventSystem");
		if (eventSystem == null) {
			// We need to create this manually...

			eventSystem = new GameObject ("EventSystem");
			eventSystem.AddComponent<EventSystem> ();
			eventSystem.AddComponent<StandaloneInputModule> ();

#if !UNITY_5_3_OR_NEWER
			eventSystem.AddComponent<TouchInputModule> ();
#endif

			GameObject.DontDestroyOnLoad(eventSystem);

		}

		EventSystem system = eventSystem.GetComponent<EventSystem> ();
		system.pixelDragThreshold = (int)(PlanetUnityOverride.screenDPI () * 0.1f);
	}

	public void ReloadCanvas () {

		CheckForEventSystem ();

		LoadCanvasXML (PlanetUnityOverride.xmlFromPath (xmlPath));

		if (scaleAutomatically) {
			SetReferenceResolution (referenceResolution.x, referenceResolution.y);
		}
	}

	public void SafeRemoveAllChildren() {

		//UnityEngine.Debug.Log ("SafeRemoveAllChildren");

		// This gets hokey, but the editor complains if the components are not removed in a specific order
		// before the game object itself is destroyed...
		planetUnityContainer = GameObject.Find ("PlanetUnityContainer");
		if (planetUnityContainer != null) {
			for (int i = planetUnityContainer.transform.childCount - 1; i >= 0; i--) {
				Transform canvasObject = planetUnityContainer.transform.GetChild (i);

				// Remove all components...
				DestroyImmediate (canvasObject.GetComponent<GraphicRaycaster> ());
				DestroyImmediate (canvasObject.GetComponent<Canvas> ());

				DestroyImmediate (canvasObject.gameObject);
			}

			DestroyImmediate (planetUnityContainer);
		}
	}

	public void EditorReloadCanvas () {
		SafeRemoveAllChildren ();
	}

	private List<Task> TaskQueue = new List<Task>();
	private object _queueLock = new object();

	public void PrivateScheduleTask(Task newTask) {

		lock (_queueLock)
		{
			TaskQueue.Add (newTask);
		}
	}

	public bool PrivateHasTasks()
	{
		return (TaskQueue.Count > 0);
	}

	public void PrivateClearTasks()
	{
		lock (_queueLock)
		{
			TaskQueue.Clear ();
		}
	}

	public static void ClearTasks()
	{
		if (System.Object.ReferenceEquals(currentGameObject, null)) {
			return;
		}
		currentGameObject.PrivateClearTasks ();
	}

	public static void ScheduleTask(Action block)
	{
		if (System.Object.ReferenceEquals(currentGameObject, null)) {
			return;
		}
		currentGameObject.PrivateScheduleTask(new Task(block));
	}

	public static void PerformTask(Action block)
	{
		if (System.Object.ReferenceEquals(currentGameObject, null)) {
			return;
		}

		if (PlanetUnityGameObject.IsMainThread ()) {
			block ();
			return;
		}

		AutoResetEvent autoEvent = new AutoResetEvent (false);

		PlanetUnityGameObject.ScheduleTask (() => {
			block ();
			autoEvent.Set ();
		});

		autoEvent.WaitOne ();
	}

	public static bool HasTasks()
	{
		if (System.Object.ReferenceEquals(currentGameObject, null)) {
			return false;
		}
		return currentGameObject.PrivateHasTasks ();
	}
}

#if UNITY_EDITOR

[ExecuteInEditMode]
public class CustomPostprocessor : AssetPostprocessor
{
	private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
	{
		foreach(string asset in importedAssets)
		{
			NotificationCenter.postNotification(null, PlanetUnity2.EDITORFILEDIDCHANGE, NotificationCenter.Args("path", asset));
		}

		if (Application.isPlaying == false) {
			GameObject puObject = GameObject.Find ("PlanetUnity");
			if (puObject == null)
				return;
			PlanetUnityGameObject script = puObject.GetComponent<PlanetUnityGameObject> ();
			if (script == null)
				return;

			script.EditorReloadCanvas ();
		}
	}
}

#endif


