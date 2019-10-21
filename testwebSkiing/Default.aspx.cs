using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testwebSkiing
{
    
    public partial class _Default : Page
    {
        public static int MaxCaida { get; set; }
        public static int MaxDistancia { get; set; }
        public static int Pasoanterior { get; set; }

        public static List<int> lista { get; set; } = new List<int>();
        public static List<string> listarutas { get; set; } = new List<string>();

        public static string Ruta { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int m;
            MaxCaida = 0;
            MaxDistancia = 0;
            Pasoanterior = 0;
            
            lista.Clear();

            string FilePath = Server.MapPath("/Mapas/4x4.txt");
            string textFile = FilePath;
            string[] lines = File.ReadAllLines(textFile);
            string linea = "";
            m = Convert.ToInt32(lines[0].Split(' ')[0]);

            int[,] mapa = new int[m, m];

            //for (let i = 0; i < restData.length; i++)
            //{
            //    this.map[i] = splitString(restData[i]);
            //}
            for (int i = 0; i < lines.Length; i++)
            {
                var numeros = (lines[i].Split(' '));
                int aux = 0;
                if (i != 0)
                {
                    foreach (var numero in numeros)
                    {
                        mapa[i-1, aux] = Convert.ToInt32(numero);
                        aux++;
                    }
                }

            }


            for (var x = 0; x < m; x++)
            {
                for (var y = 0; y < m; y++)
                {
                    //if the maxLength is greater then current value, no need to traverse
                    if (MaxDistancia < mapa[x, y])
                    {                        

                         traverse(x, y, 1, mapa[x, y],mapa,m);
                       listarutas.Add(string.Join<int>(",", lista));
                        lista.Clear();
                    }
                }
            }
            List<string> rutasfinal = new List<string>();
            foreach(var ruta in listarutas)
            {            
                var ruta1n = ruta.Split(Convert.ToChar((ruta.Substring(0, 1))));
                foreach(var x in ruta1n)
                {
                    if (x.Split(',').Select(tag => tag.Trim()).Where(tag => !string.IsNullOrEmpty(tag)).Distinct().Count() == MaxDistancia - 2)
                    {
                        var y = x.Split(',').Distinct();

                        rutasfinal.Add(ruta.Substring(0, 1) + string.Join<string>(",", y));
                    }
                }

            }

          



            TextBox1.Text = "Distancia: " + MaxDistancia + " caida:" + MaxCaida +". Ruta: "+ rutasfinal.FirstOrDefault();
        }

        private void traverse(int x, int y, int distancia, int inicio,int[,] mapa,int tamaño)
        {
            //consider as x y axis, instead of doing 4 if block. we can think about it as
            //current point [x,y]
            //go up [x, y + 1], go right [x + 1, y], go down [x, y - 1], go left [x - 1, y]
            int [] xAxis = { 0, 1, 0, -1 };
            int [] yAxis = { 1, 0, -1, 0 };

            for (var k = 0; k < 4; k++)
            {
                //check if the moving is still inside the graph
                var isInsideGraph = x + xAxis[k] >= 0 && x + xAxis[k] < tamaño && y + yAxis[k] >= 0 && y + yAxis[k] < tamaño;
                if (isInsideGraph && (mapa[x, y] > mapa[x + xAxis[k],y + yAxis[k]]))
                {
            
                    //if can traverse and the current value is bigger the the next traverse point.
                    //set the length and keep the start point. to calculate the maxlength and drop later.

                     lista.Add(mapa[x, y]);
                    traverse(x + xAxis[k], y + yAxis[k], distancia + 1, inicio,mapa, tamaño);
                   
                }
            }
            //current drop
            var caida = inicio - mapa[x,y];
            if (distancia > MaxDistancia)
            {
                Ruta = inicio.ToString();
                MaxDistancia = distancia;
                MaxCaida = caida;
            }

            //the use case where by length is the same but the drop is greater
            if (distancia == MaxDistancia && MaxCaida < caida)
            {
                MaxCaida = caida;
            }
            
        }
    }
}