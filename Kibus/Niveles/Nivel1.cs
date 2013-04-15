//  
//  Nivel1.cs
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
using Tao.Sdl;

using Graficos;
using Esedelish;
using Utileria;

namespace Niveles
{
	internal class Nivel1 : Nivel
	{
		
		private Stack<int> pasos;
		bool continuar;
		
		public Nivel1 (): base() 
		{
			Random random = new Random(System.DateTime.Now.Millisecond);
			int cantidad = random.Next(20, 80);
			
			Console.WriteLine(cantidad + "%");
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
					sprites[x,y] = new Sprite("Assets/GFX/"+ elemento+".png");
					sprites[x,y].Mover((short)(x * sprites[x,y].GetAncho()),(short) (y * sprites[x,y].GetAlto()));
					cantidad--;
				}
				           
			}
		}
		
		
		public override void Iniciar()
		{
			pasos = new Stack<int>();
			continuar = true;
			
			PosicionarCasa();
			
			kibus = new Kibus(GetCasa().GetX(), GetCasa().GetY());
			
			
			//usaurio parte
			while(continuar)
			{
				DibujarTodo();
				ComprobarTeclas();
				MoverElementos();
				Hardware.Pausar(30);
			}
			
			
			//IA parte
			while(pasos.Count > 0)
			{
				DibujarTodo();
				Hardware.EscribirTexto("Volviendo a Casa", 10, 10); 
				Hardware.RefrescarPantalla();
				Regresar();
				MoverElementos();
				Hardware.Pausar(300);
			}
			
			DibujarTodo();
			Hardware.EscribirTexto("KIBUS LLEGO! \\O/",(short)(Hardware.Alto/2), (short)(Hardware.Ancho/2));
			Hardware.RefrescarPantalla();
			while(!Hardware.TeclaPulsada(Sdl.SDLK_RETURN))
			{
				Hardware.Pausar(20);
			}
		}
		
		private void ComprobarTeclas()
		{
			
			if(Hardware.TeclaPulsada(Sdl.SDLK_RETURN))
			{
				continuar = false;
			}

	        if ((Hardware.TeclaPulsada(Sdl.SDLK_UP))
	            && EsPosibleMover(kibus.GetX(), (short)(kibus.GetY() - kibus.GetVelocidadY()),
	                    kibus.GetXFinal(), (short)(kibus.GetYFinal() - kibus.GetVelocidadY())))
			{
				pasos.Push(Sdl.SDLK_DOWN);
	            kibus.Mover(Direccion.ARRIBA);
			}
	
	        if ((Hardware.TeclaPulsada(Sdl.SDLK_DOWN))
	            && EsPosibleMover(kibus.GetX(),(short)(kibus.GetY() + kibus.GetVelocidadY()),
	                   kibus.GetXFinal(), (short)(kibus.GetYFinal() + kibus.GetVelocidadY())))
				
			{
				pasos.Push(Sdl.SDLK_UP);
	            kibus.Mover(Direccion.ABAJO);
			}
	
	        if ((Hardware.TeclaPulsada(Sdl.SDLK_RIGHT))
	            && EsPosibleMover((short)(kibus.GetX() + kibus.GetVelocidadX()), kibus.GetY(),
	                   (short)(kibus.GetXFinal() + kibus.GetVelocidadX()), kibus.GetYFinal()))
			{
				pasos.Push(Sdl.SDLK_LEFT);
	            kibus.Mover(Direccion.DERECHA);
			}
	
	        if ((Hardware.TeclaPulsada(Sdl.SDLK_LEFT) )
	            && EsPosibleMover((short)(kibus.GetX() - kibus.GetVelocidadX()), kibus.GetY(),
	                   (short)(kibus.GetXFinal() - kibus.GetVelocidadX()), kibus.GetYFinal()))
			{
				pasos.Push(Sdl.SDLK_RIGHT);
	           kibus.Mover(Direccion.IZQUIERDA);
			}

		}
		
		private void Regresar()
		{
			switch(pasos.Pop())
			{
				case Sdl.SDLK_UP:
					kibus.Mover(Direccion.ARRIBA);
					break;
				case Sdl.SDLK_DOWN:
					kibus.Mover(Direccion.ABAJO);
					break;
				case Sdl.SDLK_LEFT:
					kibus.Mover(Direccion.IZQUIERDA);
					break;
				case Sdl.SDLK_RIGHT:
					kibus.Mover(Direccion.DERECHA);
					break;
			}
		}
		
		private void PosicionarCasa()
		{
			Sdl.SDL_Event evento;
			Sdl.SDL_Rect rectangulo;
			Sprite casa = new Sprite("Assets/GFX/casini.png");
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
				                rectangulo.x = (short)(((int)(10 - ((((Hardware.Ancho) - evento.motion.x) / (float)(Hardware.Ancho))) * 10)) * 64);
				                rectangulo.y = (short)(((int)(10 - (((Hardware.Alto- evento.motion.y) / (float)Hardware.Alto)) * 10)) * 64);
								
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
				Hardware.DibujarFondo();
				DibujarObstaculos();
				casa.Dibujar();
				Hardware.RefrescarPantalla();
				
			}while(!puesta);
		}
	}
}

