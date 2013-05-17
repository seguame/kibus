//  
//  Nivel3.cs
//  
//  Author:
//       Miguel Seguame Reyes <seguame@outlook.com>
// 
//  Copyright (c) 2013 Miguel Seguame Reyes
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Graficos;
using System.Collections.Generic;
using Utileria;
using Esedelish;
using Algoritmos;
using Tao.Sdl;


namespace Niveles
{
	internal class Nivel3 : Nivel
	{
		private const int ABEJAS = 5;
		private const int RITUALES = 5;
		private Abeja[] abejinis;
		private int[][] temperatura;
		private int[][] mapa;
		
		public Nivel3 ()
		{
			abejinis = new Abeja[ABEJAS];
			new CargadorMapas().SeleccionarArchivo();
		}
		
		public override void Iniciar()
		{
			mapa = CargadorMapas.Mapa;
			
			//TODO:
			//Quitar de todos el tamaño fijo
			for(int i = 0; i < 20; i++)
			{
				for(int j = 0; j < 20; j++)
				{
					if(mapa[i][j] == EditorMapas.CASA)
					{
						Console.WriteLine("Casa en {0},{1}", i, j);
						this.casa = sprites[i,j] = new Sprite("Assets/GFX/casini.png");
					}
					else if(mapa[i][j] != 0)
					{
						Console.WriteLine("{2} en {0},{1}", i, j, mapa[i][j]);
						sprites[i,j] = new Sprite("Assets/GFX/"+mapa[i][j]+".png");
						mapa[i][j] = -1;
					}
					
					if(sprites[i,j] != null)
					{
						sprites[i,j].Mover((short)(i * sprites[i,j].Ancho),(short) (j * sprites[i,j].Alto));
					}
				}
			}
			
			PosicionarKibus();
			
			CalentarCeldas();
			
			BuscarCasa();
		}
		
		private void BuscarCasa()
		{
			Random random = new Random(System.DateTime.Now.Millisecond);
			Direccion direccion;
			Abeja elegible;
			
			//Inicializar abejas
			for(int i = 0; i < ABEJAS; i++)
			{
				abejinis[i] = new Abeja(kibus.X, kibus.Y);
			}
			
			elegible = new Abeja(0,0);
			elegible.Visible = false;
			
			do
			{
				//Mover a las abejas donde está kibus
				foreach(Abeja abejini in abejinis)
				{
					abejini.Mover(kibus.X, kibus.Y);
				}
				
				DibujarTodo();
				Hardware.EscribirTexto("Buscando Casa", 641, 10); 
				Hardware.RefrescarPantalla();
				
				//Que cada una intente moverse 5 veces para encontrar lo mas calido
				for(int i = 0; i < RITUALES; i++)
				{
					foreach(Abeja abejini in abejinis)
					{
						direccion = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
						
						while(!IntentarMover(abejini, direccion, false))
						{
							direccion = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
						}
						
						//TODO: elegir la temperatura 
						abejini.Mover(direccion, temperatura[abejini.OnToyX][abejini.OnToyY]);
					}
					DibujarTodo();
					Hardware.RefrescarPantalla();
					Hardware.Pausar(10);
				}
				
				//Iterar entre todas las abejas y elegir la que alcanzó mayor temperatura.
				foreach(Abeja abejini in abejinis)
				{
					if(abejini.TemperaturaAlcanzada > elegible.TemperaturaAlcanzada)
					{
						elegible = abejini;
					}
				}
				
				elegible.GenerarTrayectoria();
				
				while(elegible.Propagacion.Count != 0)
				{
					kibus.Mover(elegible.Propagacion.Dequeue());
					DibujarTodo();
					Hardware.RefrescarPantalla();
					Hardware.Pausar(50);
				}
				
				//reiniciar recorridos
				foreach(Abeja abejini in abejinis)
				{
					abejini.reiniciarRetropropagacion();
				}
				
				
				if(kibus.OnToyX == casa.OnToyX && kibus.OnToyY == casa.OnToyY)
					break;
				Console.WriteLine("Repetir");
			}while(true);
		}
		
		private void CalentarCeldas()
		{
			temperatura = new int[20][];
				
			for(int i = 0; i < 20; i++)
			{
				temperatura[i] = new int[20];
			}
			
			temperatura[this.casa.OnToyX][ this.casa.OnToyY] = 300;
			
			Amplitud(this.casa.OnToyX, this.casa.OnToyY + 1, temperatura[this.casa.OnToyX][this.casa.OnToyY]);
			
			
			for(int i = 0; i < 20; i++)
			{
				for(int j = 0; j < 20; j++)
				{
					/*Sprite sprite = new Sprite("Assets/GFX/32.png");
					sprite.Mover((short)(i * sprite.Ancho),(short) (j * sprite.Alto));
					Hardware.DibujarCuadroColor(sprite, (byte) temperatura[i][j], 0, 0);
					Hardware.RefrescarPantalla();
					Hardware.Pausar(20);*/
					Console.Write("{0}, ", temperatura[i][j]);
				}
				Console.WriteLine("");
			}
		}
		
		private void Amplitud (int x, int y, int tempActual)
		{
			if (mapa [x][y] == -1) return;
			temperatura[x][y] = tempActual - 1;
			
			if (x > 0) 
			{
				if (temperatura[x - 1][y] == 0) Amplitud (x - 1, y, temperatura[x][y]);
			}
			
			if (x + 1 < 20) {
				if (temperatura[x + 1][y] == 0) Amplitud (x + 1, y, temperatura[x][y]);
			}
			
			if (y > 0) {
				if (temperatura[x][y - 1] == 0) Amplitud (x, y - 1, temperatura[x][y]);
			}
			
			if (y + 1 < 20) {
				if (temperatura[x][y + 1] == 0) Amplitud (x, y + 1, temperatura[x][y]);
			}
		}
		
		protected override void DibujarTodo()
		{
			Hardware.DibujarFondo();
			
			DibujarObstaculos();
			kibus.Dibujar();
			DibujarAbejinis();
			casa.Dibujar();
			
			Hardware.RefrescarPantalla();
		}
		
		private void DibujarAbejinis()
		{
			foreach(Abeja abejini in abejinis)
			{
				abejini.Dibujar();
			}
		}
	}
}
