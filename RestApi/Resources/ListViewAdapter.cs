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

namespace RestApi.Resources
{
    class ListViewAdapter : BaseAdapter
    {
        private readonly Activity _context;
        private readonly List<Usuario> _usuarios;

        public ListViewAdapter(Activity context, List<Usuario> usuarios)
        {
            _context = context;
            _usuarios = usuarios;
        }

        public override int Count
        {
            get
            {
                return _usuarios.Count;
            }
        }

        public override long GetItemId(int position)
        {
            int id = Convert.ToInt32(_usuarios[position].Id);
            return id;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ListViewLayout, parent, false);

            var lvtxtNome = view.FindViewById<TextView>(Resource.Id.txtvNome);
            var lvtxtTelefone = view.FindViewById<TextView>(Resource.Id.txtvTelefone);
            var lvtxtEmail = view.FindViewById<TextView>(Resource.Id.txtvEmail);

            lvtxtNome.Text = _usuarios[position].nome;
            lvtxtTelefone.Text = "" + _usuarios[position].telefone;
            lvtxtEmail.Text = _usuarios[position].email;

            return view;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
    }
}