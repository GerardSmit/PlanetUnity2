

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

public partial class PUAspectFit : PUAspectFitBase {

	public PUAspectFit()
	{
		string attr;

		attr = "0,0";
		if(attr != null) { contentSize = new Vector2().PUParse(attr); } 
		attr = "FitInParent";
		if(attr != null) { mode = (PlanetUnity2.AspectFitMode)Enum.Parse(typeof(PlanetUnity2.AspectFitMode), attr); } 

	}
	

	public PUAspectFit(
			Vector2 contentSize,
			PlanetUnity2.AspectFitMode mode ) : this()
	{
		this.contentSize = contentSize;

		this.mode = mode;
	}

	

	public PUAspectFit(
			Vector2 contentSize,
			PlanetUnity2.AspectFitMode mode,
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
		this.contentSize = contentSize;

		this.mode = mode;

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




public class PUAspectFitBase : PUGameObject {






	// XML Attributes
	public Vector2? contentSize;
	public PlanetUnity2.AspectFitMode? mode;




	public override void gaxb_unload()
	{
		base.gaxb_unload();

	}

	public new void gaxb_addToParent()

	{
		if(parent != null)
		{
			FieldInfo parentField = parent.GetType().GetField("AspectFit");
			List<object> parentChildren = null;

			if(parentField != null)
			{
				parentField.SetValue(parent, this);
			}
			else
			{
				parentField = parent.GetType().GetField("AspectFits");

				if(parentField != null)
				{
					parentChildren = (List<object>)(parentField.GetValue(parent));
				}
				else
				{
					parentField = parent.GetType().GetField("GameObjects");
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

		if(this.GetType() == typeof( PUAspectFit ))
		{
			gaxb_addToParent();
		}

		//xmlns = element.GetAttribute("xmlns");


		string attr;
		attr = element.GetAttribute("contentSize");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr == null) { attr = "0,0"; }
		if(attr != null) { contentSize = new Vector2().PUParse(attr); } 
		
		attr = element.GetAttribute("mode");
		if(attr != null) { attr = PlanetUnityOverride.processString(_parent, attr); }
		if(attr == null) { attr = "FitInParent"; }
		if(attr != null) { mode = (PlanetUnity2.AspectFitMode)Enum.Parse(typeof(PlanetUnity2.AspectFitMode), attr); } 
		

	}







	public override void gaxb_appendXMLAttributes(StringBuilder sb)
	{
		base.gaxb_appendXMLAttributes(sb);

		if(contentSize != null) { sb.AppendFormat (" {0}=\"{1}\"", "contentSize", contentSize.Value.PUToString()); }
		if(mode != null) { sb.AppendFormat (" {0}=\"{1}\"", "mode", (int)mode); }

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

		sb.AppendFormat ("<{0}", "AspectFit");

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
			sb.AppendFormat (">{0}</{1}>", seq.ToString(), "AspectFit");
		}
	}
}
