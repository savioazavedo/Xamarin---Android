using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RestSharp;
using System.Threading;
using KinveyXamarin;
using System.Threading.Tasks;
using System.IO;
using SQLite.Net.Platform.XamarinAndroid;
using System.Linq;
using System.Text;
using KinveyUtils;
using KinveyXamarinAndroid;
using Newtonsoft.Json.Linq;


namespace KinveyTest
{
    [Activity(Label = "KinveyTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        private string appKey = "kid_VTNND_fxXq";
        private string appSecret = "ab5bebc75fa147c0885c074cdf48832c";
        Client kinveyClient;

        EditText txtBookName, txtAuthor, txtGenre;
        TextView txtBookId;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button btnAddBook = FindViewById<Button>(Resource.Id.btnAddBook);
            Button btnGetBook = FindViewById<Button>(Resource.Id.btnGetBook);
            Button btnEditBook = FindViewById<Button>(Resource.Id.btnEditBook);
            Button btnDeleteBook = FindViewById<Button>(Resource.Id.btnDeleteBook);

            txtBookName = FindViewById<EditText>(Resource.Id.txtBookName);
            txtAuthor = FindViewById<EditText>(Resource.Id.txtAuthor);
            txtGenre = FindViewById<EditText>(Resource.Id.txtGenre);
            txtBookId = FindViewById<TextView>(Resource.Id.txtBookId);

            kinveyClient = new Client.Builder(appKey, appSecret)
                            .setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
                            .setOfflinePlatform(new SQLitePlatformAndroid())
                            .setLogger(delegate (string msg) { Console.WriteLine(msg); })
                            .build();

           Login();

            btnAddBook.Click += AddBook;
            btnGetBook.Click += GetBook;
            btnDeleteBook.Click += DeleteBook;
            btnEditBook.Click += EditBook;
        }



        private async void Login()
        {
            User activeUser = await kinveyClient.User().LoginAsync();
        }

        private async void AddBook(object sender, EventArgs e)
        {
            Book b = new Book();

            b.BookName = txtBookName.Text;
            b.Author = txtAuthor.Text;
            b.Genre = txtGenre.Text;

            AsyncAppData<Book> myBook = kinveyClient.AppData<Book>("Books", typeof(Book));
            Book saved = await myBook.SaveAsync(b);

            txtBookId.Text = saved.id;
        }

        private async void GetBook(object sender, EventArgs e)
        {
            AsyncAppData<Book> books = kinveyClient.AppData<Book>("Books", typeof(Book));
            Book[] booklist = await books.GetAsync();
            Book tmpBook = booklist.First(p => p.BookName == "Harry Potter");

            txtBookId.Text = tmpBook.id;
            txtAuthor.Text = tmpBook.Author;
            txtGenre.Text = tmpBook.Genre;
            txtBookName.Text = tmpBook.BookName;
        }

        // Works fine as long as the current user is editing his record

        private async void EditBook(object sender, EventArgs e)
        {
            AsyncAppData<Book> books = kinveyClient.AppData<Book>("Books", typeof(Book));
            Book tmpBook = await books.GetEntityAsync(txtBookId.Text);

            tmpBook.Author = txtAuthor.Text;
            tmpBook.Genre = txtGenre.Text;
            tmpBook.BookName = txtBookName.Text;

            await books.SaveAsync(tmpBook);
        }

        private async void DeleteBook(object sender, EventArgs e)
        {
            AsyncAppData<Book> books = kinveyClient.AppData<Book>("Books", typeof(Book));
            KinveyDeleteResponse deleted = await books.DeleteAsync(txtBookId.Text);
        }

    }
}

