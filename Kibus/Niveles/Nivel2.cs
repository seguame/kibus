//  
//  Nivel2.cs
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
using System.Collections.Generic;
using Utileria;
using Algoritmos;

namespace Niveles
{
	internal class Nivel2 : Nivel
	{
		public Nivel2 ()
		{
			new CargadorMapas().SeleccionarArchivo();
		}
		
		public override void Iniciar()
		{
			int[][] mapa = CargadorMapas.Mapa;
			
			//TODO:
			//Quitar de todos el tamaño fijo
			for(int i = 0; i < 20; i++)
			{
				for(int j = 0; j < 20; j++)
				{
					if(mapa[i][j] == EditorMapas.CASA)
					{
						Console.WriteLine("Casa en {0},{1}", i, j);
						this.casa = sprites[i,j] = new Sprite("Assets/GFX/casini.png");
					}
					else if(mapa[i][j] != 0)
					{
						Console.WriteLine("{2} en {0},{1}", i, j, mapa[i][j]);
						sprites[i,j] = new Sprite("Assets/GFX/"+mapa[i][j]+".png");
					}
					
					if(sprites[i,j] != null)
					{
						sprites[i,j].Mover((short)(i * sprites[i,j].Ancho),(short) (j * sprites[i,j].Alto));
					}
				}
			}
			
			PosicionarKibus();

			BuscarCasa();
		}
		
		private void BuscarCasa()
		{
			List<Direccion> usadas = new List<Direccion>((int)Direccion.MISINGNO);
			int[,] mapa = new int[20,20];
			
			Random random = new Random(System.DateTime.Now.Millisecond);
			Queue<Direccion> cola = Algoritmo.CalculaLineaBresenham(kibus.OnToyX, kibus.OnToyY, casa.OnToyX, casa.OnToyY);
			Direccion direccion;
			
			while(cola.Count != 0)
			{
				DibujarTodo();
				Hardware.EscribirTexto("Buscando Casa", 641, 10); 
				Hardware.RefrescarPantalla();
				
				direccion = cola.Dequeue();
				
				if(IntentarMover(direccion))
				{
					kibus.Mover(direccion);
				}
				else
				{
					usadas.Clear();
					Console.WriteLine("Recalculando");
					Direccion tmp;
					
					do
					{
						tmp = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
						Console.WriteLine("{0} {1}", tmp, direccion);
						
						if(!usadas.Contains(tmp))
						{
							usadas.Add (tmp);
						}
						else if(usadas.Count == (int)Direccion.MISINGNO)
						{
							break;
						}
						
					}while(tmp == direccion || !IntentarMover(tmp));
					
					kibus.Mover(tmp);
					
					
					cola = Algoritmo.CalculaLineaBresenham(kibus.OnToyX, kibus.OnToyY, casa.OnToyX, casa.OnToyY);
					
					Console.WriteLine("Elegido nuevo camino");
				}
				
				Console.WriteLine("ONTABAN!  {0} {1}", kibus.OnTabaX, kibus.OnTabaY);
				
				try
				{
					if(++mapa[kibus.OnTabaX, kibus.OnTabaY] == 10)
					{
						if(sprites[kibus.OnTabaX, kibus.OnTabaY] == null)
						{
							sprites[kibus.OnTabaX, kibus.OnTabaY] = new Sprite("Assets/GFX/12.png");
							sprites[kibus.OnTabaX, kibus.OnTabaY].Visible = true;
							sprites[kibus.OnTabaX, kibus.OnTabaY].Bandera = true;
							sprites[kibus.OnTabaX, kibus.OnTabaY].Mover((short)(kibus.OnTabaX * sprites[kibus.OnTabaX, kibus.OnTabaY].Ancho),(short) (kibus.OnTabaY * sprites[kibus.OnTabaX, kibus.OnTabaY].Alto));
						}
					}
					else
					{
						Console.WriteLine("{0}", mapa[kibus.OnTabaX, kibus.OnTabaY]);
					}
				}
				catch{}
				
				Hardware.Pausar(10);
			}
			
			DibujarTodo();
			Hardware.EscribirTexto("KIBUS LLEGO! \\O/",(short)(Hardware.Alto/2), (short)(Hardware.Ancho/2));
			Hardware.RefrescarPantalla();
			while(!Hardware.TeclaPulsada(Sdl.SDLK_RETURN))
			{
				Hardware.Pausar(20);
			}
		}
		
		private bool IntentarMover(Direccion direccion)
		{
			
			switch(direccion)
			{
				case Direccion.ARRIBA:
					return EsPosibleMover(
										kibus.X, 
										(short)(kibus.Y - kibus.GetVelocidadY()),
	                    				kibus.GetXFinal(), 
										(short)(kibus.GetYFinal() - kibus.GetVelocidadY()));
				
				case Direccion.ABAJO:
					return EsPosibleMover(
										kibus.X, 
										(short)(kibus.Y + kibus.GetVelocidadY()),
	                    				kibus.GetXFinal(), 
										(short)(kibus.GetYFinal() + kibus.GetVelocidadY()));
				
				case Direccion.ABAJO_DER:
					return EsPosibleMover(
										(short)(kibus.X + kibus.GetVelocidadX()), 
										(short)(kibus.Y + kibus.GetVelocidadY()),
	                   					(short)(kibus.GetXFinal() + kibus.GetVelocidadX()), 
										(short)(kibus.GetYFinal() + kibus.GetVelocidadY()));
				
				case Direccion.ABAJO_IZQ:
					return EsPosibleMover(
										(short)(kibus.X - kibus.GetVelocidadX()), 
										(short)(kibus.Y + kibus.GetVelocidadY()),
	                   					(short)(kibus.GetXFinal() - kibus.GetVelocidadX()), 
										(short)(kibus.GetYFinal() + kibus.GetVelocidadY()));
				
				case Direccion.ARRIBA_DER:
					return EsPosibleMover(
										(short)(kibus.X + kibus.GetVelocidadX()), 
										(short)(kibus.Y - kibus.GetVelocidadY()),
	                   					(short)(kibus.GetXFinal() + kibus.GetVelocidadX()), 
										(short)(kibus.GetYFinal() - kibus.GetVelocidadY()));
				
				case Direccion.ARRIBA_IZQ:
					return EsPosibleMover(
										(short)(kibus.X - kibus.GetVelocidadX()), 
										(short)(kibus.Y - kibus.GetVelocidadY()),
	                   					(short)(kibus.GetXFinal() - kibus.GetVelocidadX()), 
										(short)(kibus.GetYFinal() - kibus.GetVelocidadY()));
				
				case Direccion.DERECHA:
					return EsPosibleMover(
										(short)(kibus.X + kibus.GetVelocidadX()), 
										kibus.Y,
	                   					(short)(kibus.GetXFinal() + kibus.GetVelocidadX()), 
										kibus.GetYFinal());
				
				case Direccion.IZQUIERDA:
					return EsPosibleMover(
										(short)(kibus.X - kibus.GetVelocidadX()), 
										kibus.Y,
	                   					(short)(kibus.GetXFinal() - kibus.GetVelocidadX()), 
										kibus.GetYFinal());
				
				default:
					//Console.WriteLine("Movimiento no definido");
					return false;
			}
		}
		
		private void PosicionarKibus()
		{
			Sdl.SDL_Event evento;
			Sdl.SDL_Rect rectangulo;
			kibus  = new Kibus();
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
	}
}

