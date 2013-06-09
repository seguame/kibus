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

namespace Niveles
{
	internal class Nivel4 : Nivel
	{
		private int minimo = Int32.MaxValue;
		private int maximo = Int32.MinValue;
		private int mediaHistorica;
		private Nodo[] arregloNodosTotal;
		
		private Nodo[] nodosVisitados;
		
		
		private int[][] mapa;
		bool modoGrafico;
		
		public Nivel4 ()
		{
			new CargadorMapas().SeleccionarArchivo();
			
			arregloNodosTotal = new Nodo[20*20];
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
			
			foreach(Nodo n in arregloNodosTotal)
			{
				if(n != null)
					Console.WriteLine(n.ToString());
			}
		}
		
		private void ConfeccionarCamino()
		{
			int repeticiones = 1000;
			int castigo;
			
			Direccion direccion;
			Random random = new Random(System.DateTime.Now.Millisecond);
			
			Console.WriteLine("CASA: {0},{1}", casa.OnToyX,casa.OnToyY);
			Console.WriteLine("KIBU: {0},{1}", kibus.OnToyX,kibus.OnToyY);
			
			int origen = kibus.OnToyY + kibus.OnToyX * 20;
			int destino = casa.OnToyY + casa.OnToyX * 20;
			//Setear los 2 nodos iniciales, origen y destino
			
			Sdl.SDL_Rect rectanguloOrigen = kibus.GetRectangulo();
			
			while(repeticiones-->0)
			{
				Console.WriteLine(repeticiones);
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
					
					do
					{
						
						direccion = (Direccion)random.Next(0, (int)Direccion.MISINGNO);
						
					}while(!IntentarMover(kibus, direccion, false));
					
					int anterior = kibus.OnToyY + kibus.OnToyX * 20;
					
					kibus.Mover(direccion);
					
					int actual = kibus.OnToyY + kibus.OnToyX * 20;
					
					if(nodosVisitados[actual] == null)
					{
						nodosVisitados[actual] = new Nodo();
					}
					
					Conexion cnxDestino;
					
					if(nodosVisitados[anterior].conexion[(int)direccion] == null)
					{
						cnxDestino = new Conexion();
					}
					else
					{
						cnxDestino = nodosVisitados[anterior].conexion[(int)direccion];
					}
					                                     
					cnxDestino.NodoConexion = nodosVisitados[actual];
					
					//Verificacion chafa a falta de la instruccion Assert.
					if(nodosVisitados[anterior] == null)
					{
						throw new Exception("Esto no debe pasar");
					}
					
					
					nodosVisitados[anterior].conexion[(int)direccion] = cnxDestino;
					
					
					
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
				
				
				Console.WriteLine("Maximo :" + maximo);
				Console.WriteLine("Minimo :" + minimo);
				Console.WriteLine("Media  :" + mediaHistorica);
				Console.WriteLine("Conexiones: " + Conexion.conexionesUsadas);
				Console.WriteLine("Castigo: " + castigo);
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
						for(int j = 0; j < nodosVisitados[i].conexion.Length; j++)
						{
							
							if(nodosVisitados[i].conexion[j] != null) //Si la conexion se generó se verá actualizado su valor en el mapa
							{
								if(arregloNodosTotal[i].conexion[j] == null) //si esto ocurre, se asignal la conexion tal cual, pues es nueva
								{
									arregloNodosTotal[i].conexion[j] = nodosVisitados[i].conexion[j];
								}
								else //se actualizara el valor dependiendo del castigo
								{
									arregloNodosTotal[i].conexion[j].Peso += castigo;
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

