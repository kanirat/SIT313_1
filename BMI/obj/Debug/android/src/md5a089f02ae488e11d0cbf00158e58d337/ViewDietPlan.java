package md5a089f02ae488e11d0cbf00158e58d337;


public class ViewDietPlan
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("BMI.ViewDietPlan, BMI", ViewDietPlan.class, __md_methods);
	}


	public ViewDietPlan ()
	{
		super ();
		if (getClass () == ViewDietPlan.class)
			mono.android.TypeManager.Activate ("BMI.ViewDietPlan, BMI", "", this, new java.lang.Object[] {  });
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
