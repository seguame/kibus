//  
//  Nivel.cs
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
using Tao.Sdl;

using Graficos;
using Esedelish;

namespace Niveles
{
	internal abstract class Nivel
	{
		protected Sprite[,] sprites;
		protected Sprite casa;
		protected Personaje kibus;
		
		protected Nivel()
		{
			sprites = new Sprite[20,20];
		}
		
		public bool EsPosibleMover(short x, short y, short xFin, short yFin, bool usandoBandera)
		{
			if(xFin < Hardware.Ancho + 31 && yFin < Hardware.Alto + 32 && xFin > 0 && yFin > 0)
			{
				foreach(Sprite sprite in sprites)
				{
					if(sprite != null &&  sprite != casa && sprite.ColisionCon(x, y, xFin, yFin))
					{
						if(usandoBandera)
						{
							if(sprite.Bandera)
							{
								return true;
							}
						}
						
						return false;
					}
				}
				
				return true;
			}
			
			return false;
		}
		
		protected void DibujarTodo()
		{
			Hardware.DibujarFondo();
			
			DibujarObstaculos();
			kibus.Dibujar();
			casa.Dibujar();
			
			Hardware.RefrescarPantalla();
			
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
		
		protected void PosicionarKibus()
		{
			Sdl.SDL_Event evento;
			Sdl.SDL_Rect rectangulo;
			kibus  = new Personaje();
			bool puesto;
			
			if(this.casa == null)
			{
				Sprite casa = new Sprite("Assets/GFX/casini.png");
				puesto = false;
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
									
									try
									{
										if(sprites[rectangulo.x/32,rectangulo.y/32] == null)
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
									if(sprites[rectangulo.x/32,rectangulo.y/32] == null)
									{
										sprites[rectangulo.x/32,rectangulo.y/32] = casa;
										this.casa = casa;
										puesto = true;
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
					
				}while(!puesto);
			}
			
			
			puesto = false;
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
								
								try
								{
									if(sprites[rectangulo.x/32,rectangulo.y/32] == null)
									{
										kibus.Mover(rectangulo);
									}
								}
								catch (IndexOutOfRangeException) 
								{}
								
				            }
							break;
						
						case Sdl.SDL_MOUSEBUTTONDOWN:
							try
							{
								if(sprites[rectangulo.x/32,rectangulo.y/32] == null)
								{
									puesto = true;
								}
							}
							catch (IndexOutOfRangeException)
							{}

							break;
					}
				}
				
				Hardware.DibujarFondo();
				DibujarObstaculos();
				kibus.Dibujar();
				Hardware.RefrescarPantalla();
				
			}while(!puesto);
		}
		
		public abstract void Iniciar();
	}
}

