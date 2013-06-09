//  
//  Nivel4.cs
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
using Graficos;
using Utileria;
using Esedelish;
using Tao.Sdl;
using Algoritmos.Estructuras;
using System.Collections.Generic;
using Algoritmos;

namespace Niveles
{
	internal class Nivel4 : Nivel
	{
		private int minimo = Int32.MaxValue;
		private int maximo = Int32.MinValue;
		private int mediaHistorica;
		private Nodo[] arregloNodosTotal;
		
		private Nodo[] nodosVisitados;
		
		private Sdl.SDL_Rect rectanguloOrigen;
		
		private int[][] mapa;
		bool modoGrafico;
		
		ComparadorNodos comparadorDeNodos;
		
		public Nivel4 ()
		{
			new CargadorMapas().SeleccionarArchivo();
			
			arregloNodosTotal = new Nodo[20*20];
			
			comparadorDeNodos = new ComparadorNodos();
		}
		
		public override void Iniciar()
		{
			mapa = CargadorMapas.Mapa;
			
			//TODO:
			//Quitar de todos el tamaño fijo
			for(int i = 0; i < 20; i++)
			{
				for(int j = 0; j < 20; j++)
				{
					if(mapa[i][j] == EditorMapas.CASA)
					{
						requierePosicionarCasa = true;
						//Console.WriteLine("Casa en {0},{1}", i, j);
						//this.casa = sprites[i,j] = new Sprite("Assets/GFX/casini.png");
					}
					else if(mapa[i][j] != 0)
					{
						//Console.WriteLine("{2} en {0},{1}", i, j, mapa[i][j]);
						sprites[i,j] = new Sprite("Assets/GFX/"+mapa[i][j]+".png");
						mapa[i][j] = -1;
					}
					
					if(sprites[i,j] != null)
					{
						sprites[i,j].Mover((short)(i * sprites[i,j].Ancho),(short) (j * sprites[i,j].Alto));
					}
				}
			}
			
			PosicionarKibus();
			//El entrenamiento puesun
			ConfeccionarCamino();
			
			
			
			kibus.Mover(rectanguloOrigen);
			
			List<Nodo> configuracionDeMapa = new List<Nodo>(arregloNodosTotal);
			
			
			#region activar para ver ordenada la configuracion
			for(int i = 0; i < configuracionDeMapa.Count;i++) 
			{
				if(configuracionDeMapa[i] == null)
				{
					configuracionDeMapa[i] = new Nodo();
					configuracionDeMapa[i].numeroDeNodo = Int32.MaxValue;
				}
			}
			configuracionDeMapa.Sort(comparadorDeNodos);
			#endregion
			
			Console.WriteLine("Inicia el Confeccionado con \"Primero el Mejor\"");
			
			Queue<Direccion> movimientosASeguir = Algoritmo.PrimeroElMejor(configuracionDeMapa[1], configuracionDeMapa[0]);
			
			Console.WriteLine("Termina el Confeccionado con \"Primero el Mejor\"");
			
			while(movimientosASeguir.Count != 0)
			{
				kibus.Mover(movimientosASeguir.Dequeue());
				DibujarTodo();
				Hardware.EscribirTexto("Llendo a Casa", 641, 10); 
				Hardware.RefrescarPantalla();
				Hardware.Pausar(100);
			}
			
			Hardware.EscribirTexto("KIBUS LLEGO! \\O/",(short)(Hardware.Alto/2), (short)(Hardware.Ancho/2));
			Hardware.RefrescarPantalla();
			while(!Hardware.TeclaPulsada(Sdl.SDLK_RETURN))
			{
				Hardware.Pausar(20);
			}
			
			foreach(Nodo n in configuracionDeMapa)
			{
				if(n.numeroDeNodo != Int32.MaxValue)
					Console.WriteLine(n.ToString());
			}
			
		}
		
		private void ConfeccionarCamino()
		{
			int intentos;
			int repeticiones = 2000;
			int castigo;
			
			Direccion direccion;
			Random random = new Random(System.DateTime.Now.Millisecond);
			
			int origen = kibus.OnToyY + kibus.OnToyX * 20;
			int destino = casa.OnToyY + casa.OnToyX * 20;
			//Setear los 2 nodos iniciales, origen y destino
			
			rectanguloOrigen = kibus.GetRectangulo();
			
			while(repeticiones-->0)
			{
				Console.WriteLine("Iteracion #" + (1999 - repeticiones));
				//Volver a iniciar los valores;
				nodosVisitados = new Nodo[20*20];
				Nodo.CantidadNodosVisitados = 0;
				Conexion.conexionesUsadas = 0;
				
				nodosVisitados[destino] = new Nodo();
				nodosVisitados[origen] = new Nodo();
				
				kibus.Mover(rectanguloOrigen);
				
				while(kibus.OnToyX != casa.OnToyX || kibus.OnToyY != casa.OnToyY)
				{
					modoGrafico = Hardware.TeclaPulsada(Sdl.SDLK_g);
					
					if(modoGrafico)
					{
						DibujarTodo();
						Hardware.EscribirTexto("Entrenando", 641, 10); 
						Hardware.RefrescarPantalla();
					}
					
					int anterior;
					int actual;
					intentos = 0;
					Sdl.SDL_Rect rectTemp = kibus.GetRectangulo();
					do
					{
						kibus.Mover(rectTemp);
						do
						{
							
							direccion = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
							intentos++;
							
						}while(!IntentarMover(kibus, direccion, false));
						
						anterior = kibus.OnToyY + kibus.OnToyX * 20;
						
						kibus.Mover(direccion);
						
						actual = kibus.OnToyY + kibus.OnToyX * 20;
						
						if(nodosVisitados[actual] == null)
						{
							nodosVisitados[actual] = new Nodo();
							break;
						}
						
					}while(intentos < 8);
					
					Conexion cnxDestino;
					
					if(nodosVisitados[anterior].conexiones[(int)direccion] == null)
					{
						cnxDestino = new Conexion();
						cnxDestino.direccionUsada = direccion;
					}
					else
					{
						cnxDestino = nodosVisitados[anterior].conexiones[(int)direccion];
					}
					                                     
					cnxDestino.NodoConexion = nodosVisitados[actual];
					
					nodosVisitados[anterior].conexiones[(int)direccion] = cnxDestino;
					
					if(modoGrafico)
					{
						Hardware.Pausar(10);
					}
				}
				
				//Console.WriteLine("CASA: {0},{1}", casa.OnToyX,casa.OnToyY);
				//Console.WriteLine("KIBU: {0},{1}", kibus.OnToyX,kibus.OnToyY);
				
				
				
				//Seteo de los valores minimos y máximos
				if(Conexion.conexionesUsadas > maximo) maximo = Conexion.conexionesUsadas;
				if(Conexion.conexionesUsadas < minimo) minimo = Conexion.conexionesUsadas;
				
				mediaHistorica = (maximo + minimo)/2;
				
				
				castigo = /*Math.Abs(*/Conexion.conexionesUsadas - mediaHistorica/*)*/;
				
				
				Console.WriteLine("Maximo :\t" + maximo);
				Console.WriteLine("Minimo :\t" + minimo);
				Console.WriteLine("Media  :\t" + mediaHistorica);
				Console.WriteLine("Conexiones:\t" + Conexion.conexionesUsadas);
				Console.WriteLine("Castigo:\t" + castigo);
				Console.WriteLine(".------------------.");
				actualizarMundoValorifico(nodosVisitados, castigo);
			}
		}
		
		private void actualizarMundoValorifico(Nodo[] nodosVisitados, int castigo)
		{
			for(int i = 0; i < nodosVisitados.Length; i++)
			{
				if(nodosVisitados[i] != null)
				{
					if(arregloNodosTotal[i] == null)
					{
						arregloNodosTotal[i] = nodosVisitados[i];
					}
					else
					{
						//Checar las 8 conexiones del nodo y ver que fueron usadas, para aplicarles el castigop puesun.
						for(int j = 0; j < nodosVisitados[i].conexiones.Length; j++)
						{
							
							if(nodosVisitados[i].conexiones[j] != null) //Si la conexion se generó se verá actualizado su valor en el mapa
							{
								if(arregloNodosTotal[i].conexiones[j] == null) //si esto ocurre, se asignal la conexion tal cual, pues es nueva
								{
									arregloNodosTotal[i].conexiones[j] = nodosVisitados[i].conexiones[j];
								}
								else //se actualizara el valor dependiendo del castigo
								{
									arregloNodosTotal[i].conexiones[j].Peso += castigo;
								}
							}
							//else, si no se utilizo la conexion, no tenemos que actualizar su situacion en el mapa
						}
					}
				}
			}
		}
	}
}

