package md53ff4aa3c6deb1225528077d2ecc39ec0;


public class ActivityMain
	extends md59e336b20c5f59a4196ec0611a339f132.AndroidGameActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("LightsOut2.ActivityMain, LightsOut2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ActivityMain.class, __md_methods);
	}


	public ActivityMain () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ActivityMain.class)
			mono.android.TypeManager.Activate ("LightsOut2.ActivityMain, LightsOut2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
