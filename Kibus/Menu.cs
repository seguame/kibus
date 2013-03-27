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
	
	            SDL.EscribirTexto("S.- Salir",
	                     250, 410);
	
	            SDL.RefrescarPantalla();
				
				SDL.Pausar(20);
	
	        } while ((!SDL.TeclaPulsada(Sdl.SDLK_1))
	                 && !SDL.TeclaPulsada(Sdl.SDLK_s));
			
			opcionEscogida = Opcion.PRACTICA_1;
	        if (SDL.TeclaPulsada(Sdl.SDLK_s))
	            opcionEscogida = Opcion.SALIR;
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

