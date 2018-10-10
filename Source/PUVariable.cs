using UnityEngine;
using UnityEngine.UI;

public partial class PUVariable : PUVariableBase
{
	public override void gaxb_init ()
	{
		base.gaxb_init ();

	    Canvas.GetVariable(key).Value = int.Parse(value);
    }
}
