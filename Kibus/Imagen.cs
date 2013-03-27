using System;

namespace Kibus
{
	public class Imagen
	{
		private IntPtr apuntadorImagen;
		
		public Imagen (string archivo)
		{
			apuntadorImagen = SDL.CargarImagen(archivo);
		}
		
		public IntPtr PunteroImagen()
		{
			return apuntadorImagen;
		}
		
		public void Dibujar(short x, short y)
		{
			SDL.DibujarImagen(apuntadorImagen, x ,y);
		}
	}
}

