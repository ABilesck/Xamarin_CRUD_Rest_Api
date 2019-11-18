using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using RestApi.Resources;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net;

namespace RestApi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        long id;
        ListView lvDados;
        List<Usuario> usuarios = new List<Usuario>();

        EditText txtNome;
        EditText txtTelefone;
        EditText txtEmail;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            

            lvDados = FindViewById<ListView>(Resource.Id.lvDados);

            txtNome = FindViewById<EditText>(Resource.Id.txtNome);
            txtTelefone = FindViewById<EditText>(Resource.Id.txtTelefone);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);

            var btnIncluir = FindViewById<Button>(Resource.Id.btnIncluir);
            var btnEditar = FindViewById<Button>(Resource.Id.btnEditar);
            var btnDeletar = FindViewById<Button>(Resource.Id.btnDeletar);

            //BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            //navigation.SetOnNavigationItemSelectedListener(this);

            CarregarDados();

            btnIncluir.Click += async delegate
            {
                using (var client = new HttpClient())
                {
                    // cria um novo post
                    var novoUsuario = new Usuario
                    {
                        nome = txtNome.Text,
                        telefone = txtTelefone.Text,
                        email = txtEmail.Text
                    };

                    // cria o conteudo da requisição e define o tipo Json
                    var json = JsonConvert.SerializeObject(novoUsuario);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var uri = "https://fprestapi.000webhostapp.com/api/Usuario/Create.php";
                    var result = await client.PostAsync(uri, content);

                    // Se ocorrer um erro lança uma exceção
                    result.EnsureSuccessStatusCode();

                    // processa a resposta
                    var resultString = await result.Content.ReadAsStringAsync();
                    var post = JsonConvert.DeserializeObject<Usuario>(resultString);

                }
            };

            lvDados.ItemClick += (s, e) =>
            {
                for (int i = 0; i < lvDados.Count; i++)
                {
                    if (e.Position == i)
                        lvDados.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Gray);
                    //else
                        //lvDados.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }

                //vinculando dados do listview 
                var lvtxtNome = e.View.FindViewById<TextView>(Resource.Id.txtvNome);
                var lvtxtTel = e.View.FindViewById<TextView>(Resource.Id.txtvTelefone);
                var lvtxtEmail = e.View.FindViewById<TextView>(Resource.Id.txtvEmail);

                txtNome.Text = lvtxtNome.Text;
                id = e.Id;
                txtTelefone.Text = lvtxtTel.Text;
                txtEmail.Text = lvtxtEmail.Text;
            };
    }
//public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
//{
//    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

//    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
//}
//public bool OnNavigationItemSelected(IMenuItem item)
//{
//    switch (item.ItemId)
//    {
//        case Resource.Id.navigation_home:
//            //textMessage.SetText(Resource.String.title_home);
//            return true;
//        case Resource.Id.navigation_dashboard:
//            //textMessage.SetText(Resource.String.title_dashboard);
//            return true;
//    }
//    return false;
//}
private async void CarregarDados()
{

    using (var client = new HttpClient())
    {
        // envia a requisição GET
        var uri = "https://fprestapi.000webhostapp.com/api/Usuario/Read.php";
        var result = await client.GetStringAsync(uri);

        // processa a resposta
        var posts = JsonConvert.DeserializeObject<List<Usuario>>(result);

        usuarios = posts;
        var adapter = new ListViewAdapter(this, usuarios);
        lvDados.Adapter = adapter;
    }
}

private void LimpaCampos()
{
    txtNome.Text = "";
    txtTelefone.Text = "";
    txtEmail.Text = "";
}

private async void Incluir()
{

}
    }
}


