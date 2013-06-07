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
		private int mediaHistoricaAnterior;
		private int castigo;
		//private ArregloNodosMapa nodosMapa;
		private Nodo[] arregloNodosTotal;
		
		private Nodo[] nodosVisitados;
		private Conexion[] conexionesVisitadas;
		
		private bool pimeraVuelta = true;
		
		
		private int[][] mapa;
		bool modoGrafico;
		
		public Nivel4 ()
		{
			new CargadorMapas().SeleccionarArchivo();
			
			//nodosMapa = new ArregloNodosMapa();
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
			
			//Setear los 2 nodos iniciales, origen y destino
			arregloNodosTotal[kibus.OnToyY + kibus.OnToyX * 20] = new Nodo();
			arregloNodosTotal[casa.OnToyY + casa.OnToyX * 20] = new Nodo();
			
			//El entrenamiento puesun
			ConfeccionarCamino();
		}
		
		private void ConfeccionarCamino()
		{
			//Volver a iniciar los valores;
			nodosVisitados = new Nodo[20*20];
			conexionesVisitadas = new Conexion[20*20];
			Nodo.CantidadNodosVisitados = 0;
			
			Direccion direccion;
			Random random = new Random(System.DateTime.Now.Millisecond);
			
			Console.WriteLine("CASA: {0},{1}", casa.OnToyX,casa.OnToyY);
			Console.WriteLine("KIBU: {0},{1}", kibus.OnToyX,kibus.OnToyY);
			
			while(kibus.OnToyX != casa.OnToyX || kibus.OnToyY != casa.OnToyY)
			{
				modoGrafico = !Hardware.TeclaPulsada(Sdl.SDLK_g);
				
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
				
				Conexion destino = new Conexion();
				
				kibus.Mover(direccion);
				
				int actual = kibus.OnToyY + kibus.OnToyX * 20;
				
				if(arregloNodosTotal[actual] == null)
				{
					arregloNodosTotal[actual] = new Nodo();
				}
				
				destino.NodoConexion = arregloNodosTotal[actual];
				
				//Verificacion chafa a falta de la instruccion Assert.
				if(arregloNodosTotal[anterior] == null)
				{
					throw new Exception("Esto no debe pasar");
					//arregloNodosTotal[anterior] = new Nodo();
				}
				
				
				arregloNodosTotal[anterior].conexion = destino;
				
				
				
				if(modoGrafico)
				{
					Hardware.Pausar(2);
				}
				else
				{
					//Hardware.Pausar(2);
				}
			}
			
			Console.WriteLine("CASA: {0},{1}", casa.OnToyX,casa.OnToyY);
			Console.WriteLine("KIBU: {0},{1}", kibus.OnToyX,kibus.OnToyY);
			
			Console.WriteLine("Nodos visitados: {0}", Nodo.CantidadNodosVisitados);
			
			
			//Seteo de los valores minimos y máximos
			if(Nodo.CantidadNodosVisitados > maximo) maximo = Nodo.CantidadNodosVisitados;
			if(Nodo.CantidadNodosVisitados < minimo) minimo = Nodo.CantidadNodosVisitados;
			
			mediaHistorica = (maximo + minimo)/2;
			
			if(pimeraVuelta)
			{
				mediaHistoricaAnterior = mediaHistorica;
				pimeraVuelta = false;
			}
			
			castigo = Math.Abs(mediaHistorica - mediaHistoricaAnterior);
		}
	}
}

