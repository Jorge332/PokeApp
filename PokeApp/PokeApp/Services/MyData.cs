using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApp.Services
{
    public class MyData
    {
        #region Properties
        public static Pokemon Pokemon { get; set; }
        public List<string> Sugestions { get; set; }
        #endregion
        public MyData()
        {
            Sugestions = new List<string>();
            Sugestions.Add("Azul");
            Sugestions.Add("Rojo");
            Sugestions.Add("Verde");
            Sugestions.Add("Naranja");
            Sugestions.Add("Purpura");
        }
    }
}
