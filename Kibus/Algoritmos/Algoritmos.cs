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
	}
}

