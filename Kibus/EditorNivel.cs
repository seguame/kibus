using System;
using System.Collections.Generic;
using System.IO;

using Tao.Sdl;
using System.Text;

namespace Kibus
{
	public class EditorNivel : Nivel
	{
		private int[,] matriz = new int[10,10];
		public EditorNivel() : base(){}
		
		public override void Iniciar() 
		{
			Sdl.SDL_Event evento;
			Sdl.SDL_Rect rectangulo;
			bool terminado = false;
			
			Random random = new Random(System.DateTime.Now.Millisecond);
			int elemento;
			elemento = random.Next(36,42);
			Sprite obstaculo = new Sprite("GFX/"+ elemento+".png");;
			
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
										obstaculo.Mover(rectangulo);
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
									sprites[rectangulo.x/64,rectangulo.y/64] = obstaculo;
									matriz[rectangulo.x/64,rectangulo.y/64] = elemento;
									elemento = random.Next(36,42);
									obstaculo = new Sprite("GFX/"+ elemento+".png");;
								}
							}
							catch (IndexOutOfRangeException)
							{}

							break;
						
						case Sdl.SDL_KEYDOWN:
							if(evento.key.keysym.sym == Sdl.SDLK_SPACE)
							{
								terminado = true;
							}
							break;
					}
				}
				
				/*for(int i = 0;i < 10; i++)
				{
					for(int j = 0; j < 10; j++)
					{
						Console.Write(matriz[i,j]);
					}
					Console.WriteLine();
				}
				Console.WriteLine("----------------------------------------------------");*/
				
				SDL.DibujarFondo();
				DibujarObstaculos();
				obstaculo.Dibujar();
				SDL.RefrescarPantalla();
				
			}while(!terminado);
			
			string nombre  = "";
			
			terminado = false;
			
			do
			{
				SDL.DibujarFondo();
				DibujarObstaculos();
				SDL.EscribirTexto("Nombre del mapa: ", 200, 300);
				SDL.EscribirTexto(nombre, 200, 340);
				SDL.RefrescarPantalla();
				
				while(Sdl.SDL_PollEvent(out evento) > 0)
				{
					
					switch(evento.type)
					{
						case Sdl.SDL_KEYDOWN:
							if(evento.key.keysym.sym == Sdl.SDLK_RETURN)
							{
								terminado = true;
							}
							else
							{
								nombre += Char.ConvertFromUtf32(evento.key.keysym.sym);
							}
							break;
					}
				}
				
			}while(!terminado);
			
            ///Fill the table

            List<string> lineasArreglo = new List<string>();
            for(int i = 0; i < 10; i++)
            {
                StringBuilder linea = new StringBuilder();
                for(int j = 0; j < 10; j++)
				{
                    linea.Append(matriz[i, j]);
					
					if(j != 9)
					{
						linea.Append(",");
					}
				}
                lineasArreglo.Add(linea.ToString());
            }
			
            File.WriteAllLines("Mapas/"+nombre+".txt", lineasArreglo.ToArray());
		}
	}
}

