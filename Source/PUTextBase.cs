

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

public partial class PUText : PUTextBase {

	public PUText()
	{
		string attr;

		attr = "12";
		if(attr != null) { fontSize = (int)float.Parse(attr); } 
		attr = "0,0,0,1";
		if(attr != null) { fontColor = new Color().PUParse(attr); } 
		attr = "middleCenter";
		if(attr != null) { alignment = (PlanetUnity2.TextAlignment)Enum.Parse(typeof(PlanetUnity2.TextAlignment), attr); } 

	}
	

	public PUText(
			string font,
			int fontSize,
			PlanetUnity2.FontStyle fontStyle,
			Color fontColor,
			float lineSpacing,
			PlanetUnity2.TextAlignment alignment,
			string value,
			bool sizeToFit,
			int maxFontSize,
			int minFontSize,
			bool vOverflow,
			bool hOverflow,
			string onLinkClick ) : this()
	{
		this.font = font;

		this.fontSize = fontSize;

		this.fontStyle = fontStyle;

		this.fontColor = fontColor;

		this.lineSpacing = lineSpacing;

		this.alignment = alignment;

		this.value = value;

		this.sizeToFit = sizeToFit;

		this.maxFontSize = maxFontSize;

		this.minFontSize = minFontSize;

		this.vOverflow = vOverflow;

		this.hOverflow = hOverflow;

		this.onLinkClick = onLinkClick;
	}

	

	public PUText(
			string font,
			int fontSize,
			PlanetUnity2.FontStyle fontStyle,
			Color fontColor,
			float lineSpacing,
			PlanetUnity2.TextAlignment alignment,
			string value,
			bool sizeToFit,
			int maxFontSize,
			int minFontSize,
			bool vOverflow,
			bool hOverflow,
			string onLinkClick,
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
		this.font = font;

		this.fontSize = fontSize;

		this.fontStyle = fontStyle;

		this.fontColor = fontColor;

		this.lineSpacing = lineSpacing;

		this.alignment = alignment;

		this.value = value;

		this.sizeToFit = sizeToFit;

		this.maxFontSize = maxFontSize;

		this.minFontSize = minFontSize;

		this.vOverflow = vOverflow;

		this.hOverflow = hOverflow;

		this.onLinkClick = onLinkClick;

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




public class PUTextBase : PUGameObject {






	// XML Attributes
	public string raw_font;
	public string font;
	public string raw_fontSize;
	public int? fontSize;
	public string raw_fontStyle;
	public PlanetUnity2.FontStyle? fontStyle;
	public string raw_fontColor;
	public Color? fontColor;
	public string raw_lineSpacing;
	public float? lineSpacing;
	public string raw_alignment;
	public PlanetUnity2.TextAlignment? alignment;
	public string raw_value;
	public string value;
	public string raw_sizeToFit;
	public bool sizeToFit;
	public string raw_maxFontSize;
	public int? maxFontSize;
	public string raw_minFontSize;
	public int? minFontSize;
	public string raw_vOverflow;
	public bool vOverflow;
	public string raw_hOverflow;
	public bool hOverflow;
	public string raw_onLinkClick;
	public string onLinkClick;




	public override void gaxb_unload()
	{
		base.gaxb_unload();

	}

	public new void gaxb_addToParent()

	{
		if(parent != null)
		{
			FieldInfo parentField = parent.GetType().GetField("Text");
			List<object> parentChildren = null;

			if(parentField != null)
			{
				parentField.SetValue(parent, this);
			}
			else
			{
				parentField = parent.GetType().GetField("Texts");

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

	public override void gaxb_loadattrs()
	{
		base.gaxb_loadattrs();


		string attr;
		attr = raw_font;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr != null) { font = unescape(attr); } 
		
		attr = raw_fontSize;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr == null) { attr = "12"; }
		if(attr != null) { fontSize = (int)float.Parse(attr); } 
		
		attr = raw_fontStyle;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr != null) { fontStyle = (PlanetUnity2.FontStyle)Enum.Parse(typeof(PlanetUnity2.FontStyle), attr); } 
		
		attr = raw_fontColor;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr == null) { attr = "0,0,0,1"; }
		if(attr != null) { fontColor = new Color().PUParse(attr); } 
		
		attr = raw_lineSpacing;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr != null) { lineSpacing = float.Parse(attr); } 
		
		attr = raw_alignment;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr == null) { attr = "middleCenter"; }
		if(attr != null) { alignment = (PlanetUnity2.TextAlignment)Enum.Parse(typeof(PlanetUnity2.TextAlignment), attr); } 
		
		attr = raw_value;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr != null) { value = unescape(attr); } 
		
		attr = raw_sizeToFit;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr != null) { sizeToFit = bool.Parse(attr); } 
		
		attr = raw_maxFontSize;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr != null) { maxFontSize = (int)float.Parse(attr); } 
		
		attr = raw_minFontSize;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr != null) { minFontSize = (int)float.Parse(attr); } 
		
		attr = raw_vOverflow;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr != null) { vOverflow = bool.Parse(attr); } 
		
		attr = raw_hOverflow;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr != null) { hOverflow = bool.Parse(attr); } 
		
		attr = raw_onLinkClick;
		if(attr != null) { attr = PlanetUnityOverride.processString(this, parent, attr); }
		if(attr != null) { onLinkClick = unescape(attr); } 
		

	}

	public override void gaxb_load(TBXMLElement element, object _parent, Hashtable args)
	{
		base.gaxb_load(element, _parent, args);

		if(element == null && _parent == null)
			return;

		parent = _parent;

		if(this.GetType() == typeof( PUText ))
		{
			gaxb_addToParent();
		}

		//xmlns = element.GetAttribute("xmlns");

		raw_font = element.GetAttribute("font");		
		raw_fontSize = element.GetAttribute("fontSize");		
		raw_fontStyle = element.GetAttribute("fontStyle");		
		raw_fontColor = element.GetAttribute("fontColor");		
		raw_lineSpacing = element.GetAttribute("lineSpacing");		
		raw_alignment = element.GetAttribute("alignment");		
		raw_value = element.GetAttribute("value");		
		raw_sizeToFit = element.GetAttribute("sizeToFit");		
		raw_maxFontSize = element.GetAttribute("maxFontSize");		
		raw_minFontSize = element.GetAttribute("minFontSize");		
		raw_vOverflow = element.GetAttribute("vOverflow");		
		raw_hOverflow = element.GetAttribute("hOverflow");		
		raw_onLinkClick = element.GetAttribute("onLinkClick");		
		gaxb_loadattrs();
	}







	public override void gaxb_appendXMLAttributes(StringBuilder sb)
	{
		base.gaxb_appendXMLAttributes(sb);

		if(font != null) { sb.AppendFormat (" {0}=\"{1}\"", "font", SecurityElement.Escape (font)); }
		if(fontSize != null) { sb.AppendFormat (" {0}=\"{1}\"", "fontSize", fontSize); }
		if(fontStyle != null) { sb.AppendFormat (" {0}=\"{1}\"", "fontStyle", (int)fontStyle); }
		if(fontColor != null) { sb.AppendFormat (" {0}=\"{1}\"", "fontColor", fontColor.Value.PUToString()); }
		if(lineSpacing != null) { sb.AppendFormat (" {0}=\"{1}\"", "lineSpacing", lineSpacing.Value.ToString ("0.##")); }
		if(alignment != null) { sb.AppendFormat (" {0}=\"{1}\"", "alignment", (int)alignment); }
		if(value != null) { sb.AppendFormat (" {0}=\"{1}\"", "value", SecurityElement.Escape (value)); }
		 sb.AppendFormat (" {0}=\"{1}\"", "sizeToFit", sizeToFit.ToString().ToLower()); 
		if(maxFontSize != null) { sb.AppendFormat (" {0}=\"{1}\"", "maxFontSize", maxFontSize); }
		if(minFontSize != null) { sb.AppendFormat (" {0}=\"{1}\"", "minFontSize", minFontSize); }
		 sb.AppendFormat (" {0}=\"{1}\"", "vOverflow", vOverflow.ToString().ToLower()); 
		 sb.AppendFormat (" {0}=\"{1}\"", "hOverflow", hOverflow.ToString().ToLower()); 
		if(onLinkClick != null) { sb.AppendFormat (" {0}=\"{1}\"", "onLinkClick", SecurityElement.Escape (onLinkClick)); }

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

		sb.AppendFormat ("<{0}", "Text");

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
			sb.AppendFormat (">{0}</{1}>", seq.ToString(), "Text");
		}
	}
}
