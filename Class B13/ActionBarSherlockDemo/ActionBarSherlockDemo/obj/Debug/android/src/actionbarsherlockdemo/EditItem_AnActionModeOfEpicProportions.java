package actionbarsherlockdemo;


public class EditItem_AnActionModeOfEpicProportions
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.actionbarsherlock.view.ActionMode.Callback
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onActionItemClicked:(Lcom/actionbarsherlock/view/ActionMode;Lcom/actionbarsherlock/view/MenuItem;)Z:GetOnActionItemClicked_Lcom_actionbarsherlock_view_ActionMode_Lcom_actionbarsherlock_view_MenuItem_Handler:Xamarin.ActionbarSherlockBinding.Views.ActionMode/ICallbackInvoker, ActionBarSherlock\n" +
			"n_onCreateActionMode:(Lcom/actionbarsherlock/view/ActionMode;Lcom/actionbarsherlock/view/Menu;)Z:GetOnCreateActionMode_Lcom_actionbarsherlock_view_ActionMode_Lcom_actionbarsherlock_view_Menu_Handler:Xamarin.ActionbarSherlockBinding.Views.ActionMode/ICallbackInvoker, ActionBarSherlock\n" +
			"n_onDestroyActionMode:(Lcom/actionbarsherlock/view/ActionMode;)V:GetOnDestroyActionMode_Lcom_actionbarsherlock_view_ActionMode_Handler:Xamarin.ActionbarSherlockBinding.Views.ActionMode/ICallbackInvoker, ActionBarSherlock\n" +
			"n_onPrepareActionMode:(Lcom/actionbarsherlock/view/ActionMode;Lcom/actionbarsherlock/view/Menu;)Z:GetOnPrepareActionMode_Lcom_actionbarsherlock_view_ActionMode_Lcom_actionbarsherlock_view_Menu_Handler:Xamarin.ActionbarSherlockBinding.Views.ActionMode/ICallbackInvoker, ActionBarSherlock\n" +
			"";
		mono.android.Runtime.register ("ActionBarSherlockDemo.EditItem/AnActionModeOfEpicProportions, ActionBarSherlockDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", EditItem_AnActionModeOfEpicProportions.class, __md_methods);
	}


	public EditItem_AnActionModeOfEpicProportions () throws java.lang.Throwable
	{
		super ();
		if (getClass () == EditItem_AnActionModeOfEpicProportions.class)
			mono.android.TypeManager.Activate ("ActionBarSherlockDemo.EditItem/AnActionModeOfEpicProportions, ActionBarSherlockDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public EditItem_AnActionModeOfEpicProportions (actionbarsherlockdemo.EditItem p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == EditItem_AnActionModeOfEpicProportions.class)
			mono.android.TypeManager.Activate ("ActionBarSherlockDemo.EditItem/AnActionModeOfEpicProportions, ActionBarSherlockDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "ActionBarSherlockDemo.EditItem, ActionBarSherlockDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public boolean onActionItemClicked (com.actionbarsherlock.view.ActionMode p0, com.actionbarsherlock.view.MenuItem p1)
	{
		return n_onActionItemClicked (p0, p1);
	}

	private native boolean n_onActionItemClicked (com.actionbarsherlock.view.ActionMode p0, com.actionbarsherlock.view.MenuItem p1);


	public boolean onCreateActionMode (com.actionbarsherlock.view.ActionMode p0, com.actionbarsherlock.view.Menu p1)
	{
		return n_onCreateActionMode (p0, p1);
	}

	private native boolean n_onCreateActionMode (com.actionbarsherlock.view.ActionMode p0, com.actionbarsherlock.view.Menu p1);


	public void onDestroyActionMode (com.actionbarsherlock.view.ActionMode p0)
	{
		n_onDestroyActionMode (p0);
	}

	private native void n_onDestroyActionMode (com.actionbarsherlock.view.ActionMode p0);


	public boolean onPrepareActionMode (com.actionbarsherlock.view.ActionMode p0, com.actionbarsherlock.view.Menu p1)
	{
		return n_onPrepareActionMode (p0, p1);
	}

	private native boolean n_onPrepareActionMode (com.actionbarsherlock.view.ActionMode p0, com.actionbarsherlock.view.Menu p1);

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
