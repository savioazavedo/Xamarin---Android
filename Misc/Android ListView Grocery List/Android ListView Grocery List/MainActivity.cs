using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using SQLite.Net;
using System.Linq;
using System.Collections.Generic;

namespace Android_ListView_Grocery_List
{
    [Activity(Label = "Android_ListView_Grocery_List", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        List<int> lstSelected;
        List<int> lstHidden;
        List<Grocery> myList;
        AutoCompleteTextView acItems;
        Button btnAdd, btnHide, btnDelete;
        ListView lvItems, lvPrice;
        ArrayAdapter listAdapter, listAdapter2;     
        SQLite.SQLiteConnection conn;
        int id = 0;
        bool Hide = false;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            lvItems = FindViewById<ListView>(Resource.Id.lvItems);
            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            btnHide = FindViewById<Button>(Resource.Id.btnHide);
            var grocerylist_array = Resources.GetStringArray(Resource.Array.grocerylist);

            acItems = FindViewById<AutoCompleteTextView>(Resource.Id.acItems);
            acItems.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.TestListItem, grocerylist_array);
            
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnHide.Click += BtnHide_Click;

            lvItems.ChoiceMode = ChoiceMode.Multiple;
            listAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice);
            lvItems.Adapter =listAdapter;

            lvItems.ItemClick += LvItems_ItemClick;
            lstHidden = new List<int>();
            lstSelected = new List<int>();
            myList = new List<Grocery>();
            InitializeDatabase();

            btnHide.LongClick += BtnHide_LongClick;

            


            //
            lvPrice = FindViewById<ListView>(Resource.Id.lvPrice);
            lvPrice.ChoiceMode = ChoiceMode.Single;
            listAdapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1);
            lvPrice.Adapter = listAdapter2;
            lvPrice.ItemClick += LvPrice_Click;
            //
        }

        private void BtnHide_LongClick(object sender, View.LongClickEventArgs e)
        {
            lstHidden.Clear();
            ShowAllGroceryItems();
        }

        private void InitializeDatabase()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            conn = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, "grocery.db"));
            conn.CreateTable<Grocery>();

            //var s = new Grocery { Item = "Test Grocery Item", Price = 0, Time = System.DateTime.Now, Completed = false };
            //conn.Insert(s);
            ShowAllGroceryItems();


        }

        private void ShowAllGroceryItems()
        {
          var query = conn.Table<Grocery>().Select(p => p);
         
                listAdapter.Clear();
                myList.Clear();

                foreach (var item in query)
                {              
                    listAdapter.Add(item.Item + "\n" + item.Time);
                    myList.Add(item);
                }
            foreach (var item in query)
            {
                foreach (int h in lstHidden)
                {
                    if (h == item.Id)
                    {
                        listAdapter.Remove(item.Item + "\n" + item.Time);
                    }
                }
            }        
                listAdapter.NotifyDataSetChanged();
            lstSelected.Clear();
            
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (acItems.Text.Length > 0)
            {
                var s = new Grocery { Item = acItems.Text, Price = 0, Time = System.DateTime.Now, Completed = false };
                conn.Insert(s);
                Console.WriteLine("{0} == {1}", s.Item, s.Id);
                ShowAllGroceryItems();
                acItems.Text = "";          
            }
        }
        private void LvItems_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            bool Doubleup = false;
            var GroceryItem = myList[e.Position];
            id = GroceryItem.Id;


            foreach (int item in lstSelected)
            {
                if (id == item)
                {
                    Doubleup = true;
                }
            }

            if (Doubleup == false)
            {
                lstSelected.Add(id);
            }
            else if(Doubleup == true)
            {
                lstSelected.Remove(id);
            }
        }

        private void BtnHide_Click(object sender, EventArgs e)
        {
            foreach (int item in lstSelected)
            {
                lstHidden.Add(item);
            }
            ShowAllGroceryItems();
            lvItems.ClearChoices();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (myList != null)
            {
                foreach (int item in lstSelected)
                {
                    conn.Delete<Grocery>(item);
                }
            }
            ShowAllGroceryItems();
            acItems.Text = "";
            lstSelected.Clear();
            lvItems.ClearChoices(); 
        }

        private void LvPrice_Click(object sender, AdapterView.ItemClickEventArgs e)
        {

        }
    
    }
}

