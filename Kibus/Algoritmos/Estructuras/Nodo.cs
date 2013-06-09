//  
//  Nodo.cs
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
using System.Text;
using Utileria;

namespace Algoritmos.Estructuras
{
	/*public class ArregloNodosMapa
	{
		public int CantidadNodosVisitados
		{
			private set;
			get;
		}
		
		private Nodo[] arregloNodos;
		
		public ArregloNodosMapa()
		{
			CantidadNodosVisitados = 0;
			arregloNodos = new Nodo[20*20];
		}
		
		public void agregarNodoConexion(int posicion, Conexion conexion)
		{
			if(arregloNodos[posicion] == null)
			{
				arregloNodos[posicion] = new Nodo();
				CantidadNodosVisitados += 1;
			}
			
			arregloNodos[posicion].conexion = conexion;
		}
	}*/
	
	public class Nodo
	{
		public static int CantidadNodosVisitados = 0;
		
		public Conexion[] conexiones;
		public int numeroDeNodo;
		public bool Visitado {get; set;}
		
		public Nodo()
		{
			Visitado = false;
			conexiones = new Conexion[8];
			numeroDeNodo = ++CantidadNodosVisitados;
		}
		
		public override string ToString()
		{
			StringBuilder salida = new StringBuilder("");
			
			switch(numeroDeNodo)
			{
			case 1:
				salida.AppendLine("Nodo CASA");
				break;
			case 2:
				salida.AppendLine("Nodo Salida");
				break;
			default:
				salida.AppendLine(string.Format("Nodo {0}",numeroDeNodo));
				break;
			}
			
			foreach(Conexion cnx in conexiones)
			{
				salida.Append("\t");
				if(cnx == null)
				{
					salida.AppendLine("[Conexion: NodoConexion = null\t Peso = -1\tVisitado = ???]");
				}
				else
				{
					salida.AppendLine(cnx.ToString());
				}
			}
			
			return salida.ToString();
		}
		
		public override bool Equals(object otro)
		{
			return ((Nodo)otro).numeroDeNodo == this.numeroDeNodo;
		}
		
		public override int GetHashCode ()
		{
			return (numeroDeNodo * numeroDeNodo);
		}
	}
	
	
	public class Conexion
	{
		public static int conexionesUsadas = 0;
		public Nodo NodoConexion {get; set;}
		public int Peso {get; set;}
		public Direccion direccionUsada;
		
		public Conexion()
		{
			Peso = (Int32.MaxValue / 2);
			conexionesUsadas++;
		}
		
		public override string ToString ()
		{
			switch(NodoConexion.numeroDeNodo)
			{
			case 1:
				return string.Format ("[Conexion: NodoConexion = CASA\t Peso = {0}\tVisitado = {1}]", Peso, NodoConexion.Visitado);
			case 2:
				return string.Format ("[Conexion: NodoConexion = SALIDA Peso = {0}\tVisitado = {1}]", Peso, NodoConexion.Visitado);
			default:
				return string.Format ("[Conexion: NodoConexion = {0}\t Peso = {1}\tVisitado = {2}]", NodoConexion.numeroDeNodo, Peso, NodoConexion.Visitado);
			}
			
		}
	}
}

