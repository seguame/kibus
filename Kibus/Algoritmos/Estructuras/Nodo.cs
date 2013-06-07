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
		
		public Conexion conexion
		{
			set;
			get;
		}
		
		public Nodo()
		{
			CantidadNodosVisitados++;
		}
	}
	
	
	public class Conexion
	{
		public Nodo NodoConexion {get; set;}
		public int Peso {get; set;}
	}
}

