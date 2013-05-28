//  
//  Nivel4.cs
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
using Utileria;
using Esedelish;
using Tao.Sdl;

namespace Niveles
{
	internal class Nivel4 : Nivel
	{
		private int[][] mapa;
		bool modoGrafico;
		
		public Nivel4 ()
		{
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
						requierePosicionarCasa = true;
						//Console.WriteLine("Casa en {0},{1}", i, j);
						//this.casa = sprites[i,j] = new Sprite("Assets/GFX/casini.png");
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
			
			ConfeccionarCamino();
		}
		
		private void ConfeccionarCamino()
		{
			Direccion direccion;
			Random random = new Random(System.DateTime.Now.Millisecond);
			
			Console.WriteLine("CASA: {0},{1}", casa.OnToyX,casa.OnToyY);
			Console.WriteLine("KIBU: {0},{1}", kibus.OnToyX,kibus.OnToyY);
			
			while(kibus.OnToyX != casa.OnToyX || kibus.OnToyY != casa.OnToyY)
			{
				modoGrafico = !Hardware.TeclaPulsada(Sdl.SDLK_g);
				
				if(modoGrafico)
				{
					DibujarTodo();
					Hardware.EscribirTexto("Entrenando", 641, 10); 
					Hardware.RefrescarPantalla();
				}
				
				do
				{
					
					direccion = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
					
				}while(!IntentarMover(kibus, direccion, false));
				
				kibus.Mover(direccion);
				
				if(modoGrafico)
				{
					Hardware.Pausar(25);
				}
				else
				{
					//Hardware.Pausar(2);
				}
			}
			
			Console.WriteLine("CASA: {0},{1}", casa.OnToyX,casa.OnToyY);
			Console.WriteLine("KIBU: {0},{1}", kibus.OnToyX,kibus.OnToyY);
		}
	}
}

