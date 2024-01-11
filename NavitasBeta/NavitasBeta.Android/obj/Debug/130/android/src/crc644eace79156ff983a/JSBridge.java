package crc644eace79156ff983a;


public class JSBridge
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_InvokeAction:(Ljava/lang/String;)V:__export__\n" +
			"";
		mono.android.Runtime.register ("NavitasBeta.Droid.JSBridge, NavitasBeta.Android", JSBridge.class, __md_methods);
	}


	public JSBridge ()
	{
		super ();
		if (getClass () == JSBridge.class) {
			mono.android.TypeManager.Activate ("NavitasBeta.Droid.JSBridge, NavitasBeta.Android", "", this, new java.lang.Object[] {  });
		}
	}

	public JSBridge (crc644eace79156ff983a.HybridWebViewRenderer p0)
	{
		super ();
		if (getClass () == JSBridge.class) {
			mono.android.TypeManager.Activate ("NavitasBeta.Droid.JSBridge, NavitasBeta.Android", "NavitasBeta.Droid.HybridWebViewRenderer, NavitasBeta.Android", this, new java.lang.Object[] { p0 });
		}
	}

	@android.webkit.JavascriptInterface

	public void invokeAction (java.lang.String p0)
	{
		n_InvokeAction (p0);
	}

	private native void n_InvokeAction (java.lang.String p0);

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
