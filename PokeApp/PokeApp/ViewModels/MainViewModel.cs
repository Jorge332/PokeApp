﻿using Newtonsoft.Json;
using PokeApiNet;
using PokeApp.Services;
using PokeApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PokeApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties

        public List<String> Sugestions { get; set; }
        public ICommand mySearchCommand { get; }

        PokeApiClient client;
        
        private string Message;

        public string LabelMessage
        {
            get => Message;
            set
            {
                if (Message == value)
                    return;
                Message = value;
                OnPropertyChanged(nameof(LabelMessage));
            }
        }


        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading == value)
                    return;
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private bool _isWaiting;

        public bool IsNotWaiting
        {
            get => _isWaiting;
            set
            {
                if (_isWaiting == value)
                    return;
                _isWaiting = value;
                OnPropertyChanged(nameof(IsNotWaiting));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        public MainViewModel()
        {
            Sugestions = GetData();

            IsNotWaiting = true;
            client = new PokeApiClient();

            mySearchCommand = new Command<string>((string name) => { SearchForPokemon(name); });

        }

        private List<string> GetData()
        {
            var Lista = new List<string>();

            try
            {
                string jsonFileName = "PokeData.json";
                var assembly = typeof(MainPage).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Services.{jsonFileName}");
                using (var reader = new System.IO.StreamReader(stream))
                {
                    var jsonString = reader.ReadToEnd();

                    //Converting JSON Array Objects into generic list    
                    Lista = JsonConvert.DeserializeObject<List<String>>(jsonString);
                }
            }
            catch (Exception e)
            {

            }

            return Lista;
        }

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private async void SearchForPokemon(string name)
        {
            IsLoading = true;
            IsNotWaiting = false;

            try
            {
                var MyPokemon = await client.GetResourceAsync<Pokemon>(name);
                MyData.Pokemon = MyPokemon;

                await Shell.Current.GoToAsync("pokemondetails");

            }
            catch (HttpRequestException e)
            {
                if (e.Message.Contains("404 (Not Found)"))
                {
                    LabelMessage = "Couldn't find a pokemon with that name, please try again";
                }

                Console.WriteLine("Exception: {0}", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            IsNotWaiting = true;
            IsLoading = false;

        }
    }
}
