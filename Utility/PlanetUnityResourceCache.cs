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
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System;

public class PlanetUnityResourceCache
{
	static private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
	static private Dictionary<string, string> stringFiles = new Dictionary<string, string>();
	static private Dictionary<string, Font> fonts = new Dictionary<string, Font>();

	static public void DrainCaches() {
		sprites.Clear ();
		stringFiles.Clear ();
		fonts.Clear ();
	}

	static public Texture2D GetTexture(string s)
	{
		if (s == null) {
			return null;
		}

		if (s.StartsWith("/") && (s.EndsWith (".png") || s.EndsWith (".jpg"))) {
			if (File.Exists(s))     {
				Texture2D fileImage = new Texture2D(512, 512, TextureFormat.ARGB32, false);
				fileImage.LoadImage(File.ReadAllBytes(s));
				fileImage.filterMode = FilterMode.Bilinear;
				fileImage.wrapMode = TextureWrapMode.Clamp;
				return fileImage;
			}
		}

		TextAsset fileData = (TextAsset)PlanetUnityOverride.LoadResource(typeof(TextAsset), s);
        
        if (fileData !=null) {
			Texture2D tex = new Texture2D (2,2,TextureFormat.ARGB32, false);
			tex.LoadImage (fileData.bytes);
			tex.filterMode = FilterMode.Bilinear;
			tex.wrapMode = TextureWrapMode.Clamp;
			return tex;
		}

		Texture2D t = (Texture2D)PlanetUnityOverride.LoadResource(typeof(Texture2D), s);
        
		if (t == null) {
			#if (UNITY_WEBPLAYER == false && UNITY_WEBGL == false)
			if (s.EndsWith (".png") || s.EndsWith (".jpg")) {
				string filePath = Application.streamingAssetsPath + "/" + s;
				if (File.Exists(filePath))     {
					t = new Texture2D(2, 2, TextureFormat.ARGB32, false);
					t.LoadImage(File.ReadAllBytes(filePath));
					t.filterMode = FilterMode.Bilinear;
					t.wrapMode = TextureWrapMode.Clamp;
				}
			}
			#endif

			if (t == null) {
				Debug.Log ("Unable to load streaming asset: " + Application.streamingAssetsPath + "/" + s);
				return null;
			}
		}
		t.filterMode = FilterMode.Bilinear;
		return t;
	}

	static public Sprite GetSprite(string s)
	{
		if (s == null) {
			return null;
		}

		string spriteName = Path.GetFileName (s);
		string spriteKey = s + spriteName;
		if (sprites.ContainsKey(spriteKey)) {
			return sprites [spriteKey];
		}

		Texture2D texture = (Texture2D)PlanetUnityOverride.LoadResource(typeof(Texture2D), s);
		if (texture != null) {
			Sprite sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), Vector2.zero);
			sprites [spriteKey] = sprite;
			return sprite;
		}

		// try load all
		var allSprites = PlanetUnityOverride.LoadAllResources (typeof(Sprite), Path.GetDirectoryName (s));
		foreach (Sprite sprite in allSprites) {
			sprites [s + sprite.name] = sprite;
		}

		if (sprites.ContainsKey (spriteKey)) {
			return sprites [spriteKey];
		}

		return null;
	}

	static public AudioClip GetAudioClip(string s)
	{
		if (s == null) {
			return null;
		}

		return (AudioClip)PlanetUnityOverride.LoadResource(typeof(AudioClip), s);
	}

	static public T GetAsset<T>(string s)
	{
		if (s == null) {
			return default(T);
		}

		return (T)PlanetUnityOverride.LoadResource(typeof(T), s);
	}

	static public string GetTextFile(string s)
	{
		if (s == null) {
			return null;
		}
		if (stringFiles.ContainsKey(s)) {
			return stringFiles [s];
		}
        
        TextAsset stringData = (TextAsset)PlanetUnityOverride.LoadResource(typeof(TextAsset), s);
        
		if (stringData == null) {
			return null;
		}
		string t = stringData.text;
		#if UNITY_EDITOR
		#else
		stringFiles [s] = t;
		#endif
		return t;
	}

	static public string GetTextFileNoCache(string s)
	{
		if (s == null) {
			return null;
		}
        
        TextAsset stringData = (TextAsset)PlanetUnityOverride.LoadResource(typeof(TextAsset), s);
        
		if (stringData == null) {
			return null;
		}
		return stringData.text;
	}

	static public Font GetFont(string s)
	{
		if (s == null) {
			return null;
		}
		if (fonts.ContainsKey(s)) {
			return fonts [s];
		}

		Font font = null;

		if (s.Equals ("Arial")) {
			font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		}

		if (font == null) {
			font = (Font)PlanetUnityOverride.LoadResource(typeof(Font), s);
		}

		if (font == null) {
			return null;
		}
		fonts [s] = font;
		return font;
	}
}
