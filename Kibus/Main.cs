using System;
using Tao.Sdl;

namespace Kibus
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			SDL.Inicializar(840, 640, 24, false);
			SDL.CambiarColorTexto(255, 255, 255);
			
			Menu.Opcion opcion;
			Menu menu = new Menu();
			
			do
			{
				menu.Mostrar();
				
				opcion = menu.OpcionElegida;
				
				if(opcion == Menu.Opcion.SALIR)
				{
					Sdl.SDL_Quit();
				}
				else
				{
					new Escenario(opcion).Iniciar();
				}
				
			}while(opcion != Menu.Opcion.SALIR);
		}
	}
}
