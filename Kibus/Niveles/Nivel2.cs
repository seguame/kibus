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
			for(int i = 0; i < 10; i++)
			{
				for(int j = 0; j < 10; j++)
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
						sprites[i,j].Mover((short)(i * sprites[i,j].GetAncho()),(short) (j * sprites[i,j].GetAlto()));
					}
				}
			}
			
			PosicionarKibus();

			BuscarCasa();
		}

		private void BuscarCasa()
		{
			int[,] mapa = new int[10,10];
			
			Random random = new Random(System.DateTime.Now.Millisecond);
			Queue<Direccion> cola = Algoritmo.CalculaLineaBresenham(kibus.GetX()/64, kibus.GetY ()/64, casa.GetX ()/64, casa.GetY()/64);
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
					Console.WriteLine("Recalculando");
					Direccion tmp = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
					
					while(tmp == direccion || !IntentarMover(tmp))
					{
						Console.WriteLine("{0} {1}", tmp, direccion);
						tmp = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
					}
					
					kibus.Mover(tmp);
					
					
					cola = Algoritmo.CalculaLineaBresenham(kibus.GetX()/64, kibus.GetY ()/64, casa.GetX ()/64, casa.GetY()/64);
					
					Console.WriteLine("Elegido nuevo camino");
				}
				
				if(++mapa[kibus.OnTabaX, kibus.OnTabaY] == 5)
				{
					sprites[kibus.OnTabaX, kibus.OnTabaY] = new Sprite("Assets/GFX/30.png");
					sprites[kibus.OnTabaX, kibus.OnTabaY].Visible = false;
				}
				
				Hardware.Pausar(200);
			}
		}
		
		private bool IntentarMover(Direccion direccion)
		{
			switch(direccion)
			{
				case Direccion.ARRIBA:
					return EsPosibleMover(
										kibus.GetX(), 
										(short)(kibus.GetY() - kibus.GetVelocidadY()),
	                    				kibus.GetXFinal(), 
										(short)(kibus.GetYFinal() - kibus.GetVelocidadY()));
				
				case Direccion.ABAJO:
					return EsPosibleMover(
										kibus.GetX(), 
										(short)(kibus.GetY() + kibus.GetVelocidadY()),
	                    				kibus.GetXFinal(), 
										(short)(kibus.GetYFinal() + kibus.GetVelocidadY()));
				
				case Direccion.ABAJO_DER:
					return EsPosibleMover(
										(short)(kibus.GetX() + kibus.GetVelocidadX()), 
										(short)(kibus.GetY() + kibus.GetVelocidadY()),
	                   					(short)(kibus.GetXFinal() + kibus.GetVelocidadX()), 
										(short)(kibus.GetYFinal() + kibus.GetVelocidadY()));
				
				case Direccion.ABAJO_IZQ:
					return EsPosibleMover(
										(short)(kibus.GetX() + kibus.GetVelocidadX()), 
										(short)(kibus.GetY() - kibus.GetVelocidadY()),
	                   					(short)(kibus.GetXFinal() + kibus.GetVelocidadX()), 
										(short)(kibus.GetYFinal() - kibus.GetVelocidadY()));
				
				case Direccion.ARRIBA_DER:
					return EsPosibleMover(
										(short)(kibus.GetX() + kibus.GetVelocidadX()), 
										(short)(kibus.GetY() - kibus.GetVelocidadY()),
	                   					(short)(kibus.GetXFinal() + kibus.GetVelocidadX()), 
										(short)(kibus.GetYFinal() - kibus.GetVelocidadY()));
				
				case Direccion.ARRIBA_IZQ:
					return EsPosibleMover(
										(short)(kibus.GetX() - kibus.GetVelocidadX()), 
										(short)(kibus.GetY() - kibus.GetVelocidadY()),
	                   					(short)(kibus.GetXFinal() - kibus.GetVelocidadX()), 
										(short)(kibus.GetYFinal() - kibus.GetVelocidadY()));
				
				case Direccion.DERECHA:
					return EsPosibleMover(
										(short)(kibus.GetX() + kibus.GetVelocidadX()), 
										kibus.GetY(),
	                   					(short)(kibus.GetXFinal() + kibus.GetVelocidadX()), 
										kibus.GetYFinal());
				
				case Direccion.IZQUIERDA:
					return EsPosibleMover(
										(short)(kibus.GetX() - kibus.GetVelocidadX()), 
										kibus.GetY(),
	                   					(short)(kibus.GetXFinal() - kibus.GetVelocidadX()), 
										kibus.GetYFinal());
				
				default:
					Console.WriteLine("Movimiento no definido");
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
							if (evento.motion.x > 0 && evento.motion.y > 0)
				            {
				                rectangulo.x = (short)(((int)(10 - ((((Hardware.Ancho) - evento.motion.x) / (float)(Hardware.Ancho))) * 10)) * 64);
				                rectangulo.y = (short)(((int)(10 - (((Hardware.Alto- evento.motion.y) / (float)Hardware.Alto)) * 10)) * 64);
								
								try
								{
									if(sprites[rectangulo.x/64,rectangulo.y/64] == null)
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
								if(sprites[rectangulo.x/64,rectangulo.y/64] == null)
								{
									//sprites[rectangulo.x/64,rectangulo.y/64] = casa;
									//this.casa = casa;
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

