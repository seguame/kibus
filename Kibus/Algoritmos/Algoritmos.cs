//  
//  Algoritmos.cs
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

using Utileria;
using Algoritmos.Estructuras;
using Esedelish;


namespace Algoritmos
{
	public class Algoritmo
	{
		public static Queue<Direccion> CalculaLineaBresenham(int Xinicial, int Yinicial, int Xfinal, int Yfinal)
		{
			Queue<Direccion> cola = new Queue<Direccion>();
			Console.WriteLine("Inicio:{0} {1}\nFin:{2} {3}\n", Xinicial, Yinicial, Xfinal, Yfinal);
			int deltaX, deltaY, x, y, ultimaX, ultimaY, p;
			int incrementoY = 1,incrementoX = 1; 
			bool yInversa = false;
			bool xInversa = false;
			
			deltaX = Math.Abs(Xinicial - Xfinal);
			deltaY = Math.Abs(Yinicial - Yfinal);
			
			if(deltaY > deltaX) //m >= 1
		    {
				p =  (2 * deltaX) - deltaY;
				
				if(Yinicial > Yfinal)
				{
					yInversa = true;
					incrementoY = -1;
				}
				
				if(Xinicial > Xfinal)//si la coord X inicial es menor que la final
		        {
					//tenemos que irnos hacia atras
					incrementoX = -1;
					xInversa = true;
				}
				
				x       = Xinicial;
				y       = Yinicial;
				ultimaY = Yfinal;
				
				
				while(yInversa ? y >= ultimaY : y <= ultimaY) //mientras Y no sea Y final
		        {
					y += incrementoY; //vamos incrementando Y
					
					if (p < 0) //si 'p' es negativa, solo incrementamos Y
		            {
						p += 2 * deltaX;
						Console.WriteLine("Pondriamos X:{0} Y:{1}", x, y - incrementoY);
						Console.WriteLine(DireccionUtils.DeterminarDireccion(x, y, x, y - incrementoY, xInversa, yInversa));
						cola.Enqueue(DireccionUtils.DeterminarDireccion(x, y, x, y - incrementoY, xInversa, yInversa));
						
					}
					else //sino, incrementamos X
		            {
						x += incrementoX;
						p += 2 *(deltaX - deltaY);
						
						Console.WriteLine("Pondriamos X:{0} Y:{1}", x - incrementoX, y - incrementoY);
						Console.WriteLine(DireccionUtils.DeterminarDireccion(x, y, x - incrementoX , y - incrementoY, xInversa, yInversa));
						cola.Enqueue(DireccionUtils.DeterminarDireccion(x, y, x - incrementoX , y - incrementoY, xInversa, yInversa));
					}
					
					Console.WriteLine();
				}
			}
			else  //0 <= m < 1
		    {
				p = (2 * deltaY) - deltaX;
				
				if(Xinicial > Xfinal)
				{
					incrementoX = -1;
					xInversa = true;
				}
				
				if(Yinicial > Yfinal)
				{
					//tenemos que graficar hacia abajo
					incrementoY = -1;
					yInversa = true;
				}
				
				x       = Xinicial;
				y       = Yinicial;
				ultimaX = Xfinal;
				
				while(xInversa? x >= ultimaX : x <= ultimaX)
				{
					x += incrementoX;
					
					if (p < 0)//si 'p' es negativo solo incrementamos X
		            {
						p += 2 * deltaY;
						
						Console.WriteLine("Pondriamos X:{0} Y:{1}", x - incrementoX, y);
						Console.WriteLine(DireccionUtils.DeterminarDireccion(x, y, x - incrementoX, y, xInversa, yInversa));
						cola.Enqueue(DireccionUtils.DeterminarDireccion(x, y, x - incrementoX, y, xInversa, yInversa));
						
					}
					else
					{
						//sino incrementamos Y
						y += incrementoY;
						p += 2 * (deltaY - deltaX);
						
						Console.WriteLine("Pondriamos X:{0} Y:{1}", x - incrementoX, y - incrementoY);
						Console.WriteLine(DireccionUtils.DeterminarDireccion(x, y, x -incrementoX, y - incrementoY, xInversa, yInversa));
						cola.Enqueue(DireccionUtils.DeterminarDireccion(x, y, x -incrementoX, y - incrementoY, xInversa, yInversa));
						
					}
					
					Console.WriteLine();
				}
			}
			
			cola.Dequeue();// El primero no nos sirve
			
			return cola;
		}
		
		private static Stack<Nodo> anteriores;
		private static List<Direccion> listaMovimientos;
		
		public static Queue<Direccion> PrimeroElMejor(Nodo origen, Nodo destino)
		{
			anteriores = new Stack<Nodo>();
			listaMovimientos = new List<Direccion>();
			Nodo actual = origen;
			
			do
			{
				Conexion tmp = BuscarConexionMenor( actual );
				
				anteriores.Push(actual); // pila de anteriores
				actual = tmp.NodoConexion;
				listaMovimientos.Add(tmp.direccionUsada);
			}while(actual.numeroDeNodo != destino.numeroDeNodo);
			
			return new Queue<Direccion>(listaMovimientos);
		}
		
		private static Conexion BuscarConexionMenor( Nodo actual)
		{
			Conexion[] conexiones = actual.conexiones;
			Conexion menor = null;
			
			foreach(Conexion cnx in conexiones)
			{
				if(cnx != null && !cnx.NodoConexion.Visitado) 
	    		{
					if(menor == null || cnx.NodoConexion.numeroDeNodo == 1)
					{
						menor = cnx;
					}
					else
					{
						if(menor.Peso > cnx.Peso)
						{
							menor = cnx;
						}
					}
					
					if(menor.NodoConexion.numeroDeNodo == 1) //es el destino
						break;
	    		}
			}
			
			if(menor == null)
			{
				//Se elimina la ultima adicion pues nos lleva a un camino cíclico. 
				listaMovimientos.RemoveAt(listaMovimientos.Count - 1);
				menor = BuscarConexionMenor(anteriores.Pop());
			}
			else
			{
				menor.NodoConexion.Visitado = true;
			}
			
			return menor;
		}
		
		
		private static Queue<Direccion> movimientos;
		
		public static Queue<Direccion> Dijkstra(Nodo[] mapa, Nodo origen, Nodo destino)
		{
			movimientos = new Queue<Direccion>();
			
			HacerDijkstra(mapa, origen, destino, 0);
			Console.WriteLine("Distancia a recorrer :" + destino.distancia);
			return movimientos;
		}
		/*
	     * Recorremos los distintos nodos que vamos visitando
	     * El primer valor es posición del nodo origen (el nodo origen se va modificando
	     * conforme vamos avanzando en la busqueda)
	     * El segundo valor es posición del nodo destino final.
	     * Distancia del nodo examinado. Este valor lo sumaremos al del valor modificable obteniendo 
	     * la distancia real entre el nodo origen y los nodos examinados
	     */
	    private static void HacerDijkstra(Nodo[] mapa, Nodo origen, Nodo destino, int pesoAcumulado)
	    {
			
			
	        foreach(Nodo n in mapa)
	        {
				if(n  != null)
				{
					foreach(Conexion cnx in n.conexiones)
					{
						if(cnx != null)
						{
							/* Si el nodo origen tiene alguna conexión con 
		                 	* el nodo examinado Y El nodo examinado en la 
		                 	* tabla Dijkstra no tiene un valor todavía 
		                 	* o Si el nodo examinado en la tabla Dijkstra 
		                 	* tiene valor pero es superior al que obtendremos. */
							if(cnx.NodoConexion.numeroDeNodo == origen.numeroDeNodo && 
							  (n.distancia == -1 || (pesoAcumulado + cnx.Peso) < n.distancia))
							{
								n.distancia = pesoAcumulado + cnx.Peso;
							}
								
						}
					}
				}
	        }
			
			Console.WriteLine("Origen " + origen.ToString());
			origen.Visitado = true;
	        Nodo nodo = BuscarNodoDeMenorPeso(mapa);
	        if (nodo != null)
	        {
				int pos =  ObtenerPosicionNodo(mapa, nodo);
				Console.WriteLine(pos);
	            HacerDijkstra(mapa, nodo, destino, mapa[ pos ].distancia);
	        }
	    }
		
		/**
	     * Obtenemos la posición del nodo que dispone de la menor distancia.
	     * La obtenemos entre los nodos no visitados y accesibles.
	     * Devolvemos la posición del nodo con el peso mas bajo
	     */
		private static Nodo BuscarNodoDeMenorPeso(Nodo[] mapa)
		{
			Int32 pesoMinimo = Int32.MaxValue;
	        Nodo nodoCorto = null;
	        foreach(Nodo n in mapa)
	        {
				if(n != null)
				{
					foreach(Conexion cnx in n.conexiones)
					{
						if(cnx != null)
						{
							/* Si el nodo de la tabla Dijkstra no es de los 
				             * ya visitados, se le ha asignado ya alguna distancia 
				             * y o todavía no hemos designado el nodo de peso mínimo
				             * o ya habiéndolo designado el examinado es inferior 
				             * al ya propuesto */
				            if (!cnx.NodoConexion.Visitado && cnx.Peso < pesoMinimo)
				            {
				                pesoMinimo = cnx.Peso;
				                nodoCorto = cnx.NodoConexion;
				            }
						}
					}
				}
	        }
	        return nodoCorto;
		}
		
		/**
	     * Obtenemos la posición del nodo en función del numero de nodo
	     */ 
	    private static int ObtenerPosicionNodo(Nodo[] mapa, Nodo nodo)
	    {
			return nodo.numeroDeNodo -1;
			/*Console.WriteLine("Buscando nodo " + nodo.numeroDeNodo);
			for(int i = 0;i < mapa.Length; i++)
			{
				if(mapa[i] != null && mapa[i].numeroDeNodo == nodo.numeroDeNodo)
					return i;
			}
			
			Console.WriteLine("No existe");
	        return -1;*/
	    }     
	}
}

