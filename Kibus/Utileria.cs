//  
//  Utileria.cs
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
using Algoritmos.Estructuras;


namespace Utileria
{
	public enum Direccion
	{
		ARRIBA,
		IZQUIERDA,
		ARRIBA_IZQ,
		ARRIBA_DER,
		ABAJO_IZQ,
		ABAJO_DER,
		DERECHA,
		ABAJO,
		MISINGNO
	};
	
	public static class DireccionUtils
	{
		public static Direccion DeterminarDireccion(int nuevaX, int nuevaY, int viejaX, int viejaY,bool xInversa, bool yInversa)
		{
			//Primero las direcciones compuestas
			if(viejaX > nuevaX && viejaY < nuevaY){ Console.WriteLine("vX:{0} > nX:{2} | vY:{1} < nY:{3}  {4} {5}", viejaX, viejaY, nuevaX, nuevaY, xInversa, yInversa); return Direccion.ABAJO_IZQ;}
			if(viejaX < nuevaX && viejaY < nuevaY){ Console.WriteLine("vX:{0} < nX:{2} | vY:{1} < nY:{3}  {4} {5}", viejaX, viejaY, nuevaX, nuevaY, xInversa, yInversa); return Direccion.ABAJO_DER;}
			if(viejaX > nuevaX && viejaY > nuevaY){ Console.WriteLine("vX:{0} > nX:{2} | vY:{1} > nY:{3}  {4} {5}", viejaX, viejaY, nuevaX, nuevaY, xInversa, yInversa); return Direccion.ARRIBA_IZQ;}
			if(viejaX < nuevaX && viejaY > nuevaY){ Console.WriteLine("vX:{0} < nX:{2} | vY:{1} > nY:{3}  {4} {5}", viejaX, viejaY, nuevaX, nuevaY, xInversa, yInversa); return Direccion.ARRIBA_DER;}
			
			//Y ya pues las normales
			if(viejaX < nuevaX){ Console.WriteLine("vX:{0} < nX:{1} {2}", viejaX, nuevaX, xInversa); return Direccion.DERECHA; }
			if(viejaX > nuevaX){ Console.WriteLine("vX:{0} > nX:{1} {2}", viejaX, nuevaX, xInversa); return Direccion.IZQUIERDA; }
			if(viejaY < nuevaY){ Console.WriteLine("vY:{0} < nY:{1} {2}", viejaY, nuevaY, yInversa); return Direccion.ABAJO; }
			if(viejaY > nuevaY){ Console.WriteLine("vX:{0} > nX:{1} {2}", viejaY, nuevaY, yInversa); return Direccion.ARRIBA; }
			
			return Direccion.MISINGNO;
		}
	}
	
	
	public class ComparadorNodos : Comparer<Nodo>
	{
		#region implemented abstract members of System.Collections.Generic.Comparer
		public override int Compare (Nodo x, Nodo y)
		{
			return x.numeroDeNodo - y.numeroDeNodo;
		}
		#endregion
	}
}

