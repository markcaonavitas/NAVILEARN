package crc644eace79156ff983a;


public class Adapter
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("NavitasBeta.Droid.Adapter, NavitasBeta.Android", Adapter.class, __md_methods);
	}


	public Adapter ()
	{
		super ();
		if (getClass () == Adapter.class) {
			mono.android.TypeManager.Activate ("NavitasBeta.Droid.Adapter, NavitasBeta.Android", "", this, new java.lang.Object[] {  });
		}
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
