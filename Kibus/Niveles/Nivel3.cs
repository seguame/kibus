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
		private Abeja[] abejinis;
		private int[][] temperatura;
		private int[][] mapa;
		
		public Nivel3 ()
		{
			abejinis = new Abeja[5];
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
		}
		
		private void BuscarCasa()
		{
			foreach(Abeja abejini in abejinis)
			{
				
			}
		}
		
		private void CalentarCeldas()
		{
			temperatura = new int[20][];
				
			for(int i = 0; i < 20; i++)
			{
				temperatura[i] = new int[20];
			}
			
			temperatura[this.casa.OnToyX][ this.casa.OnToyY] = 255;
			
			Amplitud(this.casa.OnToyX, this.casa.OnToyY + 1, temperatura[this.casa.OnToyX][this.casa.OnToyY]);
			
			
			for(int i = 0; i < 20; i++)
			{
				for(int j = 0; j < 20; j++)
				{
					/*Sprite sprite = new Sprite("Assets/GFX/32.png");
					sprite.Mover((short)(i * sprite.Ancho),(short) (j * sprite.Alto));
					Hardware.DibujarCuadroColor(sprite, (byte) temperatura[i][j], 0, 0);
					Hardware.RefrescarPantalla();*/
					Console.Write("{0}, ", temperatura[i][j]);
				}
				Console.WriteLine("");
			}
		}
		
		private void Amplitud (int x, int y, int tempActual)
		{
			if (mapa [x][y] == -1) return;
			temperatura[x][y] = tempActual - 1;
			//mapa[x][y] = 1;
			
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
	}
}

