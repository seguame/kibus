using System;
using Tao.Sdl;

namespace Kibus
{
	public class Nivel
	{
		private Sprite[,] sprites;
		private Sprite casa;
		
		public Nivel ()
		{
			Random random = new Random(System.DateTime.Now.Millisecond);
			int cantidad = random.Next(20, 80);
			
			Console.WriteLine(cantidad + "%");
			sprites = new Sprite[10,10];
			int x;
			int y;
			int elemento;
			
			while(cantidad > 0)
			{
				x = random.Next(10);
				y = random.Next(10);
				elemento = random.Next(36,42);
				if(sprites[x,y] == null)
				{
					sprites[x,y] = new Sprite("GFX/"+ elemento+".png");
					sprites[x,y].Mover((short)(x * sprites[x,y].GetAncho()),(short) (y * sprites[x,y].GetAlto()));
					cantidad--;
				}
				           
			}
		}
		
		public bool EsPosibleMover(short x, short y, short xFin, short yFin)
		{
			if(xFin < SDL.ancho + 64 - 201 && yFin < SDL.alto + 64 && xFin > 0 && yFin > 0)
			{
				foreach(Sprite sprite in sprites)
				{
					if(sprite != null && /*sprite != casa &&*/ sprite.ColisionCon(x, y, xFin, yFin))
					{
						return false;
					}
				}
				
				return true;
			}
			
			return false;
		}
		
		public void Dibujar()
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
		
		public void PosicionarCasa()
		{
			Sdl.SDL_Event evento;
			Sdl.SDL_Rect rectangulo;
			Sprite casa = new Sprite("GFX/casini.png");
			bool puesta = false;
			
			do
			{
				while(Sdl.SDL_PollEvent(out evento) > 0)
				{
					
					switch(evento.type)
					{
						case Sdl.SDL_MOUSEMOTION:
							if (evento.motion.x > 0 && evento.motion.y > 0)
				            {
				                rectangulo.x = (short)(((int)(10 - ((((SDL.ancho - 200) - evento.motion.x) / (float)(SDL.ancho - 200))) * 10)) * 64);
				                rectangulo.y = (short)(((int)(10 - (((SDL.alto- evento.motion.y) / (float)SDL.alto)) * 10)) * 64);
								
								try
								{
									if(sprites[rectangulo.x/64,rectangulo.y/64] == null)
									{
										casa.Mover(rectangulo);
									}
								}
								catch (IndexOutOfRangeException) 
								{}
								
				            }
							break;
						
						case Sdl.SDL_MOUSEBUTTONDOWN:
							try
							{
								if(sprites[rectangulo.x/64,rectangulo.y/64] == null)
								{
									sprites[rectangulo.x/64,rectangulo.y/64] = casa;
									this.casa = casa;
									puesta = true;
								}
							}
							catch (IndexOutOfRangeException)
							{}

							break;
					}
				}
				SDL.DibujarFondo();
				this.Dibujar();
				casa.Dibujar();
				SDL.RefrescarPantalla();
				
			}while(!puesta);
		}
	}
}

