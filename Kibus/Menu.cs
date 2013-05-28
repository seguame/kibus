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

using Graficos;
using Esedelish;

public class Menu
{
	public enum Opcion
	{
		PRACTICA_1,
		PRACTICA_2,
		PRACTICA_3,
		PRACTICA_4,
		CREAR_NIVEL,
		SALIR
	};
		
	private Imagen imagenDeFondo;
		
		
	public Menu ()
	{
		imagenDeFondo = new Imagen("Assets/GFX/presentacion.jpg");
	}
		
	public void Mostrar()
	{
		do
	    {
		    imagenDeFondo.Dibujar(0,0);
	
	        Hardware.EscribirTexto("Ve a casa Kibus!!:",
	                     310, 300);
	
	        Hardware.EscribirTexto("1.- Practica 1",
	                     250, 390);
				
			Hardware.EscribirTexto("2.- Practica 2",
	                     250, 410);
			
			Hardware.EscribirTexto("3.- Practica 3",
	                     250, 430);
			
			Hardware.EscribirTexto("4.- Practica 4",
	                     250, 450);
				
			Hardware.EscribirTexto("C.- Crear Nivel",
	                     250, 470);
	
	        Hardware.EscribirTexto("S.- Salir",
	                     250, 490);
	
	        Hardware.RefrescarPantalla();
				
			Hardware.Pausar(20);
	
	    } while (!Hardware.TeclaPulsada(Sdl.SDLK_1)
			     && !Hardware.TeclaPulsada(Sdl.SDLK_2)
		         && !Hardware.TeclaPulsada(Sdl.SDLK_3)
		         && !Hardware.TeclaPulsada(Sdl.SDLK_4)
			     && !Hardware.TeclaPulsada(Sdl.SDLK_c)
	             && !Hardware.TeclaPulsada(Sdl.SDLK_s));
			
			
		if(Hardware.TeclaPulsada(Sdl.SDLK_1)) OpcionElegida = Opcion.PRACTICA_1;
		else if(Hardware.TeclaPulsada(Sdl.SDLK_2)) OpcionElegida = Opcion.PRACTICA_2;
		else if(Hardware.TeclaPulsada(Sdl.SDLK_3)) OpcionElegida = Opcion.PRACTICA_3;
		else if(Hardware.TeclaPulsada(Sdl.SDLK_4)) OpcionElegida = Opcion.PRACTICA_4;
		else if(Hardware.TeclaPulsada(Sdl.SDLK_c)) OpcionElegida = Opcion.CREAR_NIVEL;
	    else if (Hardware.TeclaPulsada(Sdl.SDLK_s)) OpcionElegida = Opcion.SALIR;
	}
		
	public Opcion OpcionElegida
	{
		private set;
		get;
	}
	
}

