using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KinveyXamarin;
using SQLite.Net.Platform.XamarinAndroid;

namespace KinveyLogin
{
    [Activity(Label = "ToDoActivity")]
    public class ToDoActivity : Activity
    {
        Button btnAdd, btnEdit, btnRefresh, btnDelete;
        TextView txtTitle,txtDescription;
        ListView lstItems;


        private string appKey = "kid_VTNND_fxXq";
        private string appSecret = "ab5bebc75fa147c0885c074cdf48832c";
        Client kinveyClient;
        User myUser;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.ToDo);

            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            btnRefresh = FindViewById<Button>(Resource.Id.btnRefresh);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete); 

            txtTitle = FindViewById<EditText>(Resource.Id.txtTitle);
            txtDescription = FindViewById<EditText>(Resource.Id.txtDescription);

            lstItems = FindViewById<ListView>(Resource.Id.listView1);

            kinveyClient = new Client.Builder(appKey, appSecret)
                            .setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
                            .setOfflinePlatform(new SQLitePlatformAndroid())
                            .setLogger(delegate (string msg) { Console.WriteLine(msg); })
                            .build();

            myUser = kinveyClient.CurrentUser;

            btnAdd.Click += BtnAdd_Click;

            GetAllItems();

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddItem();
           
        }

        public async void AddItem()
        {
            ToDo item = new ToDo();

            item.Title = txtTitle.Text;
            item.Description = txtDescription.Text;
            item.CreatedBy = myUser;

            AsyncAppData<ToDo> myBook = kinveyClient.AppData<ToDo>("tblToDo", typeof(ToDo));
            ToDo saved = await myBook.SaveAsync(item);
            GetAllItems();
        }

        public void EditItem()
        {

        }

        public void DeleteItem()
        {

        }

        public async void GetAllItems()
        {
            AsyncAppData<ToDo> item = kinveyClient.AppData<ToDo>("tblToDo", typeof(ToDo));
            ToDo[] itemlist = await item.GetAsync();

            List<ToDo> templist = itemlist.Where(p => p.CreatedBy.Id == myUser.Id).ToList();
            lstItems.Adapter = new DataAdapter(this, templist);

        }

    }
}