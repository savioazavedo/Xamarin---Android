package facebooklogin;


public class UserDetails
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.facebook.Session.StatusCallback,
		com.facebook.Request.GraphUserCallback
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_call:(Lcom/facebook/Session;Lcom/facebook/SessionState;Ljava/lang/Exception;)V:GetCall_Lcom_facebook_Session_Lcom_facebook_SessionState_Ljava_lang_Exception_Handler:Xamarin.Facebook.Session/IStatusCallbackInvoker, Xamarin.Facebook\n" +
			"n_onCompleted:(Lcom/facebook/model/GraphUser;Lcom/facebook/Response;)V:GetOnCompleted_Lcom_facebook_model_GraphUser_Lcom_facebook_Response_Handler:Xamarin.Facebook.Request/IGraphUserCallbackInvoker, Xamarin.Facebook\n" +
			"";
		mono.android.Runtime.register ("FacebookLogin.UserDetails, FacebookLogin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", UserDetails.class, __md_methods);
	}


	public UserDetails () throws java.lang.Throwable
	{
		super ();
		if (getClass () == UserDetails.class)
			mono.android.TypeManager.Activate ("FacebookLogin.UserDetails, FacebookLogin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void call (com.facebook.Session p0, com.facebook.SessionState p1, java.lang.Exception p2)
	{
		n_call (p0, p1, p2);
	}

	private native void n_call (com.facebook.Session p0, com.facebook.SessionState p1, java.lang.Exception p2);


	public void onCompleted (com.facebook.model.GraphUser p0, com.facebook.Response p1)
	{
		n_onCompleted (p0, p1);
	}

	private native void n_onCompleted (com.facebook.model.GraphUser p0, com.facebook.Response p1);

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
