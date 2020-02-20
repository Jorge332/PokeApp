using Newtonsoft.Json;
using PokeApiNet;
using PokeApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("MyPokemon", "pokemon")]
    public partial class PokemonDetailsPage : ContentPage
    {
        public PokemonDetailsPage()
        {
            InitializeComponent();
            BindingContext = MyData.Pokemon;
        }
    }
}