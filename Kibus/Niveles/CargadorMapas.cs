//  
//  CargadorMapas.cs
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
using System.IO;

using Tao.Sdl;

using Esedelish;
using System.Collections.Generic;

namespace Niveles
{
	public class CargadorMapas
	{
		private const string rutaMapas = "Mapas/";
		public static int[][] Mapa
		{
			private set;
			get;
		}
		
		private bool hayHarchivos;
		
		private string[] archivos;
		
		public CargadorMapas ()
		{
			LeerCarpetaMapas();
		}
		
		public void SeleccionarArchivo()
		{
			Sdl.SDL_Event evento;
			int seleccion = -1;
			
			if(!hayHarchivos)
			{
				Hardware.DibujarFondo(0,0,150);
				Hardware.EscribirTexto("Crea primero algun mapa", 50, 50);
				Hardware.RefrescarPantalla();
				Hardware.Pausar(1000);

				new EditorMapas().Iniciar();
				LeerCarpetaMapas();
			}
			
			
			do
			{
				Hardware.DibujarFondo(0,0,150);
				ListarArchivos();
				Hardware.RefrescarPantalla();
				
				
				while(Sdl.SDL_PollEvent(out evento) > 0)
				{
					
					/*Temporal, servirá de momento en lo que se crea
					 * la clase que permitira clikear sobre el texto 
					 * para seleccionar el mapa
					 */
					switch(evento.type)
					{
						case Sdl.SDL_KEYDOWN:
							seleccion = evento.key.keysym.sym - 48;
							break;
					}
				}
				
				Hardware.Pausar(300);
				
			}while(seleccion == -1);
			
			MapearSeleccion(seleccion);
		}

		private void LeerCarpetaMapas()
		{
			archivos = Directory.GetFiles(rutaMapas,"*.txt");
			
			if(archivos.Length == 0)
			{
				hayHarchivos = false;
			}
			else
			{
				hayHarchivos = true;
			}
		}
		
		private void ListarArchivos()
		{
			for(int i = 0; i < archivos.Length; i++)
			{
				Hardware.EscribirTexto(i+": "+archivos[i], 5, (i * 30));
			}
		}
		
		private void MapearSeleccion(int indice)
		{
			char[] separadores = new char[]{','};
			
			try
			{
				StreamReader lector = new StreamReader(archivos[indice]);
				List<string[]> lineas = new List<string[]>();
				string linea;
				string[] fila;
				
				while((linea = lector.ReadLine()) != null)
				{
					fila = linea.Split(separadores);
					lineas.Add(fila);
				}
				
				ParsearMapa(lineas);
			}
			catch (IndexOutOfRangeException)
			{
				Random random = new Random(System.DateTime.Now.Millisecond);
				int cantidad = 100;
				
				int[][] mapa = new int[20][];
				
				for(int i = 0; i < 20; i++)
				{
					mapa[i] = new int[20];
				}
				
				int x;
				int y;
				int elemento;
				
				while(cantidad > 0)
				{
					x = random.Next(20);
					y = random.Next(20);
					elemento = random.Next(36,42);
					if(mapa[x][y] == 0)
					{
						mapa[x][y] = elemento;
						cantidad--;
					}
					           
				}
				
				Mapa = mapa;
			}
		}
		
		private void ParsearMapa(List<string[]> tmp)
		{
			int filas = tmp.Count;
			int columnas;
			int[][] mapa = new int[filas][];
			
			try
			{
				for(int i = 0; i < filas; i++)
				{
					columnas = tmp[i].Length;
					mapa[i] = new int[columnas];
					
					for(int j = 0; j < columnas; j++)
					{
						mapa[i][j] = Convert.ToInt32(tmp[i][j]);
					}
				}
			}
			catch(FormatException)
			{
				Console.WriteLine("Error cargando el mapa seleccionado");
			}
			
			Mapa = mapa;
		}
	}
}

