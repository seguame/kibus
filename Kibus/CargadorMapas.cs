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

namespace Kibus
{
	public class CargadorMapas
	{
		public static string Mapa
		{
			private set;
			get;
		}
		
		private bool hayHarchivos;
		
		private string[] archivos;
		
		public CargadorMapas ()
		{
			archivos = Directory.GetFiles("Mapas/","*.txt");
			
			if(archivos.Length == 0)
			{
				hayHarchivos = false;
			}
			else
			{
				hayHarchivos = true;
			}
		}
		
		public void SeleccionarArchivo()
		{
			if(!hayHarchivos)
			{
				SDL.DibujarFondo(0,0,150);
				SDL.EscribirTexto("Crea primero algun mapa", 50, 50);
				SDL.RefrescarPantalla();
				SDL.Pausar(1000);
				return;
			}
			
			
			do
			{
				SDL.DibujarFondo(0,0,150);
				ListarArchivos();
				SDL.RefrescarPantalla();
			}while(true);
		}
		
		private void ListarArchivos()
		{
			for(int i = 0; i < archivos.Length; i++)
			{
				SDL.EscribirTexto(archivos[i], 5, (i * 30));
			}
		}
	}
}

