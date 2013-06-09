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
		
		public Conexion[] conexion;
		public int numeroDeNodo;
		
		public Nodo()
		{
			conexion = new Conexion[8];
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
			
			foreach(Conexion cnx in conexion)
			{
				//salida.Append("\t");
				if(cnx == null)
				{
					//salida.AppendLine("[Conexion: NodoConexion = null, Peso = -1]");
				}
				else
				{
					salida.AppendLine("\t"+cnx.ToString());
				}
			}
			
			return salida.ToString();
		}
	}
	
	
	public class Conexion
	{
		public static int conexionesUsadas = 0;
		public Nodo NodoConexion {get; set;}
		public int Peso {get; set;}
		
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
				return string.Format ("[Conexion: NodoConexion = CASA\t, Peso = {0}]", Peso);
			case 2:
				return string.Format ("[Conexion: NodoConexion = SALIDA, Peso = {0}]", Peso);
			default:
				return string.Format ("[Conexion: NodoConexion = {0}\t, Peso = {1}]", NodoConexion.numeroDeNodo, Peso);
			}
			
		}
	}
}

