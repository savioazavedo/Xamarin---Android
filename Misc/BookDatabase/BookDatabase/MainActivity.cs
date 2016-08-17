using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using System.Linq;

namespace BookDatabase
{
    [Activity(Label = "Grocery", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        AutoCompleteTextView acItem;
        ArrayAdapter listAdapter;
        Button btnAdd,btnHide,btnDelete;
        ListView lvItems;
        SQLiteConnection conn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            lvItems = FindViewById<ListView>(Resource.Id.listView1);
            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnHide = FindViewById<Button>(Resource.Id.btnHide);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            var grocerylist_array = Resources.GetStringArray(Resource.Array.grocerylist);
            acItem = FindViewById<AutoCompleteTextView>(Resource.Id.acItem);
            acItem.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.TestListItem, grocerylist_array);

            btnAdd.Click += BtnAdd_Click;
            btnHide.Click += BtnHide_Click;
            btnDelete.Click += BtnDelete_Click;

            // Initialising a list and binding it to an adapter
            lvItems.ChoiceMode = ChoiceMode.Multiple;
            listAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice);
            lvItems.Adapter = listAdapter;
            lvItems.ItemClick += LvItems_ItemClick;

            InitializeDatabase();


        }

        private void LvItems_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string item = listAdapter.GetItem(e.Position).ToString();

            var obj = conn.Table<Grocery>().First(p => p.Item == item);

              

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var selectedItems = FindViewById<ListView>(Resource.Id.listView1).CheckedItemPositions;

            for (var i = 0; i < selectedItems.Size(); i++)
            {
                if (selectedItems.ValueAt(i) == true && selectedItems.Size() < lvItems.Count)
                {
                    listAdapter.Remove(lvItems.GetItemAtPosition(selectedItems.KeyAt(i)));

                    var item = lvItems.GetItemAtPosition(selectedItems.KeyAt(i)).ToString();

                    conn.Table<Grocery>().Delete(p => p.Item == item);

                    listAdapter.NotifyDataSetChanged();
                }
            }

            lvItems.ClearChoices();
        }

        private void InitializeDatabase()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            conn = new SQLiteConnection(System.IO.Path.Combine(folder, "grocery.db"));
            conn.CreateTable<Grocery>();
            
            //var s = new Grocery { Item = "Test Grocery Item", Price = 0, Time = System.DateTime.Now };
            //conn.Insert(s);

            ShowAllGroceryItems();
        }

        private void ShowAllGroceryItems()
        {
            var query = conn.Table<Grocery>().Select(p => p);
            listAdapter.Clear();
            
            foreach (var item in query)
            {
                
                listAdapter.Add(item.Item);
            }

            listAdapter.NotifyDataSetChanged();
        }

        private void BtnHide_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (acItem.Text.Length > 0)
            {
               
                var s = new Grocery { Item = acItem.Text , Price = 0 , Time = System.DateTime.Now };
                conn.Insert(s);
                Console.WriteLine("{0} == {1}", s.Item, s.Id);
                ShowAllGroceryItems();
                acItem.Text = "";
            }
        }
    }
}

