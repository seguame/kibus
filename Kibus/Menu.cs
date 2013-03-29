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

