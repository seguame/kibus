using System;
using Tao.Sdl;

namespace Kibus
{
	public abstract class Nivel
	{
		protected Sprite[,] sprites;
		protected Sprite casa;
		protected Kibus kibus;
		
		protected Nivel()
		{
			sprites = new Sprite[10,10];
		}
		
		public bool EsPosibleMover(short x, short y, short xFin, short yFin)
		{
			if(xFin < SDL.ancho + 64 - 201 && yFin < SDL.alto + 64 && xFin > 0 && yFin > 0)
			{
				foreach(Sprite sprite in sprites)
				{
					if(sprite != null &&  sprite.ColisionCon(x, y, xFin, yFin))
					{
						return false;
					}
				}
				
				return true;
			}
			
			return false;
		}
		
		protected void DibujarTodo()
		{
			SDL.DibujarFondo();
			
			DibujarObstaculos();
			kibus.Dibujar();
			casa.Dibujar();
			
			SDL.RefrescarPantalla();
			
		}
		
		protected void DibujarObstaculos()
		{
			foreach(Sprite sprite in sprites)
			{
				if(sprite != null)
				{
					sprite.Dibujar();
				}
			}
		}
		
		public Sprite GetCasa()
		{
			return casa;
		}
		
		protected void MoverElementos()
		{
			kibus.Dibujar();
		}
		
		public abstract void Iniciar();
	}
}

