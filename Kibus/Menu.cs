//  
//  Menu.cs
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
using System.Threading;
using Tao.Sdl;

namespace Kibus
{
	public class Menu
	{
		public enum Opcion
		{
			PRACTICA_1,
			PRACTICA_2,
			CREAR_NIVEL,
			SALIR
		};
		
		private Imagen imagenDeFondo;
		private Opcion opcionEscogida;
		
		
		public Menu ()
		{
			imagenDeFondo = new Imagen("GFX/presentacion.jpg");
		}
		
		public void Mostrar()
		{
			do
	        {
	            imagenDeFondo.Dibujar(0,0);
	
	            SDL.EscribirTexto("Ve a casa Kibus!!:",
	                     310, 300);
	
	            SDL.EscribirTexto("1.- Practica 1",
	                     250, 390);
				
				SDL.EscribirTexto("2.- Practica 2",
	                     250, 410);
				
				SDL.EscribirTexto("C.- Crear Nivel",
	                     250, 430);
	
	            SDL.EscribirTexto("S.- Salir",
	                     250, 450);
	
	            SDL.RefrescarPantalla();
				
				SDL.Pausar(20);
	
	        } while (!SDL.TeclaPulsada(Sdl.SDLK_1)
			         && !SDL.TeclaPulsada(Sdl.SDLK_2)
			         && !SDL.TeclaPulsada(Sdl.SDLK_c)
	                 && !SDL.TeclaPulsada(Sdl.SDLK_s));
			
			
			if(SDL.TeclaPulsada(Sdl.SDLK_1))opcionEscogida = Opcion.PRACTICA_1;
			else if(SDL.TeclaPulsada(Sdl.SDLK_2)) opcionEscogida = Opcion.PRACTICA_2;
			else if(SDL.TeclaPulsada(Sdl.SDLK_c)) opcionEscogida = Opcion.CREAR_NIVEL;
	        else if (SDL.TeclaPulsada(Sdl.SDLK_s)) opcionEscogida = Opcion.SALIR;
		}
		
		public Opcion OpcionElegida
		{
			get
			{
				return opcionEscogida;
			}
		}
	}
}

