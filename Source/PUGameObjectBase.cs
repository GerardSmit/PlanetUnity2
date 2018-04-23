

using UnityEngine;


//
// Autogenerated by gaxb ( https://github.com/SmallPlanet/gaxb )
//

using System;
using System.Xml;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Security;
using TB;

public partial class PUGameObject : PUGameObjectBase {

	public PUGameObject()
	{
		string attr;

		attr = "0,0,0";
		if(attr != null) { position = new Vector3().PUParse(attr); } 
		attr = "0,0";
		if(attr != null) { size = new Vector2().PUParse(attr); } 
		attr = "0,0,0";
		if(attr != null) { rotation = new Vector3().PUParse(attr); } 
		attr = "1,1,1";
		if(attr != null) { scale = new Vector3().PUParse(attr); } 
		attr = "0,0";
		if(attr != null) { pivot = new Vector2().PUParse(attr); } 
		attr = "bottom,left";
		if(attr != null) { anchor = attr; } 
		attr = "true";
		if(attr != null) { active = bool.Parse(attr); } 
		attr = "false";
		if(attr != null) { showMaskGraphic = bool.Parse(attr); } 

	}
	

	public PUGameObject(
			Vector4 bounds,
			Vector3 position,
			Vector2 size,
			Vector3 rotation,
			Vector3 scale,
			Vector2 pivot,
			string anchor,
			bool active,
			bool rectMask2D,
			bool mask,
			bool showMaskGraphic,
			Vector4 maskInset,
			bool outline,
			float lastY,
			float lastX,
			string shader,
			bool ignoreMouse,
			string components ) : this()
	{
		this.bounds = bounds;

		this.position = position;

		this.size = size;

		this.rotation = rotation;

		this.scale = scale;

		this.pivot = pivot;

		this.anchor = anchor;

		this.active = active;

		this.rectMask2D = rectMask2D;

		this.mask = mask;

		this.showMaskGraphic = showMaskGraphic;

		this.maskInset = maskInset;

		this.outline = outline;

		this.lastY = lastY;

		this.lastX = lastX;

		this.shader = shader;

		this.ignoreMouse = ignoreMouse;

		this.components = components;
	}

	

	public PUGameObject(
			Vector4 bounds,
			Vector3 position,
			Vector2 size,
			Vector3 rotation,
			Vector3 scale,
			Vector2 pivot,
			string anchor,
			bool active,
			bool rectMask2D,
			bool mask,
			bool showMaskGraphic,
			Vector4 maskInset,
			bool outline,
			float lastY,
			float lastX,
			string shader,
			bool ignoreMouse,
			string components,
			string title,
			string tag,
			string tag1,
			string tag2,
			string tag3,
			string tag4,
			string tag5,
			string tag6 ) : this()
	{
		this.bounds = bounds;

		this.position = position;

		this.size = size;

		this.rotation = rotation;

		this.scale = scale;

		this.pivot = pivot;

		this.anchor = anchor;

		this.active = active;

		this.rectMask2D = rectMask2D;

		this.mask = mask;

		this.showMaskGraphic = showMaskGraphic;

		this.maskInset = maskInset;

		this.outline = outline;

		this.lastY = lastY;

		this.lastX = lastX;

		this.shader = shader;

		this.ignoreMouse = ignoreMouse;

		this.components = components;

		this.title = title;

		this.tag = tag;

		this.tag1 = tag1;

		this.tag2 = tag2;

		this.tag3 = tag3;

		this.tag4 = tag4;

		this.tag5 = tag5;

		this.tag6 = tag6;
	}


}




public class PUGameObjectBase : PUObject {






	// XML Attributes
	public Vector4? bounds;
	public Vector3? position;
	public Vector2? size;
	public Vector3? rotation;
	public Vector3? scale;
	public Vector2? pivot;
	public string anchor;
	public bool active;
	public bool rectMask2D;
	public bool mask;
	public bool showMaskGraphic;
	public Vector4? maskInset;
	public bool outline;
	public float? lastY;
	public float? lastX;
	public string shader;
	public bool ignoreMouse;
	public string components;




	public override void gaxb_unload()
	{
		base.gaxb_unload();

	}

	public new void gaxb_addToParent()

	{
		if(parent != null)
		{
			FieldInfo parentField = parent.GetType().GetField("GameObject");
			List<object> parentChildren = null;

			if(parentField != null)
			{
				parentField.SetValue(parent, this);
			}
			else
			{
				parentField = parent.GetType().GetField("GameObjects");

				if(parentField != null)
				{
					parentChildren = (List<object>)(parentField.GetValue(parent));
				}
				else
				{
					parentField = parent.GetType().GetField("Objects");
					if(parentField != null)
					{
						parentChildren = (List<object>)(parentField.GetValue(parent));
					}
				}
				if(parentChildren == null)
				{
					FieldInfo childrenField = parent.GetType().GetField("children");
					if(childrenField != null)
					{
						parentChildren = (List<object>)childrenField.GetValue(parent);
					}
				}
				if(parentChildren != null)
				{
					parentChildren.Add(this);
				}

			}
		}
	}

	private string unescape(string s) {
		if (string.IsNullOrEmpty(s)) return s;

		string returnString = s;
		returnString = returnString.Replace("&amp;", "&");
		returnString = returnString.Replace("&apos;", "'");
		returnString = returnString.Replace("&quot;", "\"");
		returnString = returnString.Replace("&gt;", ">");
		returnString = returnString.Replace("&lt;", "<");
		return returnString;
	}

	public override void gaxb_load(TBXMLElement element, object _parent, Hashtable args)
	{
		base.gaxb_load(element, _parent, args);

		if(element == null && _parent == null)
			return;

		parent = _parent;

		if(this.GetType() == typeof( PUGameObject ))
		{
			gaxb_addToParent();
		}

		//xmlns = element.GetAttribute("xmlns");


		string attr;
		attr = element.GetAttribute("bounds");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr != null) { bounds = new Vector4().PUParse(attr); } 
		
		attr = element.GetAttribute("position");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr == null) { attr = "0,0,0"; }
		if(attr != null) { position = new Vector3().PUParse(attr); } 
		
		attr = element.GetAttribute("size");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr == null) { attr = "0,0"; }
		if(attr != null) { size = new Vector2().PUParse(attr); } 
		
		attr = element.GetAttribute("rotation");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr == null) { attr = "0,0,0"; }
		if(attr != null) { rotation = new Vector3().PUParse(attr); } 
		
		attr = element.GetAttribute("scale");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr == null) { attr = "1,1,1"; }
		if(attr != null) { scale = new Vector3().PUParse(attr); } 
		
		attr = element.GetAttribute("pivot");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr == null) { attr = "0,0"; }
		if(attr != null) { pivot = new Vector2().PUParse(attr); } 
		
		attr = element.GetAttribute("anchor");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr == null) { attr = "bottom,left"; }
		if(attr != null) { anchor = unescape(attr); } 
		
		attr = element.GetAttribute("active");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr == null) { attr = "true"; }
		if(attr != null) { active = bool.Parse(attr); } 
		
		attr = element.GetAttribute("rectMask2D");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr != null) { rectMask2D = bool.Parse(attr); } 
		
		attr = element.GetAttribute("mask");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr != null) { mask = bool.Parse(attr); } 
		
		attr = element.GetAttribute("showMaskGraphic");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr == null) { attr = "false"; }
		if(attr != null) { showMaskGraphic = bool.Parse(attr); } 
		
		attr = element.GetAttribute("maskInset");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr != null) { maskInset = new Vector4().PUParse(attr); } 
		
		attr = element.GetAttribute("outline");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr != null) { outline = bool.Parse(attr); } 
		
		attr = element.GetAttribute("lastY");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr != null) { lastY = float.Parse(attr); } 
		
		attr = element.GetAttribute("lastX");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr != null) { lastX = float.Parse(attr); } 
		
		attr = element.GetAttribute("shader");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr != null) { shader = unescape(attr); } 
		
		attr = element.GetAttribute("ignoreMouse");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr != null) { ignoreMouse = bool.Parse(attr); } 
		
		attr = element.GetAttribute("components");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr != null) { components = unescape(attr); } 
		

	}







	public override void gaxb_appendXMLAttributes(StringBuilder sb)
	{
		base.gaxb_appendXMLAttributes(sb);

		if(bounds != null) { sb.AppendFormat (" {0}=\"{1}\"", "bounds", bounds.Value.PUToString()); }
		if(position != null) { sb.AppendFormat (" {0}=\"{1}\"", "position", position.Value.PUToString()); }
		if(size != null) { sb.AppendFormat (" {0}=\"{1}\"", "size", size.Value.PUToString()); }
		if(rotation != null) { sb.AppendFormat (" {0}=\"{1}\"", "rotation", rotation.Value.PUToString()); }
		if(scale != null) { sb.AppendFormat (" {0}=\"{1}\"", "scale", scale.Value.PUToString()); }
		if(pivot != null) { sb.AppendFormat (" {0}=\"{1}\"", "pivot", pivot.Value.PUToString()); }
		if(anchor != null) { sb.AppendFormat (" {0}=\"{1}\"", "anchor", SecurityElement.Escape (anchor)); }
		 sb.AppendFormat (" {0}=\"{1}\"", "active", active.ToString().ToLower()); 
		 sb.AppendFormat (" {0}=\"{1}\"", "rectMask2D", rectMask2D.ToString().ToLower()); 
		 sb.AppendFormat (" {0}=\"{1}\"", "mask", mask.ToString().ToLower()); 
		 sb.AppendFormat (" {0}=\"{1}\"", "showMaskGraphic", showMaskGraphic.ToString().ToLower()); 
		if(maskInset != null) { sb.AppendFormat (" {0}=\"{1}\"", "maskInset", maskInset.Value.PUToString()); }
		 sb.AppendFormat (" {0}=\"{1}\"", "outline", outline.ToString().ToLower()); 
		if(lastY != null) { sb.AppendFormat (" {0}=\"{1}\"", "lastY", lastY.Value.ToString ("0.##")); }
		if(lastX != null) { sb.AppendFormat (" {0}=\"{1}\"", "lastX", lastX.Value.ToString ("0.##")); }
		if(shader != null) { sb.AppendFormat (" {0}=\"{1}\"", "shader", SecurityElement.Escape (shader)); }
		 sb.AppendFormat (" {0}=\"{1}\"", "ignoreMouse", ignoreMouse.ToString().ToLower()); 
		if(components != null) { sb.AppendFormat (" {0}=\"{1}\"", "components", SecurityElement.Escape (components)); }

	}

	public override void gaxb_appendXMLSequences(StringBuilder sb)
	{
		base.gaxb_appendXMLSequences(sb);


	}

	public override void gaxb_appendXML(StringBuilder sb)
	{
		if(sb.Length == 0)
		{
			sb.AppendFormat ("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
		}

		sb.AppendFormat ("<{0}", "GameObject");

		if(xmlns != null) {
			if(parent == null) {
				sb.AppendFormat (" {0}=\"{1}\"", "xmlns", xmlns);
			}else{
				FieldInfo parentField = parent.GetType().GetField("xmlns");
				if(parentField != null && xmlns.Equals(parentField.GetValue(parent)) == false)
				{
					sb.AppendFormat (" {0}=\"{1}\"", "xmlns", xmlns);
				}
			}
		}

		gaxb_appendXMLAttributes(sb);


		StringBuilder seq = new StringBuilder();
		seq.AppendFormat(" ");
		gaxb_appendXMLSequences(seq);

		if(seq.Length == 1)
		{
			sb.AppendFormat (" />");
		}
		else
		{
			sb.AppendFormat (">{0}</{1}>", seq.ToString(), "GameObject");
		}
	}
}
