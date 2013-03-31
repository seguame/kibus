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

