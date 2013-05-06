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
				
				if(IntentarMover(kibus, direccion, false))
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
							tmp = BrincaBandera(random);
							break;
						}
						
					}while(tmp == direccion || !IntentarMover(kibus, tmp, false));
					
					kibus.Mover(tmp);
					
					
					cola = Algoritmo.CalculaLineaBresenham(kibus.OnToyX, kibus.OnToyY, casa.OnToyX, casa.OnToyY);
					
					Console.WriteLine("Elegido nuevo camino");
				}
				
				Console.WriteLine("ONTABAN!  {0} {1}", kibus.OnTabaX, kibus.OnTabaY);
				
				try
				{
					if(++mapa[kibus.OnTabaX, kibus.OnTabaY] == 5)
					{
						if(sprites[kibus.OnTabaX, kibus.OnTabaY] == null)
						{
							sprites[kibus.OnTabaX, kibus.OnTabaY] = new Sprite("Assets/GFX/12.png");
							sprites[kibus.OnTabaX, kibus.OnTabaY].Visible = false;
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
				
				Hardware.Pausar(50);
			}
			
			DibujarTodo();
			Hardware.EscribirTexto("KIBUS LLEGO! \\O/",(short)(Hardware.Alto/2), (short)(Hardware.Ancho/2));
			Hardware.RefrescarPantalla();
			while(!Hardware.TeclaPulsada(Sdl.SDLK_RETURN))
			{
				Hardware.Pausar(20);
			}
		}

		private Direccion BrincaBandera(Random random)
		{
			Direccion direccion = Direccion.MISINGNO;

			do
			{
				direccion = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
			}while(!IntentarMover(kibus, direccion, true));

			return direccion;
		}
		
		private bool IntentarMover(Personaje personaje, Direccion direccion, bool usandoBandera)
		{
			
			switch(direccion)
			{
				case Direccion.ARRIBA:
					return EsPosibleMover(
										personaje.X, 
										(short)(personaje.Y - personaje.GetVelocidadY()),
	                    				personaje.GetXFinal(), 
										(short)(personaje.GetYFinal() - personaje.GetVelocidadY()), 
										usandoBandera);
				
				case Direccion.ABAJO:
					return EsPosibleMover(
										personaje.X, 
										(short)(personaje.Y + personaje.GetVelocidadY()),
	                    				personaje.GetXFinal(), 
										(short)(personaje.GetYFinal() + personaje.GetVelocidadY()), 
										usandoBandera);
				
				case Direccion.ABAJO_DER:
					return EsPosibleMover(
										(short)(personaje.X + personaje.GetVelocidadX()), 
										(short)(personaje.Y + personaje.GetVelocidadY()),
	                   					(short)(personaje.GetXFinal() + personaje.GetVelocidadX()), 
										(short)(personaje.GetYFinal() + personaje.GetVelocidadY()), 
										usandoBandera);
				
				case Direccion.ABAJO_IZQ:
					return EsPosibleMover(
										(short)(personaje.X - personaje.GetVelocidadX()), 
										(short)(personaje.Y + personaje.GetVelocidadY()),
	                   					(short)(personaje.GetXFinal() - personaje.GetVelocidadX()), 
										(short)(personaje.GetYFinal() + personaje.GetVelocidadY()), 
										usandoBandera);
				
				case Direccion.ARRIBA_DER:
					return EsPosibleMover(
										(short)(personaje.X + personaje.GetVelocidadX()), 
										(short)(personaje.Y - personaje.GetVelocidadY()),
	                   					(short)(personaje.GetXFinal() + personaje.GetVelocidadX()), 
										(short)(personaje.GetYFinal() - personaje.GetVelocidadY()), 
										usandoBandera);
				
				case Direccion.ARRIBA_IZQ:
					return EsPosibleMover(
										(short)(personaje.X - personaje.GetVelocidadX()), 
										(short)(personaje.Y - personaje.GetVelocidadY()),
	                   					(short)(personaje.GetXFinal() - personaje.GetVelocidadX()), 
										(short)(personaje.GetYFinal() - personaje.GetVelocidadY()), 
										usandoBandera);
				
				case Direccion.DERECHA:
					return EsPosibleMover(
										(short)(personaje.X + personaje.GetVelocidadX()), 
										personaje.Y,
	                   					(short)(personaje.GetXFinal() + personaje.GetVelocidadX()), 
										personaje.GetYFinal(), 
										usandoBandera);
				
				case Direccion.IZQUIERDA:
					return EsPosibleMover(
										(short)(personaje.X - personaje.GetVelocidadX()), 
										personaje.Y,
	                   					(short)(personaje.GetXFinal() - personaje.GetVelocidadX()), 
										personaje.GetYFinal(),
										usandoBandera);
				
				default:
					//Console.WriteLine("Movimiento no definido");
					return false;
			}
		}
		
	}
}

