//  
//  EditorNivel.cs
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
using System.Collections.Generic;
using System.IO;

using Tao.Sdl;
using System.Text;

using Graficos;
using Esedelish;

namespace Niveles
{
	internal class EditorMapas : Nivel
	{
		private int[,] matriz = new int[20,20];
		public EditorMapas() : base(){}
		
		public static int CASA
		{
			get
			{
				return 99;
			}
		}
		
		public override void Iniciar() 
		{
			Sdl.SDL_Event evento;
			Sdl.SDL_Rect rectangulo;
			bool terminado = false;
			bool casaTaim = false;
			
			Random random = new Random(System.DateTime.Now.Millisecond);
			int elemento;
			elemento = random.Next(36,42);
			Sprite obstaculo = new Sprite("Assets/GFX/"+ elemento+".png");;
			
			do
			{
				
				                       
				while(Sdl.SDL_PollEvent(out evento) > 0)
				{
					
					switch(evento.type)
					{
						case Sdl.SDL_MOUSEMOTION:
							if (evento.motion.x > 0 && evento.motion.y > 0 
						    	&& evento.motion.x < Hardware.Ancho && evento.motion.y < Hardware.Alto)
				            {
								rectangulo.x = /*(short)(evento.motion.x - 16);*/(short)(((int)(20 - (((Hardware.Ancho - evento.motion.x) / (float)Hardware.Ancho)) * 20)) * 32);
				                rectangulo.y = /*(short)(evento.motion.y - 16);*/(short)(((int)(20 - (((Hardware.Alto- evento.motion.y) / (float)Hardware.Alto)) * 20)) * 32);
								
								//try
								//{
									if(sprites[rectangulo.x/32,rectangulo.y/32] == null)
									{
										obstaculo.Mover(rectangulo);
									}
								/*}
								catch (IndexOutOfRangeException) 
								{}*/
								
				            }
							break;
						
						case Sdl.SDL_MOUSEBUTTONDOWN:
							try
							{
								if(sprites[rectangulo.x/32,rectangulo.y/32] == null)
								{
								
									sprites[rectangulo.x/32,rectangulo.y/32] = obstaculo;
								
									if(casaTaim)
									{
										matriz[rectangulo.x/32,rectangulo.y/32] = EditorMapas.CASA;
										terminado = true;
									}
									else
									{
										matriz[rectangulo.x/32,rectangulo.y/32] = elemento;
										elemento = random.Next(36,42);
										obstaculo = new Sprite("Assets/GFX/"+ elemento+".png");
									}
								}
							}
							catch (IndexOutOfRangeException)
							{}

							break;
						
						case Sdl.SDL_KEYDOWN:
							if(evento.key.keysym.sym == Sdl.SDLK_SPACE)
							{
								obstaculo = new Sprite("Assets/GFX/casini.png");
								casaTaim = true;
							}
							break;
					}
				}
				
				Hardware.DibujarFondo();
				DibujarObstaculos();
				obstaculo.Dibujar();
				Hardware.RefrescarPantalla();
				
			}while(!terminado);
			
			string nombre  = "";
			
			terminado = false;
			
			do
			{
				Hardware.DibujarFondo();
				DibujarObstaculos();
				Hardware.EscribirTexto("Nombre del mapa: ", 200, 300);
				Hardware.EscribirTexto(nombre, 200, 340);
				Hardware.RefrescarPantalla();
				
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
			

            List<string> lineasArreglo = new List<string>();
            for(int i = 0; i < 20; i++)
            {
                StringBuilder linea = new StringBuilder();
                for(int j = 0; j < 20; j++)
				{
                    linea.Append(matriz[i, j]);
					
					if(j != 19)
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

