package xamrainspike;


public class ViewPageListenerForActionBar
	extends android.support.v4.view.ViewPager.SimpleOnPageChangeListener
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onPageSelected:(I)V:GetOnPageSelected_IHandler\n" +
			"";
		mono.android.Runtime.register ("XamrainSpike.ViewPageListenerForActionBar, XamrainSpike, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ViewPageListenerForActionBar.class, __md_methods);
	}


	public ViewPageListenerForActionBar () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ViewPageListenerForActionBar.class)
			mono.android.TypeManager.Activate ("XamrainSpike.ViewPageListenerForActionBar, XamrainSpike, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ViewPageListenerForActionBar (android.app.ActionBar p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == ViewPageListenerForActionBar.class)
			mono.android.TypeManager.Activate ("XamrainSpike.ViewPageListenerForActionBar, XamrainSpike, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.App.ActionBar, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onPageSelected (int p0)
	{
		n_onPageSelected (p0);
	}

	private native void n_onPageSelected (int p0);

	java.util.ArrayList refList;
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
